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
			System.Windows.Forms.Label nameLabel;
			System.Windows.Forms.Label label1;
			System.Windows.Forms.Label label2;
			this.deployTarget = new System.Windows.Forms.TextBox();
			this.testVault = new System.Windows.Forms.TextBox();
			this.testPath = new System.Windows.Forms.TextBox();
			nameLabel = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// nameLabel
			// 
			nameLabel.AutoSize = true;
			nameLabel.Location = new System.Drawing.Point(3, 0);
			nameLabel.Name = "nameLabel";
			nameLabel.Size = new System.Drawing.Size(73, 13);
			nameLabel.TabIndex = 1;
			nameLabel.Text = "Deploy target:";
			// 
			// deployTarget
			// 
			this.deployTarget.Location = new System.Drawing.Point(6, 16);
			this.deployTarget.Margin = new System.Windows.Forms.Padding(3, 3, 3, 8);
			this.deployTarget.Name = "deployTarget";
			this.deployTarget.Size = new System.Drawing.Size(284, 20);
			this.deployTarget.TabIndex = 2;
			// 
			// testVault
			// 
			this.testVault.Location = new System.Drawing.Point(6, 60);
			this.testVault.Margin = new System.Windows.Forms.Padding(3, 3, 3, 8);
			this.testVault.Name = "testVault";
			this.testVault.Size = new System.Drawing.Size(284, 20);
			this.testVault.TabIndex = 4;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(3, 44);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(57, 13);
			label1.TabIndex = 3;
			label1.Text = "Test vault:";
			// 
			// testPath
			// 
			this.testPath.Location = new System.Drawing.Point(6, 104);
			this.testPath.Margin = new System.Windows.Forms.Padding(3, 3, 3, 8);
			this.testPath.Name = "testPath";
			this.testPath.Size = new System.Drawing.Size(284, 20);
			this.testPath.TabIndex = 6;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(3, 88);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(55, 13);
			label2.TabIndex = 5;
			label2.Text = "Test path:";
			// 
			// ApplicationDebugPropertyPageUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.testPath);
			this.Controls.Add(label2);
			this.Controls.Add(this.testVault);
			this.Controls.Add(label1);
			this.Controls.Add(this.deployTarget);
			this.Controls.Add(nameLabel);
			this.Name = "ApplicationDebugPropertyPageUI";
			this.Size = new System.Drawing.Size(803, 497);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox deployTarget;
		private System.Windows.Forms.TextBox testVault;
		private System.Windows.Forms.TextBox testPath;

	}
}
