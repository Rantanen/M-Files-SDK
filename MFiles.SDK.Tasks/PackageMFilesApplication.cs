using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using MFiles.SDK.Tasks.PackageDefinition;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace MFiles.SDK.Tasks
{
	public class PackageMFilesApplication : Task
	{
		public override bool Execute()
		{
			Log.LogMessage( MessageImportance.Low, "Packing M-Files Application." );

			// Gather all the JavaScript files
			var scripts = new List<string>();
			scripts.AddRange( SourceFiles );

			// Gather all reference files.
			// For now we do not support project or zip references so this step doesn't require anything special.
			var referenceFiles = References ?? new string[] { };

			Log.LogMessage( MessageImportance.Low, "Resolving references." );

			// Make sure the referenced files exist.
			var missingFiles = referenceFiles.Where( r => !File.Exists( r ) ).ToList();
			foreach( var missingFile in missingFiles )
				Log.LogError( "Referenced file '{0}' does not exist.", missingFile );
			if( missingFiles.Count > 0 )
				return false;

			// Resolve references by filename.
			var referencesByName = referenceFiles.GroupBy( f => Path.GetFileName( f ) );

			// Resolve the final filenames.
			var finalReferences = new Dictionary<string, string>();
			foreach( var nameGroup in referencesByName )
			{
				var fileName = nameGroup.Key;
				var files = nameGroup.ToList();

				if( fileName.ToLower() == "appdef.xml" ) continue;
				if( fileName.ToLower() == "_package.js" ) continue;

				for( int i = 0; i < files.Count; i++ )
				{
					// If there are more than one file with the same name, use a $i as postfix for it.
					string postfix = "";
					if( files.Count > 1 )
						postfix = "$" + i;

					string finalFileName =
						Path.GetFileNameWithoutExtension( fileName ) +
						postfix +
						Path.GetExtension( fileName );

					finalReferences[ files[ i ] ] = finalFileName;
				}

			}

			// Generate the package definition.
			var appdef = new ApplicationDefinition();
			appdef.Guid = this.Guid.Replace("{", "").Replace("}", "");
			appdef.Name = this.Name;
			var appfiles = new List<ApplicationFile>();
			appfiles.Add( new ApplicationFile { Name = "_package.js" } );
			foreach( var environment in new[] { "vaultcore", "vaultui", "shellui" } )
			{
				var module = new ApplicationModule { Environment = environment };
				appdef.Modules.Add( module );
				module.Files = appfiles;
			}

			// Build the zip file.

			// Add the local scripts.
			if( File.Exists( OutputFile ) )
				File.Delete( OutputFile );
			var outputZip = new Ionic.Zip.ZipFile( OutputFile );
			foreach( var file in SourceFiles )
			{
				outputZip.AddFile( file );
				appfiles.Add( new ApplicationFile { Name = file } );
			}

			// Add the referenced scripts.
			foreach( var reference in finalReferences )
			{
				var file = reference.Key;
				if( Path.GetExtension( file ) == ".zip" )
				{
					var inputZip = new Ionic.Zip.ZipFile( file );
					foreach( var e in inputZip.Entries )
					{
						var filename = Path.GetFileName( e.FileName ).ToLower();
						if( filename == "appdef.xml" ) continue;
						if( filename == "_package.js" ) continue;

						var tempStream = new MemoryStream();
						e.Extract( tempStream );
						tempStream.Position = 0;
						var projectName = Path.GetFileNameWithoutExtension( reference.Value );
						var entryPath = "_references/" + projectName + "/" + e.FileName;
						outputZip.AddEntry( entryPath, tempStream );
						appfiles.Add( new ApplicationFile { Name = entryPath } );
					}
				}
				else
				{
					var entry = outputZip.AddFile( file );
					entry.FileName = "_references/" + reference.Value;
					appfiles.Add( new ApplicationFile { Name = entry.FileName } );
				}
			}

			var stream = new MemoryStream();
			var serializer = new XmlSerializer( typeof( ApplicationDefinition ) );
			serializer.Serialize( stream, appdef );
			stream.Flush();
			stream.Position = 0;
			outputZip.AddEntry( "appdef.xml", stream );

			var packageStream = this.GetType().Assembly.GetManifestResourceStream( "MFiles.SDK.Tasks.Scripts.package.js" );
			outputZip.AddEntry( "_package.js", packageStream );

			outputZip.Save();

			LogArray( "Scripts", outputZip.Entries.Select( e => e.FileName ) );

			return true;
		}

		private void LogArray( string name, IEnumerable<string> array )
		{
			Log.LogMessage( MessageImportance.High, name + ":" );
			if( array == null ) return;
			foreach( var item in array )
				Log.LogMessage( MessageImportance.High, "  " + item );
		}

		public string Name { get; set; }
		public string Version { get; set; }
		public string Guid { get; set; }
		public string[] SourceFiles { get; set; }
		public string OutputFile { get; set; }
		public string[] References { get; set; }
	}
}
