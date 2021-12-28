namespace WBPlugin
{
  partial class Report
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.Okay = new System.Windows.Forms.Button();
        this.CopyToClipboard = new System.Windows.Forms.Button();
        this.ReportContents = new System.Windows.Forms.TextBox();
        this.SuspendLayout();
        // 
        // Okay
        // 
        this.Okay.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
        this.Okay.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.Okay.Location = new System.Drawing.Point(75, 336);
        this.Okay.Name = "Okay";
        this.Okay.Size = new System.Drawing.Size(67, 23);
        this.Okay.TabIndex = 0;
        this.Okay.Text = "OK";
        this.Okay.UseVisualStyleBackColor = true;
        // 
        // CopyToClipboard
        // 
        this.CopyToClipboard.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
        this.CopyToClipboard.Location = new System.Drawing.Point(148, 336);
        this.CopyToClipboard.Name = "CopyToClipboard";
        this.CopyToClipboard.Size = new System.Drawing.Size(111, 23);
        this.CopyToClipboard.TabIndex = 1;
        this.CopyToClipboard.Text = "Copy to Clipboard";
        this.CopyToClipboard.UseVisualStyleBackColor = true;
        this.CopyToClipboard.Click += new System.EventHandler(this.CopyToClipboard_Click);
        // 
        // ReportContents
        // 
        this.ReportContents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.ReportContents.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.ReportContents.Location = new System.Drawing.Point(13, 11);
        this.ReportContents.Multiline = true;
        this.ReportContents.Name = "ReportContents";
        this.ReportContents.Size = new System.Drawing.Size(306, 314);
        this.ReportContents.TabIndex = 2;
        // 
        // Report
        // 
        this.AcceptButton = this.Okay;
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.CancelButton = this.Okay;
        this.ClientSize = new System.Drawing.Size(333, 371);
        this.Controls.Add(this.ReportContents);
        this.Controls.Add(this.CopyToClipboard);
        this.Controls.Add(this.Okay);
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.MinimumSize = new System.Drawing.Size(250, 250);
        this.Name = "Report";
        this.ShowIcon = false;
        this.ShowInTaskbar = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "Warmboard Report";
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button Okay;
    private System.Windows.Forms.Button CopyToClipboard;
    private System.Windows.Forms.TextBox ReportContents;
  }
}