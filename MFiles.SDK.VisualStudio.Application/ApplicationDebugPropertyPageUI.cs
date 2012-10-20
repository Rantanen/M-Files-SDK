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

			// Update control enabled state when radio buttons change.
			launchMFilesInput.CheckedChanged += ( s, a ) => UpdateControlStates();
			launchPowerShellInput.CheckedChanged += ( s, a ) => UpdateControlStates();
			UpdateControlStates();

			// Resolve the current API version and display it in the label.
			clientApp = new MFilesAPI.MFilesClientApplication();
			apiVersion = clientApp.GetAPIVersion().Display;
			apiLabel.Text = string.Format( apiLabel.Text, apiVersion );
		}

		/// <summary>
		/// Return true if the page is dirty.
		/// </summary>
		public bool IsDirty { get; protected set; }

		/// <summary>
		/// Write the properties to the configuration.
		/// </summary>
		/// <param name="properties">Project property proxy</param>
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

			IsDirty = false;
		}

		/// <summary>
		/// Read the properties from the configuration.
		/// </summary>
		/// <param name="properties">Project property proxy</param>
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
			IsDirty = false;
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
		/// Refresh the Target Vaults list.
		/// </summary>
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

		/// <summary>
		/// Select a path within a vault.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void browsePathButton_Click( object sender, EventArgs e )
		{
			// TODO: Change this to the Vista-styled select folder dialog
			// This can be used through P/Invoke or www.ookii.org/software/dialogs.
			
			// Display the folder browser dialog to the user.
			FolderBrowserDialog fbd = new FolderBrowserDialog();
			fbd.ShowNewFolderButton = false;
			fbd.RootFolder = Environment.SpecialFolder.MyComputer;
			fbd.ShowDialog();

			// Make sure the path is within the vault.
			var vaultRoot = clientApp.GetDriveLetter() + ":\\" + vaultInput.Text + "\\";
			if( !fbd.SelectedPath.StartsWith( vaultRoot ) )
			{
				MessageBox.Show( "Selected path is not in the vault." );
				return;
			}

			// Save the path.
			mfilesPathInput.Text = fbd.SelectedPath.Substring( vaultRoot.Length );
		}
	}
}
