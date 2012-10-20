using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Ionic.Zip;
using MFiles.SDK.Tasks.PackageDefinition;
using MFiles.SDK.Tasks.PackageModel;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace MFiles.SDK.Tasks
{
	public class PackageMFilesApplication : Task
	{
		public override bool Execute()
		{
			Log.LogMessage( MessageImportance.Low, "Packing M-Files Application." );

			// Make sure the collections are never null.
			References = References ?? new ITaskItem[ 0 ];
			SourceFiles = SourceFiles ?? new ITaskItem[ 0 ];
			DefaultEnvironments = DefaultEnvironments ?? new string[ 0 ];

			// Create the application package contents.
			var references = References.Select( item => new Reference( item ) ).ToList();
			var files = SourceFiles.Select( item => new PackageFile( item ) ).ToList();

			var appDef = CreateApplicationDefinition( references, files );
			var outputZip = CreatePackage( references, files );

			// Serialize the application definition file.
			var stream = new MemoryStream();
			var serializer = new XmlSerializer( typeof( ApplicationDefinition ) );
			serializer.Serialize( stream, appDef );
			stream.Flush();
			stream.Position = 0;
			outputZip.AddEntry( "appdef.xml", stream );

			// Save the zip.
			outputZip.Save();

			return true;
		}

		private ZipFile CreatePackage( List<Reference> references, List<PackageFile> files )
		{
			// Create the package.
			if( File.Exists( OutputFile ) )
				File.Delete( OutputFile );
			var outputZip = new Ionic.Zip.ZipFile( OutputFile );

			// Add the project files.
			foreach( var file in files )
			{
				outputZip.AddFile( file.FullPath ).FileName = file.PathInProject;
			}

			// Add the referenced scripts.
			foreach( var reference in references )
			{
				foreach( var e in reference.Zip )
				{
					var filename = Path.GetFileName( e.FileName ).ToLower();

					// There are some files that shouldn't be included from the references.
					if( filename == "appdef.xml" ) continue;
					if( filename == "libdef.xml" ) continue;
					if( filename == "_package_start.js" ) continue;
					if( filename == "_package_end.js" ) continue;

					var tempStream = new MemoryStream();
					e.Extract( tempStream );
					tempStream.Position = 0;

					var entryPath = string.Format( "{0}/{1}", reference.PackageName, e.FileName );
					outputZip.AddEntry( entryPath, tempStream );
				}
			}

			// Add the bootstrap libraries.
			var packageStartStream = this.GetType().Assembly.GetManifestResourceStream( "MFiles.SDK.Tasks.Scripts.package_start.js" );
			outputZip.AddEntry( "_package_start.js", packageStartStream );
			var packageEndStream = this.GetType().Assembly.GetManifestResourceStream( "MFiles.SDK.Tasks.Scripts.package_end.js" );
			outputZip.AddEntry( "_package_end.js", packageEndStream );
			return outputZip;
		}

		private ApplicationDefinition CreateApplicationDefinition( List<Reference> references, IEnumerable<PackageFile> files )
		{
			var defaultEnvironments = DefaultEnvironments.Select(
				e => ( TargetEnvironment )Enum.Parse( typeof( TargetEnvironment ), e ) );

			var scripts = files.Where( f => f.Environment != TargetEnvironment.None );
			var specifiedEnvironments = scripts.Where( f => f.Environment != TargetEnvironment.All ).Select( f => f.Environment ).Distinct();
			specifiedEnvironments = specifiedEnvironments.Union( defaultEnvironments );

			Guid = Guid.Replace( "{", "" ).Replace( "}", "" );
			ApplicationDefinition appdef = new ApplicationDefinition { Name = Name, Guid = Guid };

			// Create the module elements.
			foreach( var env in specifiedEnvironments )
			{
				var module = new ApplicationModule { Environment = env.ToString().ToLower() };
				appdef.Modules.Add( module );

				// Add the bootstrap scripts.
				module.Files.Add( new ApplicationFile { Name = "_package_start.js" } );

				// Add javascript files from the references.
				foreach( var r in references )
				{
					var referencedScripts = r.GetScriptsForEnvironment( env );
					foreach( var script in referencedScripts )
					{
						var file = new ApplicationFile { Name = r.PackageName + "/" + script };
						module.Files.Add( file );
					}
				}

				// Add the application's own javascript files.
				var environmentScripts = scripts.Where( s => s.Environment == TargetEnvironment.All || s.Environment == env );
				foreach( var script in environmentScripts )
				{
					var file = new ApplicationFile { Name = script.PathInProject };
					module.Files.Add( file );
				}

				module.Files.Add( new ApplicationFile { Name = "_package_end.js" } );
			}

			foreach( var db in files.Where(f => f.IsDashboard ) )
			{
				var dashboard = new ApplicationDashboard
				{
					Id = Path.GetFileNameWithoutExtension(db.PathInProject),
					Content = db.PathInProject
				};
				appdef.Dashboards.Add(dashboard);
			}

			foreach( var r in references )
			{
				foreach( var db in r.GetDashboards() )
				{
					var dashboard = new ApplicationDashboard
					{
						Id = db.Key,
						Content = r.PackageName + "/" + db.Value
					};
					appdef.Dashboards.Add( dashboard );
				}
			}

			return appdef;
		}

		[Required]
		public string Name { get; set; }

		[Required]
		public string Guid { get; set; }

		[Required]
		public ITaskItem[] SourceFiles { get; set; }

		[Required]
		public string OutputFile { get; set; }

		public string Version { get; set; }
		public ITaskItem[] References { get; set; }
		public string[] DefaultEnvironments { get; set; }
	}
}
