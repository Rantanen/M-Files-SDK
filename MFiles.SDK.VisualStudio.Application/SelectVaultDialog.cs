using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MFiles.SDK.VisualStudio.Application
{
	public partial class SelectVaultDialog : Form
	{
		public SelectVaultDialog()
		{
			InitializeComponent();
		}

		private void SelectVaultDialog_Load( object sender, EventArgs e )
		{
			var app = new MFilesAPI.MFilesClientApplication();
			var vaults = app.GetVaultConnections();
			foreach( MFilesAPI.VaultConnection vault in vaults )
			{
				vaultList.Items.Add( vault.Name );
			}
			vaultList.SelectedIndex = 0;
		}

		public DialogResult Result { get; protected set; }
		public string VaultName { get; protected set; }
		public bool SetDefault { get; protected set; }

		private void okButton_Click( object sender, EventArgs e )
		{
			VaultName = (string)vaultList.SelectedItem;
			SetDefault = defaultCheckbox.Checked;
			Result = System.Windows.Forms.DialogResult.OK;
			this.Close();
		}

		private void cancelButton_Click( object sender, EventArgs e )
		{
			Result = System.Windows.Forms.DialogResult.Cancel;
			this.Close();
		}
	}
}
