using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio;
using System.Runtime.InteropServices;

namespace MFiles.SDK.VisualStudio.Application
{
	public partial class ApplicationGeneralPropertyPageUI : UserControl , IApplicationPropertyPage
	{
		public ApplicationGeneralPropertyPage Page { get; protected set; }

		public ApplicationGeneralPropertyPageUI( ApplicationGeneralPropertyPage page )
		{
			InitializeComponent();
			this.Page = page;

			// If values change mark this as dirty.

			foreach( var tb in this.Controls.OfType<TextBox>() )
				tb.TextChanged += ( s, a ) => this.IsDirty = true;

			foreach( var cb in this.Controls.OfType<CheckBox>() )
				cb.CheckedChanged += ( s, a ) => this.IsDirty = true;
		}

		public bool IsDirty { get; protected set; }

		public void WriteProperties( ProjectProperties properties )
		{
			properties.SetProperty( "ApplicationName", applicationNameInput.Text );
			properties.SetProperty( "AssemblyName", packageNameInput.Text );
			properties.SetProperty( "RootNamespace", defaultNamespaceInput.Text );

			List<string> environments = new List<string>();
			if( environmentShellUiInput.Checked ) environments.Add( "ShellUI" );
			if( environmentVaultUiInput.Checked ) environments.Add( "VaultUI" );
			if( environmentVaultCoreInput.Checked ) environments.Add( "VaultCore" );
			properties.SetProperty( "DefaultEnvironments", string.Join( ";", environments.ToArray() ) );

			properties.SetProperty( "Publisher", publisherInput.Text );

			properties.SetProperty( "ApplicationVersion", GetVersion(
					applicationVersionMajor.Text,
					applicationVersionMinor.Text,
					applicationVersionRevision.Text,
					applicationVersionBuild.Text ) );

			properties.SetProperty( "MFilesVersion", GetVersion(
					mfilesVersionMajor.Text,
					mfilesVersionMinor.Text,
					mfilesVersionRevision.Text,
					mfilesVersionBuild.Text ) );

			properties.SetProperty( "Description", descriptionInput.Text );
			properties.SetProperty( "EnabledByDefault", enabledDefaultInput.Checked ? "true" : "false" );
		}

		public void ReadProperties( ProjectProperties properties )
		{
			applicationNameInput.Text = GetProperty( properties, "ApplicationName", "M-Files Application" );
			packageNameInput.Text = GetProperty( properties, "AssemblyName", "MFilesApplication" );
			defaultNamespaceInput.Text = GetProperty( properties, "RootNamespace", "MFilesApplication" );

			var environments = GetProperty( properties, "DefaultEnvironments", "shellui;vaultui;vaultcore" ).ToLower().Split( ';' );

			environmentShellUiInput.Checked = environments.Contains( "shellui" );
			environmentVaultUiInput.Checked = environments.Contains( "vaultui" );
			environmentVaultCoreInput.Checked = environments.Contains( "vaultcore" );

			publisherInput.Text = GetProperty( properties, "Publisher", "" );

			var applicationVersion = GetVersionSegments( GetProperty( properties, "ApplicationVersion", "1.0.0.0" ) );
			applicationVersionMajor.Text = applicationVersion[ 0 ];
			applicationVersionMinor.Text = applicationVersion[ 1 ];
			applicationVersionRevision.Text = applicationVersion[ 2 ];
			applicationVersionBuild.Text = applicationVersion[ 3 ];

			var mfilesVersion = GetVersionSegments( GetProperty( properties, "MFilesVersion", "9.0.3372.0" ) );
			mfilesVersionMajor.Text = mfilesVersion[ 0 ];
			mfilesVersionMinor.Text = mfilesVersion[ 1 ];
			mfilesVersionRevision.Text = mfilesVersion[ 2 ];
			mfilesVersionBuild.Text = mfilesVersion[ 3 ];

			descriptionInput.Text = GetProperty( properties, "Description", "An M-Files application." );
			enabledDefaultInput.Checked = GetProperty( properties, "EnabledByDefault", "true" ).ToLower() != "false";
		}

		private string GetProperty( ProjectProperties properties, string name, string defaultValue )
		{
			return properties.GetProperty( name ) ?? defaultValue;
		}

		private string GetVersion( string major, string minor, string revision, string build )
		{
			return string.Format( "{0}.{1}.{2}.{3}", major, minor, revision, build );
		}

		private string[] GetVersionSegments( string version )
		{
			return version.Split( '.' );
		}

		public Control Control
		{
			get { return this; }
		}
	}
}
