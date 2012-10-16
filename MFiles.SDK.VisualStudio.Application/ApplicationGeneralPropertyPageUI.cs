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

		public void WriteProperties( ProjectNode project )
		{
			project.SetProjectProperty( "ApplicationName", applicationNameInput.Text );
			project.SetProjectProperty( "AssemblyName", packageNameInput.Text );
			project.SetProjectProperty( "RootNamespace", defaultNamespaceInput.Text );

			List<string> environments = new List<string>();
			if( environmentShellUiInput.Checked ) environments.Add( "ShellUI" );
			if( environmentVaultUiInput.Checked ) environments.Add( "VaultUI" );
			if( environmentVaultCoreInput.Checked ) environments.Add( "VaultCore" );
			project.SetProjectProperty( "DefaultEnvironments", string.Join( ";", environments.ToArray() ) );

			project.SetProjectProperty( "Publisher", publisherInput.Text );

			project.SetProjectProperty( "ApplicationVersion", GetVersion(
					applicationVersionMajor.Text,
					applicationVersionMinor.Text,
					applicationVersionRevision.Text,
					applicationVersionBuild.Text ) );

			project.SetProjectProperty( "MFilesVersion", GetVersion(
					mfilesVersionMajor.Text,
					mfilesVersionMinor.Text,
					mfilesVersionRevision.Text,
					mfilesVersionBuild.Text ) );

			project.SetProjectProperty( "Description", descriptionInput.Text );
			project.SetProjectProperty( "EnabledByDefault", enabledDefaultInput.Checked ? "true" : "false" );
		}

		public void ReadProperties( ProjectNode project )
		{
			applicationNameInput.Text = GetProperty( project, "ApplicationName", "M-Files Application" );
			packageNameInput.Text = GetProperty( project, "AssemblyName", "MFilesApplication" );
			defaultNamespaceInput.Text = GetProperty( project, "RootNamespace", "MFilesApplication" );

			var environments = GetProperty( project, "DefaultEnvironments", "shellui;vaultui;vaultcore" ).ToLower().Split( ';' );

			environmentShellUiInput.Checked = environments.Contains( "shellui" );
			environmentVaultUiInput.Checked = environments.Contains( "vaultui" );
			environmentVaultCoreInput.Checked = environments.Contains( "vaultcore" );

			publisherInput.Text = GetProperty( project, "Publisher", "" );

			var applicationVersion = GetVersionSegments( GetProperty( project, "ApplicationVersion", "1.0.0.0" ) );
			applicationVersionMajor.Text = applicationVersion[ 0 ];
			applicationVersionMinor.Text = applicationVersion[ 1 ];
			applicationVersionRevision.Text = applicationVersion[ 2 ];
			applicationVersionBuild.Text = applicationVersion[ 3 ];

			var mfilesVersion = GetVersionSegments( GetProperty( project, "MFilesVersion", "9.0.3372.0" ) );
			mfilesVersionMajor.Text = mfilesVersion[ 0 ];
			mfilesVersionMinor.Text = mfilesVersion[ 1 ];
			mfilesVersionRevision.Text = mfilesVersion[ 2 ];
			mfilesVersionBuild.Text = mfilesVersion[ 3 ];

			descriptionInput.Text = GetProperty( project, "Description", "An M-Files application." );
			enabledDefaultInput.Checked = GetProperty( project, "EnabledByDefault", "true" ).ToLower() != "false";
		}

		private string GetProperty( ProjectNode project, string name, string defaultValue )
		{
			return project.GetProjectProperty( name ) ?? defaultValue;
		}

		private string GetVersion( string major, string minor, string revision, string build )
		{
			return string.Format( "{0}.{1}.{2}.{3}", major, minor, revision, build );
		}

		private string[] GetVersionSegments( string version )
		{
			return version.Split( '.' );
		}

		Control IApplicationPropertyPage.Control
		{
			get { return this; }
		}
	}
}
