using System;

namespace NetworkJitter
{
	partial class MainForm
	{
		private System.ComponentModel.IContainer components = null;

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			AppController.Instance.DisposePowerShell();
			base.Dispose(disposing);
		}

		private void MainForm_Load(object sender, EventArgs e) {}

		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(457, 165);
			this.Name = "MainForm";
			this.Text = "NetworkJitter";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);


		}
	}
}

