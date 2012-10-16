namespace MFiles.SDK.VisualStudio.Application
{
	partial class ApplicationGeneralPropertyPageUI
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.Label nameLabel;
			System.Windows.Forms.Label label1;
			System.Windows.Forms.Label label2;
			System.Windows.Forms.GroupBox groupBox1;
			System.Windows.Forms.Label label3;
			System.Windows.Forms.Label label4;
			System.Windows.Forms.Label label5;
			System.Windows.Forms.Label label6;
			System.Windows.Forms.Label label13;
			this.environmentVaultCoreInput = new System.Windows.Forms.CheckBox();
			this.environmentVaultUiInput = new System.Windows.Forms.CheckBox();
			this.environmentShellUiInput = new System.Windows.Forms.CheckBox();
			this.applicationNameInput = new System.Windows.Forms.TextBox();
			this.eventLog1 = new System.Diagnostics.EventLog();
			this.descriptionInput = new System.Windows.Forms.TextBox();
			this.applicationVersionMajor = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.applicationVersionMinor = new System.Windows.Forms.TextBox();
			this.applicationVersionRevision = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.applicationVersionBuild = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.packageNameInput = new System.Windows.Forms.TextBox();
			this.defaultNamespaceInput = new System.Windows.Forms.TextBox();
			this.mfilesVersionBuild = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.mfilesVersionRevision = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.mfilesVersionMinor = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.mfilesVersionMajor = new System.Windows.Forms.TextBox();
			this.publisherInput = new System.Windows.Forms.TextBox();
			this.enabledDefaultInput = new System.Windows.Forms.CheckBox();
			nameLabel = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			groupBox1 = new System.Windows.Forms.GroupBox();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			label13 = new System.Windows.Forms.Label();
			groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
			this.SuspendLayout();
			// 
			// nameLabel
			// 
			nameLabel.AutoSize = true;
			nameLabel.Location = new System.Drawing.Point(-3, 0);
			nameLabel.Name = "nameLabel";
			nameLabel.Size = new System.Drawing.Size(91, 13);
			nameLabel.TabIndex = 0;
			nameLabel.Text = "Application name:";
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(-3, 234);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(124, 13);
			label1.TabIndex = 2;
			label1.Text = "Minimum M-Files version:";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(-3, 44);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(82, 13);
			label2.TabIndex = 4;
			label2.Text = "Package name:";
			// 
			// groupBox1
			// 
			groupBox1.Controls.Add(label3);
			groupBox1.Controls.Add(this.environmentVaultCoreInput);
			groupBox1.Controls.Add(this.environmentVaultUiInput);
			groupBox1.Controls.Add(this.environmentShellUiInput);
			groupBox1.Location = new System.Drawing.Point(301, 3);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(281, 123);
			groupBox1.TabIndex = 10;
			groupBox1.TabStop = false;
			groupBox1.Text = "Environments";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(9, 22);
			label3.Margin = new System.Windows.Forms.Padding(6);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(261, 65);
			label3.TabIndex = 3;
			label3.Text = "Specify the environments in which the scripts defined\r\nin this project are loaded" +
    " by default.\r\n\r\nYou can change these settings for individual scripts or\r\nresourc" +
    "es in their properties.";
			// 
			// environmentVaultCoreInput
			// 
			this.environmentVaultCoreInput.AutoSize = true;
			this.environmentVaultCoreInput.Location = new System.Drawing.Point(157, 96);
			this.environmentVaultCoreInput.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
			this.environmentVaultCoreInput.Name = "environmentVaultCoreInput";
			this.environmentVaultCoreInput.Size = new System.Drawing.Size(72, 17);
			this.environmentVaultCoreInput.TabIndex = 6;
			this.environmentVaultCoreInput.Text = "VaultCore";
			this.environmentVaultCoreInput.UseVisualStyleBackColor = true;
			// 
			// environmentVaultUiInput
			// 
			this.environmentVaultUiInput.AutoSize = true;
			this.environmentVaultUiInput.Location = new System.Drawing.Point(87, 96);
			this.environmentVaultUiInput.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
			this.environmentVaultUiInput.Name = "environmentVaultUiInput";
			this.environmentVaultUiInput.Size = new System.Drawing.Size(61, 17);
			this.environmentVaultUiInput.TabIndex = 5;
			this.environmentVaultUiInput.Text = "VaultUI";
			this.environmentVaultUiInput.UseVisualStyleBackColor = true;
			// 
			// environmentShellUiInput
			// 
			this.environmentShellUiInput.AutoSize = true;
			this.environmentShellUiInput.Location = new System.Drawing.Point(9, 96);
			this.environmentShellUiInput.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
			this.environmentShellUiInput.Name = "environmentShellUiInput";
			this.environmentShellUiInput.Size = new System.Drawing.Size(60, 17);
			this.environmentShellUiInput.TabIndex = 4;
			this.environmentShellUiInput.Text = "ShellUI";
			this.environmentShellUiInput.UseVisualStyleBackColor = true;
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(298, 146);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(63, 13);
			label4.TabIndex = 7;
			label4.Text = "Description:";
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(-3, 88);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(102, 13);
			label5.TabIndex = 9;
			label5.Text = "Default namespace:";
			// 
			// label6
			// 
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(-3, 190);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(99, 13);
			label6.TabIndex = 26;
			label6.Text = "Application version:";
			// 
			// label13
			// 
			label13.AutoSize = true;
			label13.Location = new System.Drawing.Point(-3, 146);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(53, 13);
			label13.TabIndex = 36;
			label13.Text = "Publisher:";
			// 
			// applicationNameInput
			// 
			this.applicationNameInput.Location = new System.Drawing.Point(0, 16);
			this.applicationNameInput.Margin = new System.Windows.Forms.Padding(3, 3, 3, 8);
			this.applicationNameInput.Name = "applicationNameInput";
			this.applicationNameInput.Size = new System.Drawing.Size(284, 20);
			this.applicationNameInput.TabIndex = 1;
			// 
			// eventLog1
			// 
			this.eventLog1.SynchronizingObject = this;
			// 
			// descriptionInput
			// 
			this.descriptionInput.Location = new System.Drawing.Point(301, 162);
			this.descriptionInput.Multiline = true;
			this.descriptionInput.Name = "descriptionInput";
			this.descriptionInput.Size = new System.Drawing.Size(284, 84);
			this.descriptionInput.TabIndex = 16;
			// 
			// applicationVersionMajor
			// 
			this.applicationVersionMajor.Location = new System.Drawing.Point(0, 206);
			this.applicationVersionMajor.Margin = new System.Windows.Forms.Padding(3, 3, 0, 8);
			this.applicationVersionMajor.Name = "applicationVersionMajor";
			this.applicationVersionMajor.Size = new System.Drawing.Size(50, 20);
			this.applicationVersionMajor.TabIndex = 8;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(50, 209);
			this.label7.Margin = new System.Windows.Forms.Padding(0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(10, 13);
			this.label7.TabIndex = 13;
			this.label7.Text = ".";
			// 
			// applicationVersionMinor
			// 
			this.applicationVersionMinor.Location = new System.Drawing.Point(60, 206);
			this.applicationVersionMinor.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.applicationVersionMinor.Name = "applicationVersionMinor";
			this.applicationVersionMinor.Size = new System.Drawing.Size(50, 20);
			this.applicationVersionMinor.TabIndex = 9;
			// 
			// applicationVersionRevision
			// 
			this.applicationVersionRevision.Location = new System.Drawing.Point(120, 206);
			this.applicationVersionRevision.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.applicationVersionRevision.Name = "applicationVersionRevision";
			this.applicationVersionRevision.Size = new System.Drawing.Size(50, 20);
			this.applicationVersionRevision.TabIndex = 10;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(110, 209);
			this.label8.Margin = new System.Windows.Forms.Padding(0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(10, 13);
			this.label8.TabIndex = 15;
			this.label8.Text = ".";
			// 
			// applicationVersionBuild
			// 
			this.applicationVersionBuild.Location = new System.Drawing.Point(180, 206);
			this.applicationVersionBuild.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.applicationVersionBuild.Name = "applicationVersionBuild";
			this.applicationVersionBuild.Size = new System.Drawing.Size(50, 20);
			this.applicationVersionBuild.TabIndex = 11;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(170, 209);
			this.label9.Margin = new System.Windows.Forms.Padding(0);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(10, 13);
			this.label9.TabIndex = 17;
			this.label9.Text = ".";
			// 
			// packageNameInput
			// 
			this.packageNameInput.Location = new System.Drawing.Point(0, 60);
			this.packageNameInput.Margin = new System.Windows.Forms.Padding(3, 3, 3, 8);
			this.packageNameInput.Name = "packageNameInput";
			this.packageNameInput.Size = new System.Drawing.Size(284, 20);
			this.packageNameInput.TabIndex = 2;
			// 
			// defaultNamespaceInput
			// 
			this.defaultNamespaceInput.Location = new System.Drawing.Point(0, 104);
			this.defaultNamespaceInput.Margin = new System.Windows.Forms.Padding(3, 3, 3, 8);
			this.defaultNamespaceInput.Name = "defaultNamespaceInput";
			this.defaultNamespaceInput.Size = new System.Drawing.Size(284, 20);
			this.defaultNamespaceInput.TabIndex = 3;
			// 
			// mfilesVersionBuild
			// 
			this.mfilesVersionBuild.Location = new System.Drawing.Point(180, 250);
			this.mfilesVersionBuild.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.mfilesVersionBuild.Name = "mfilesVersionBuild";
			this.mfilesVersionBuild.Size = new System.Drawing.Size(50, 20);
			this.mfilesVersionBuild.TabIndex = 15;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(170, 253);
			this.label10.Margin = new System.Windows.Forms.Padding(0);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(10, 13);
			this.label10.TabIndex = 34;
			this.label10.Text = ".";
			// 
			// mfilesVersionRevision
			// 
			this.mfilesVersionRevision.Location = new System.Drawing.Point(120, 250);
			this.mfilesVersionRevision.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.mfilesVersionRevision.Name = "mfilesVersionRevision";
			this.mfilesVersionRevision.Size = new System.Drawing.Size(50, 20);
			this.mfilesVersionRevision.TabIndex = 14;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(110, 253);
			this.label11.Margin = new System.Windows.Forms.Padding(0);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(10, 13);
			this.label11.TabIndex = 32;
			this.label11.Text = ".";
			// 
			// mfilesVersionMinor
			// 
			this.mfilesVersionMinor.Location = new System.Drawing.Point(60, 250);
			this.mfilesVersionMinor.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.mfilesVersionMinor.Name = "mfilesVersionMinor";
			this.mfilesVersionMinor.Size = new System.Drawing.Size(50, 20);
			this.mfilesVersionMinor.TabIndex = 13;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(50, 253);
			this.label12.Margin = new System.Windows.Forms.Padding(0);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(10, 13);
			this.label12.TabIndex = 30;
			this.label12.Text = ".";
			// 
			// mfilesVersionMajor
			// 
			this.mfilesVersionMajor.Location = new System.Drawing.Point(0, 250);
			this.mfilesVersionMajor.Margin = new System.Windows.Forms.Padding(3, 3, 0, 8);
			this.mfilesVersionMajor.Name = "mfilesVersionMajor";
			this.mfilesVersionMajor.Size = new System.Drawing.Size(50, 20);
			this.mfilesVersionMajor.TabIndex = 12;
			// 
			// publisherInput
			// 
			this.publisherInput.Location = new System.Drawing.Point(0, 162);
			this.publisherInput.Margin = new System.Windows.Forms.Padding(3, 3, 3, 8);
			this.publisherInput.Name = "publisherInput";
			this.publisherInput.Size = new System.Drawing.Size(284, 20);
			this.publisherInput.TabIndex = 7;
			// 
			// enabledDefaultInput
			// 
			this.enabledDefaultInput.AutoSize = true;
			this.enabledDefaultInput.Location = new System.Drawing.Point(301, 252);
			this.enabledDefaultInput.Name = "enabledDefaultInput";
			this.enabledDefaultInput.Size = new System.Drawing.Size(114, 17);
			this.enabledDefaultInput.TabIndex = 17;
			this.enabledDefaultInput.Text = "Enabled by default";
			this.enabledDefaultInput.UseVisualStyleBackColor = true;
			// 
			// ApplicationGeneralPropertyPageUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.enabledDefaultInput);
			this.Controls.Add(this.publisherInput);
			this.Controls.Add(label13);
			this.Controls.Add(this.mfilesVersionBuild);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.mfilesVersionRevision);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.mfilesVersionMinor);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.mfilesVersionMajor);
			this.Controls.Add(this.defaultNamespaceInput);
			this.Controls.Add(this.packageNameInput);
			this.Controls.Add(label6);
			this.Controls.Add(nameLabel);
			this.Controls.Add(this.applicationNameInput);
			this.Controls.Add(this.applicationVersionBuild);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.applicationVersionRevision);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.applicationVersionMinor);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.applicationVersionMajor);
			this.Controls.Add(label5);
			this.Controls.Add(this.descriptionInput);
			this.Controls.Add(label4);
			this.Controls.Add(groupBox1);
			this.Controls.Add(label2);
			this.Controls.Add(label1);
			this.Name = "ApplicationGeneralPropertyPageUI";
			this.Size = new System.Drawing.Size(589, 275);
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox applicationNameInput;
		private System.Diagnostics.EventLog eventLog1;
		private System.Windows.Forms.CheckBox environmentVaultCoreInput;
		private System.Windows.Forms.CheckBox environmentVaultUiInput;
		private System.Windows.Forms.CheckBox environmentShellUiInput;
		private System.Windows.Forms.TextBox descriptionInput;
		private System.Windows.Forms.TextBox applicationVersionBuild;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox applicationVersionRevision;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox applicationVersionMinor;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox applicationVersionMajor;
		private System.Windows.Forms.CheckBox enabledDefaultInput;
		private System.Windows.Forms.TextBox publisherInput;
		private System.Windows.Forms.TextBox mfilesVersionBuild;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox mfilesVersionRevision;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox mfilesVersionMinor;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox mfilesVersionMajor;
		private System.Windows.Forms.TextBox defaultNamespaceInput;
		private System.Windows.Forms.TextBox packageNameInput;
	}
}
