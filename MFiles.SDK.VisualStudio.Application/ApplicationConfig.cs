using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio.Shell;

namespace MFiles.SDK.VisualStudio.Application
{
	class ApplicationConfig : ProjectConfig
	{
		ApplicationProjectNode project;

		public ApplicationConfig( ApplicationProjectNode project, string config )
			: base( project, config )
		{
			this.project = project;
		}

		public override int DebugLaunch( uint grfLaunch )
		{
			CCITracing.TraceCall();

			// Resolve the test vault.
			var clientApp = new MFilesAPI.MFilesClientApplication();
			var vaultName = GetConfigurationProperty( "TestVault", true );
			MFilesAPI.VaultConnection vaultConnection;

			try
			{
				// Try to get the connection.
				vaultConnection = clientApp.GetVaultConnection( vaultName );
			}
			catch
			{
				// Vault wasn't found, ask the user for a new one.
				var selectVaultDialog = new SelectVaultDialog();
				selectVaultDialog.ShowDialog();
				if( selectVaultDialog.Result == System.Windows.Forms.DialogResult.Cancel )
					return VSConstants.S_FALSE;

				// Get the user answer.
				vaultName = selectVaultDialog.VaultName;
				vaultConnection = clientApp.GetVaultConnection( selectVaultDialog.VaultName );

				// If the user defined this as the default vault, save it in the project file.
				if( selectVaultDialog.SetDefault )
					this.SetConfigurationProperty( "TestVault", selectVaultDialog.VaultName );
			}

			// Get the M-Files install directory from the registry.
			var apiVersion = clientApp.GetAPIVersion().Display;
			var hklm64 = RegistryKey.OpenBaseKey( RegistryHive.LocalMachine, RegistryView.Registry64 );
			var mfKey = hklm64.OpenSubKey( @"Software\Motive\M-Files\" + apiVersion );
			var installDir = (string)mfKey.GetValue( "InstallDir" );
			mfKey.Close();
			hklm64.Close();

			// Log out to free the current application.
			MFilesAPI.Vault vault = null;
			try
			{
				vault = clientApp.BindToVault( vaultName, IntPtr.Zero, false, true );
				if( vault != null ) vault.LogOutWithDialogs( IntPtr.Zero );
			}
			catch
			{
				// We most likely weren't logged in so everything is okay.
			}

			// Deploy the application.
			string vaultGuid = vaultConnection.GetGUID();
			var relativePath = string.Format( @"Client\Apps\{0}\sysapps\{1}",
				vaultGuid, this.project.GetProjectProperty( "Name" ) ?? "unnamed" );
			var targetDir = Path.Combine( installDir, relativePath );

			// If the directory exists, remove it so there's no residue files left.
			if( Directory.Exists( targetDir ) ) { Directory.Delete( targetDir, true ); }
			Directory.CreateDirectory( targetDir );

			// Extract the Zip contents to the target directory.
			DeployPackage( targetDir );

			// Log back into the application.
			vault = clientApp.BindToVault( vaultName, IntPtr.Zero, true, true );

			// If vault is null, the user cancelled the login -> Exit
			if( vault == null ) return VSConstants.S_FALSE;

			// Figure out the launch mode.
			var launchMode = ( GetConfigurationProperty( "LaunchMode", false ) ?? "" ).ToLowerInvariant();
			if( launchMode == "powershell" )
			{
				// Create a powershell window to launch the application.

				// Create the initial state with the app and vault references.
				var builtInState = InitialSessionState.CreateDefault();
				builtInState.Variables.Add( new SessionStateVariableEntry(
					"app", clientApp, "M-Files Application" ) );
				builtInState.Variables.Add( new SessionStateVariableEntry(
					"vault", vault, "M-Files Vault" ) );
				Runspace runspace = RunspaceFactory.CreateRunspace( builtInState );

				// Run the script.
				runspace.Open();
				Pipeline pipeline = runspace.CreatePipeline();
				pipeline.Commands.AddScript( GetConfigurationProperty( "LaunchPSScript", false ) ?? "" );
				pipeline.Invoke();
				runspace.Close();
			}
			else
			{
				// Launch the application by navigating to a path in the vault.
				var mfilesPath =
					clientApp.GetDriveLetter() + ":\\" +
					vaultName + "\\" +
					GetConfigurationProperty( "LaunchMFilesPath", false );

				Process.Start( "explorer.exe", string.Format( "\"{0}\"", mfilesPath ) );
			}

			return VSConstants.S_OK;
		}

		private void DeployPackage( string targetDir )
		{
			// Get the zip file name.
			string outputZip = this.project.GetOutputAssembly( this.ConfigName );

			// MPF is lazy and hardcodes ".exe" to the output assembly. We hardcode it to zip instead.
			outputZip = outputZip.Substring( 0, outputZip.Length - 4 ) + ".zip";

			// Extract the contents.
			var file = new Ionic.Zip.ZipFile( outputZip );
			file.ExtractAll( targetDir, Ionic.Zip.ExtractExistingFileAction.OverwriteSilently );
		}
	}
}
