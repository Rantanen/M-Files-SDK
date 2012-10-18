using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Framework;

namespace MFiles.SDK.Tasks.PackageModel
{
	public enum TargetEnvironment { None, ShellUI, VaultUI, VaultCore, All }

	public class PackageFile
	{
		public string FullPath { get; set; }
		public string PathInProject { get; set; }
		public TargetEnvironment Environment { get; set; }
		public bool IsDashboard { get; set; }

		public PackageFile( ITaskItem item )
		{
			FullPath = item.GetMetadata( "FullPath" );
			PathInProject = item.ItemSpec;

			var environmentData = item.GetMetadata( "Environment" );
			if( string.IsNullOrEmpty( environmentData ) )
				environmentData = "None";

			var isDashboardData = item.GetMetadata( "IsDashboard" );
			if( string.IsNullOrEmpty( isDashboardData ) )
				isDashboardData = "false";

			Environment = ( TargetEnvironment )Enum.Parse( typeof( TargetEnvironment ), environmentData, true );
			IsDashboard = bool.Parse( isDashboardData );
		}
	}
}
