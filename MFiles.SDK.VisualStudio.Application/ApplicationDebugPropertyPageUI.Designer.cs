namespace MFiles.SDK.VisualStudio.Application
{
	partial class ApplicationDebugPropertyPageUI
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
			System.Windows.Forms.Label label1;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationDebugPropertyPageUI));
			System.Windows.Forms.Label label5;
			System.Windows.Forms.Label label2;
			this.powerShellScriptInput = new System.Windows.Forms.TextBox();
			this.mfilesPathInput = new System.Windows.Forms.TextBox();
			this.apiLabel = new System.Windows.Forms.Label();
			this.vaultInput = new System.Windows.Forms.ComboBox();
			this.refreshButton = new System.Windows.Forms.Button();
			this.launchMFilesInput = new System.Windows.Forms.RadioButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.launchPowerShellInput = new System.Windows.Forms.RadioButton();
			this.browsePathButton = new System.Windows.Forms.Button();
			this.outputPath = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(-3, 37);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(67, 13);
			label1.TabIndex = 3;
			label1.Text = "Target vault:";
			// 
			// powerShellScriptInput
			// 
			this.powerShellScriptInput.Location = new System.Drawing.Point(121, 56);
			this.powerShellScriptInput.Margin = new System.Windows.Forms.Padding(3, 3, 3, 8);
			this.powerShellScriptInput.Multiline = true;
			this.powerShellScriptInput.Name = "powerShellScriptInput";
			this.powerShellScriptInput.Size = new System.Drawing.Size(489, 132);
			this.powerShellScriptInput.TabIndex = 9;
			this.powerShellScriptInput.Text = "$app = new-object -Com MFilesAPI.MFilesClientApplication";
			// 
			// mfilesPathInput
			// 
			this.mfilesPathInput.Location = new System.Drawing.Point(171, 23);
			this.mfilesPathInput.Margin = new System.Windows.Forms.Padding(0, 3, 3, 8);
			this.mfilesPathInput.Name = "mfilesPathInput";
			this.mfilesPathInput.Size = new System.Drawing.Size(404, 20);
			this.mfilesPathInput.TabIndex = 6;
			// 
			// apiLabel
			// 
			this.apiLabel.AutoSize = true;
			this.apiLabel.Location = new System.Drawing.Point(141, 63);
			this.apiLabel.Name = "apiLabel";
			this.apiLabel.Size = new System.Drawing.Size(429, 52);
			this.apiLabel.TabIndex = 9;
			this.apiLabel.Text = resources.GetString("apiLabel.Text");
			// 
			// vaultInput
			// 
			this.vaultInput.FormattingEnabled = true;
			this.vaultInput.Location = new System.Drawing.Point(134, 34);
			this.vaultInput.Margin = new System.Windows.Forms.Padding(3, 3, 3, 8);
			this.vaultInput.Name = "vaultInput";
			this.vaultInput.Size = new System.Drawing.Size(203, 21);
			this.vaultInput.TabIndex = 3;
			// 
			// refreshButton
			// 
			this.refreshButton.Location = new System.Drawing.Point(343, 32);
			this.refreshButton.Name = "refreshButton";
			this.refreshButton.Size = new System.Drawing.Size(75, 23);
			this.refreshButton.TabIndex = 4;
			this.refreshButton.Text = "Refresh";
			this.refreshButton.UseVisualStyleBackColor = true;
			this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
			// 
			// launchMFilesInput
			// 
			this.launchMFilesInput.AutoSize = true;
			this.launchMFilesInput.Location = new System.Drawing.Point(11, 24);
			this.launchMFilesInput.Name = "launchMFilesInput";
			this.launchMFilesInput.Size = new System.Drawing.Size(82, 17);
			this.launchMFilesInput.TabIndex = 5;
			this.launchMFilesInput.TabStop = true;
			this.launchMFilesInput.Text = "M-Files path";
			this.launchMFilesInput.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.browsePathButton);
			this.groupBox1.Controls.Add(this.launchPowerShellInput);
			this.groupBox1.Controls.Add(label5);
			this.groupBox1.Controls.Add(this.launchMFilesInput);
			this.groupBox1.Controls.Add(this.mfilesPathInput);
			this.groupBox1.Controls.Add(this.powerShellScriptInput);
			this.groupBox1.Location = new System.Drawing.Point(0, 140);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(8);
			this.groupBox1.Size = new System.Drawing.Size(621, 204);
			this.groupBox1.TabIndex = 14;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Launch options";
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(118, 26);
			label5.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(53, 13);
			label5.TabIndex = 14;
			label5.Text = "M:\\Vault\\";
			// 
			// launchPowerShellInput
			// 
			this.launchPowerShellInput.AutoSize = true;
			this.launchPowerShellInput.Location = new System.Drawing.Point(11, 56);
			this.launchPowerShellInput.Name = "launchPowerShellInput";
			this.launchPowerShellInput.Size = new System.Drawing.Size(106, 17);
			this.launchPowerShellInput.TabIndex = 8;
			this.launchPowerShellInput.TabStop = true;
			this.launchPowerShellInput.Text = "PowerShell script";
			this.launchPowerShellInput.UseVisualStyleBackColor = true;
			// 
			// browsePathButton
			// 
			this.browsePathButton.Location = new System.Drawing.Point(581, 21);
			this.browsePathButton.Name = "browsePathButton";
			this.browsePathButton.Size = new System.Drawing.Size(29, 23);
			this.browsePathButton.TabIndex = 7;
			this.browsePathButton.Text = "...";
			this.browsePathButton.UseVisualStyleBackColor = true;
			this.browsePathButton.Click += new System.EventHandler(this.browsePathButton_Click);
			// 
			// outputPath
			// 
			this.outputPath.Location = new System.Drawing.Point(134, 1);
			this.outputPath.Margin = new System.Windows.Forms.Padding(0, 3, 3, 8);
			this.outputPath.Name = "outputPath";
			this.outputPath.Size = new System.Drawing.Size(203, 20);
			this.outputPath.TabIndex = 1;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(-3, 4);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(66, 13);
			label2.TabIndex = 16;
			label2.Text = "Output path:";
			// 
			// ApplicationDebugPropertyPageUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(label2);
			this.Controls.Add(this.outputPath);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.refreshButton);
			this.Controls.Add(this.vaultInput);
			this.Controls.Add(this.apiLabel);
			this.Controls.Add(label1);
			this.Name = "ApplicationDebugPropertyPageUI";
			this.Size = new System.Drawing.Size(623, 346);
			this.Load += new System.EventHandler(this.ApplicationDebugPropertyPageUI_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox powerShellScriptInput;
		private System.Windows.Forms.TextBox mfilesPathInput;
		private System.Windows.Forms.ComboBox vaultInput;
		private System.Windows.Forms.Button refreshButton;
		private System.Windows.Forms.RadioButton launchMFilesInput;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button browsePathButton;
		private System.Windows.Forms.RadioButton launchPowerShellInput;
		private System.Windows.Forms.Label apiLabel;
		private System.Windows.Forms.TextBox outputPath;

	}
}
