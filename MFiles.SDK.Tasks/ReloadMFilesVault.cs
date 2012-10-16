using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Build.Utilities;

namespace MFiles.SDK.Tasks
{
	public class ReloadMFilesVault : Task
	{
		public override bool Execute()
		{
			MFilesAPI.MFilesClientApplication app = new MFilesAPI.MFilesClientApplication();
			try
			{
				var vault = app.BindToVault( VaultName, IntPtr.Zero, false, true );
				if( vault != null )
					vault.LogOutWithDialogs( IntPtr.Zero );
			}
			catch
			{
				// This is most likely caused by logged out vault which is what we wanted anyway.
			}

			return true;
		}

		public string VaultName { get; set; }
	}
}
