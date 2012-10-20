namespace MFiles.SDK.VisualStudio.Application
{
	partial class SelectVaultDialog
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if( disposing && ( components != null ) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.vaultList = new System.Windows.Forms.ComboBox();
			this.defaultCheckbox = new System.Windows.Forms.CheckBox();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(191, 39);
			this.label1.TabIndex = 0;
			this.label1.Text = "Could not find {0}.\r\n\r\nSelect new target vault for deployment:";
			// 
			// vaultList
			// 
			this.vaultList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.vaultList.FormattingEnabled = true;
			this.vaultList.Location = new System.Drawing.Point(15, 51);
			this.vaultList.Name = "vaultList";
			this.vaultList.Size = new System.Drawing.Size(292, 21);
			this.vaultList.TabIndex = 1;
			// 
			// defaultCheckbox
			// 
			this.defaultCheckbox.AutoSize = true;
			this.defaultCheckbox.Location = new System.Drawing.Point(15, 78);
			this.defaultCheckbox.Name = "defaultCheckbox";
			this.defaultCheckbox.Size = new System.Drawing.Size(100, 17);
			this.defaultCheckbox.TabIndex = 2;
			this.defaultCheckbox.Text = "Save as default";
			this.defaultCheckbox.UseVisualStyleBackColor = true;
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(151, 100);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 3;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(232, 100);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 4;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// SelectVaultDialog
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(319, 135);
			this.ControlBox = false;
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.defaultCheckbox);
			this.Controls.Add(this.vaultList);
			this.Controls.Add(this.label1);
			this.Name = "SelectVaultDialog";
			this.Text = "Vault Not Found";
			this.Load += new System.EventHandler(this.SelectVaultDialog_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox vaultList;
		private System.Windows.Forms.CheckBox defaultCheckbox;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
	}
}