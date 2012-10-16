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

namespace MFiles.SDK.VisualStudio.Application
{
	public partial class ApplicationDebugPropertyPageUI : UserControl, IApplicationPropertyPage
	{
		public ApplicationDebugPropertyPage Page { get; protected set; }

		public ApplicationDebugPropertyPageUI( ApplicationDebugPropertyPage page )
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
			project.SetProjectProperty( "DeployTarget", deployTarget.Text );
			project.SetProjectProperty( "TestVault", testVault.Text );
			project.SetProjectProperty( "TestPath", testPath.Text );

		}

		public void ReadProperties( ProjectNode project )
		{
			deployTarget.Text = project.GetProjectProperty( "DeployTarget" );
			testVault.Text = project.GetProjectProperty( "TestVault" );
			testPath.Text = project.GetProjectProperty( "TestPath" );
		}

		private string GetProperty( ProjectNode project, string name, string defaultValue )
		{
			return project.GetProjectProperty( name ) ?? defaultValue;
		}

		Control IApplicationPropertyPage.Control
		{
			get { return this; }
		}
	}
}
