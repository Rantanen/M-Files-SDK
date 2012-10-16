using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualStudio.Project;
using Microsoft.Win32;
using System.IO;
using System.Reflection;

namespace MFiles.SDK.VisualStudio.Application
{
	public partial class ApplicationDebugPropertyPageUI : UserControl, IApplicationPropertyPage
	{
		public ApplicationDebugPropertyPage Page { get; protected set; }
		Dictionary<string, string> MFilesInstallDirs = new Dictionary<string, string>();
		string apiVersion = null;
		MFilesAPI.MFilesClientApplication clientApp;

		public ApplicationDebugPropertyPageUI( ApplicationDebugPropertyPage page )
		{
			InitializeComponent();
			this.Page = page;

			// If values change mark this as dirty.

			outputPath.TextChanged += ( s, a ) => this.IsDirty = true;
			vaultInput.SelectedIndexChanged += ( s, a ) => this.IsDirty = true;
			launchMFilesInput.CheckedChanged += ( s, a ) => this.IsDirty = true;
			launchPowerShellInput.CheckedChanged += ( s, a ) => this.IsDirty = true;
			mfilesPathInput.TextChanged += ( s, a ) => this.IsDirty = true;
			powerShellScriptInput.TextChanged += ( s, a ) => this.IsDirty = true;

			launchMFilesInput.CheckedChanged += ( s, a ) => UpdateControlStates();
			launchPowerShellInput.CheckedChanged += ( s, a ) => UpdateControlStates();
			UpdateControlStates();

			// Resolve the current API version and display it in the label.
			clientApp = new MFilesAPI.MFilesClientApplication();
			apiVersion = clientApp.GetAPIVersion().Display;
			apiLabel.Text = string.Format( apiLabel.Text, apiVersion );
		}

		public bool IsDirty { get; protected set; }

		public void WriteProperties( ProjectProperties properties )
		{
			properties.SetConfigProperty( "OutputPath", outputPath.Text );
			properties.SetConfigProperty( "TestVault", (string)this.vaultInput.Text );

			if( launchMFilesInput.Checked )
				properties.SetConfigProperty( "LaunchMode", "MFiles" );
			else if( launchPowerShellInput.Checked )
				properties.SetConfigProperty( "LaunchMode", "PowerShell" );

			properties.SetConfigProperty( "LaunchMFilesPath", mfilesPathInput.Text );
			properties.SetConfigProperty( "LaunchPSScript", powerShellScriptInput.Text );
		}

		public void ReadProperties( ProjectProperties properties )
		{
			outputPath.Text = properties.GetConfigProperty( "OutputPath" );

			RefreshVaults();
			vaultInput.Text = properties.GetConfigProperty( "TestVault" );

			var launchMode = (properties.GetConfigProperty( "LaunchMode" ) ?? "").ToLowerInvariant();
			if( launchMode == "powershell" )
				launchPowerShellInput.Checked = true;
			else
				launchMFilesInput.Checked = true;

			mfilesPathInput.Text = properties.GetConfigProperty( "LaunchMFilesPath" );
			var psScript = properties.GetConfigProperty( "LaunchPSScript" );
			if( psScript != null ) powerShellScriptInput.Text = psScript;

			UpdateControlStates();
		}

		private string GetProperty( ProjectNode project, string name, string defaultValue )
		{
			return project.GetProjectProperty( name ) ?? defaultValue;
		}

		public Control Control
		{
			get { return this; }
		}

		private void ApplicationDebugPropertyPageUI_Load( object sender, EventArgs e )
		{
			RefreshVaults();
		}

		private void refreshButton_Click( object sender, EventArgs e )
		{
			RefreshVaults();
		}

		/// <summary>
		/// Currently not used. Waits for the proper API loading support.
		/// </summary>
		// private void RefreshMFilesVersions()
		// {
		// 	// Get the currently loaded M-Files API.

		// 	// We'll do this in its own AppDomain so we can unload the API by
		// 	// unloading the AppDomain.
		// 	string loadedApi;
		// 	using( var scope = new AppDomainScope<ClientMethods>() )
		// 	{
		// 		// Delegate the API version query to the worker.
		// 		loadedApi = scope.Object.GetLoadedAPIVersion();
		// 	}

		// 	var hklm = Registry.LocalMachine;
		// 	var mfiles = hklm.OpenSubKey( @"Software\Motive\M-Files" );
		// 	var versions = mfiles.GetSubKeyNames();

		// 	mfilesVersionInput.Items.Clear();
		// 	MFilesInstallDirs.Clear();
		// 	foreach( var v in versions )
		// 	{
		// 		var versionKey = mfiles.OpenSubKey( v );
		// 		var installDir = (string)versionKey.GetValue( "InstallDir" );
		// 		versionKey.Close();

		// 		var apiPath = Path.Combine( installDir, @"Common\MFilesAPI.dll" );

		// 		if( !File.Exists( apiPath ) )
		// 			continue;

		// 		string title = v;
		// 		if( loadedApi == v )
		// 			title += " (Loaded)";

		// 		mfilesVersionInput.Items.Add(new { Text= title, Value= v });
		// 		MFilesInstallDirs[v] = installDir;
		// 	}

		// 	// Close the open keys.
		// 	mfiles.Close();
		// 	hklm.Close();
		// }

		private void RefreshVaults()
		{
			// Get the available vaults.
			// We'll do this in its own AppDomain so we can unload the API by
			// unloading the AppDomain.
			var vaults = clientApp.GetVaultConnections();

			var selectedText = vaultInput.Text;
			vaultInput.Items.Clear();
			foreach( MFilesAPI.VaultConnection v in vaults ) 
				vaultInput.Items.Add( v.Name );
			vaultInput.Text = selectedText;
		}

		/// <summary>
		/// Retrieve the vaults set up in the current M-Files Client.
		/// </summary>
		/// <returns>List of vault connection names.</returns>
		public List<string> GetVaults()
		{
			if( clientApp == null ) return null;

			var result = new List<string>();

			var vaults = clientApp.GetVaultConnections();
			foreach( MFilesAPI.Vault v in vaults ) { result.Add( v.Name ); }

			return result;
		}

		private void UpdateControlStates()
		{
			mfilesPathInput.Enabled = launchMFilesInput.Checked;
			browsePathButton.Enabled = launchMFilesInput.Checked;
			powerShellScriptInput.Enabled = launchPowerShellInput.Checked;
		}

		private void browsePathButton_Click( object sender, EventArgs e )
		{
			var startupFolder = clientApp.GetDriveLetter() + ":\\" + vaultInput.Text + "\\";
			FolderBrowserDialog fbd = new FolderBrowserDialog();
			fbd.ShowNewFolderButton = false;
			fbd.RootFolder = Environment.SpecialFolder.MyComputer;

			fbd.ShowDialog();

			if( !fbd.SelectedPath.StartsWith( startupFolder ) )
			{
				MessageBox.Show( "Selected path is not in the vault." );
				return;
			}

			mfilesPathInput.Text = fbd.SelectedPath.Substring( startupFolder.Length );
		}
	}
}
