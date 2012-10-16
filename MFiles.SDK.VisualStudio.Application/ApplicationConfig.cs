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

			var clientApp = new MFilesAPI.MFilesClientApplication();
			var apiVersion = clientApp.GetAPIVersion().Display;
			MFilesAPI.Vault vault = null;

			var vaultName = GetConfigurationProperty( "TestVault", true );
			MFilesAPI.VaultConnection vaultConnection;
			try
			{
				vaultConnection = clientApp.GetVaultConnection( vaultName );
			}
			catch
			{
				throw new Exception( "The document vault '" + vaultName + "' was not found." );
			}
			string vaultGuid = vaultConnection.GetGUID();

			// Get the M-Files install directory from the registry.
			var hklm64 = RegistryKey.OpenBaseKey( RegistryHive.LocalMachine, RegistryView.Registry64 );
			var mfKey = hklm64.OpenSubKey( @"Software\Motive\M-Files\" + apiVersion );
			var installDir = (string)mfKey.GetValue( "InstallDir" );
			mfKey.Close();
			hklm64.Close();

			// Log out to free the current application.
			try
			{
				vault = clientApp.BindToVault( vaultName, IntPtr.Zero, false, true );
				if( vault != null ) vault.LogOutWithDialogs( IntPtr.Zero );
			}
			catch { }

			// Deploy the application.
			var relativePath = string.Format( @"Client\Apps\{0}\sysapps\{1}",
				vaultGuid, this.project.GetProjectProperty( "Name" ) ?? "unnamed" );
			var targetDir = Path.Combine( installDir, relativePath );

			// If the directory exists, remove it so there's no residue files left.
			if( Directory.Exists( targetDir ) ) { Directory.Delete( targetDir, true ); }
			Directory.CreateDirectory( targetDir );

			// Extract the Zip contents to the target directory.
			var outputZip = this.project.GetOutputAssembly( this.ConfigName );

			// MPF is lazy and hardcodes ".exe" to the output assembly. We hardcode it to zip instead.
			outputZip = outputZip.Substring( 0, outputZip.Length - 4 ) + ".zip";
			
			var file = new Ionic.Zip.ZipFile( outputZip );
			file.ExtractAll( targetDir, Ionic.Zip.ExtractExistingFileAction.OverwriteSilently );

			var launchMode = ( GetConfigurationProperty( "LaunchMode", false ) ?? "" ).ToLowerInvariant();
			vault = clientApp.BindToVault( vaultName, IntPtr.Zero, true, false );
			if( launchMode == "powershell" )
			{
				var builtInState = InitialSessionState.CreateDefault();
				builtInState.Variables.Add( new SessionStateVariableEntry(
					"vaultName", vaultName, "Name of the vault used for testing." ) );
				builtInState.Variables.Add( new SessionStateVariableEntry(
					"vault", vault, "M-Files Vault" ) );
				Runspace runspace = RunspaceFactory.CreateRunspace( builtInState );
				runspace.Open();
				Pipeline pipeline = runspace.CreatePipeline();
				pipeline.Commands.AddScript( GetConfigurationProperty( "LaunchPSScript", false ) ?? "" );
				pipeline.Invoke();
				runspace.Close();
			}
			else
			{
				var mfilesPath =
					clientApp.GetDriveLetter() + ":\\" +
					vaultName + "\\" +
					GetConfigurationProperty( "LaunchMFilesPath", false );

				try
				{
					// We need to log in first - otherwise explorer.exe can't find the folders.
					Process.Start( "explorer.exe", string.Format( "\"{0}\"", mfilesPath ) );
				}
				catch { }
			}

			return VSConstants.S_OK;
		}
	}
}
