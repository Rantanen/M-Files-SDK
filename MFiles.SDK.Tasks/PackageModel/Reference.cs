using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using Ionic.Zip;
using Microsoft.Build.Framework;

namespace MFiles.SDK.Tasks.PackageModel
{
	class Reference
	{
		public string FullPath { get; set; }
		public string PackageName { get; set; }

		protected XDocument AppDef { get; set; }
		public ZipFile Zip { get; set; }

		public Reference( ITaskItem item )
		{
			FullPath = item.GetMetadata( "FullPath" );

			Zip = new ZipFile( FullPath );
			var appdef = Zip.FirstOrDefault( ze => ze.FileName.ToLowerInvariant() == "appdef.xml" );

			if( appdef == null )
				throw new FileNotFoundException( string.Format(
					"The package definition file 'appdef.xml' was not found in the referenced package '{0}'",
					Path.GetFileName( FullPath ) ) );

			var appdefStream = new MemoryStream();
			appdef.Extract( appdefStream );

			appdefStream.Position = 0;
			AppDef = XDocument.Load(appdefStream);

			PackageName = AppDef.Element( "application" ).Element( "name" ).Value;
		}

		public IEnumerable<string> GetScriptsForEnvironment( TargetEnvironment environment )
		{
			var env = environment.ToString().ToLowerInvariant();

			var modules = AppDef.Element( "application" )
					.Element( "modules" );

			if( modules == null ) return Enumerable.Empty<string>();

			var module = modules.Elements( "module" ).Where(
				m => m.Attribute( "environment" ).Value.ToLowerInvariant() == env );

			if( module == null ) return Enumerable.Empty<string>();

			var excluded = new List<string> { "_package_start.js", "_package_end.js" };

			var files = module.Elements( "file" )
				.Where( f => !excluded.Contains( f.Value.ToLowerInvariant() ) )
				.Select( f => f.Value );
			return files;
		}

		public Dictionary<string, string> GetDashboards()
		{
			var dashboards = AppDef.Element( "application" ).Element( "dashboards" );

			if( dashboards == null ) return new Dictionary<string,string>();

			return dashboards.Elements( "dashboard" ).ToDictionary(
				db => db.Attribute( "id" ).Value,
				db => db.Element( "content" ).Value );
		}
	}
}
