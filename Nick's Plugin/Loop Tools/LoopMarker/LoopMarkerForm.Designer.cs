
namespace WBPlugin.Loop_Tools
{
    partial class LoopMarkerForm
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
            this.ManifoldLabel = new System.Windows.Forms.Label();
            this.textManifold = new System.Windows.Forms.TextBox();
            this.textRoom = new System.Windows.Forms.TextBox();
            this.labelRoom = new System.Windows.Forms.Label();
            this.textLoop = new System.Windows.Forms.TextBox();
            this.labelLoop = new System.Windows.Forms.Label();
            this.labelAddLength = new System.Windows.Forms.Label();
            this.textAddLength = new System.Windows.Forms.TextBox();
            this.cmdOk = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ManifoldLabel
            // 
            this.ManifoldLabel.AutoSize = true;
            this.ManifoldLabel.Location = new System.Drawing.Point(13, 31);
            this.ManifoldLabel.Name = "ManifoldLabel";
            this.ManifoldLabel.Size = new System.Drawing.Size(47, 13);
            this.ManifoldLabel.TabIndex = 0;
            this.ManifoldLabel.Text = "Manifold";
            // 
            // textManifold
            // 
            this.textManifold.Location = new System.Drawing.Point(66, 28);
            this.textManifold.Name = "textManifold";
            this.textManifold.Size = new System.Drawing.Size(100, 20);
            this.textManifold.TabIndex = 1;
            // 
            // textRoom
            // 
            this.textRoom.Location = new System.Drawing.Point(66, 71);
            this.textRoom.Name = "textRoom";
            this.textRoom.Size = new System.Drawing.Size(100, 20);
            this.textRoom.TabIndex = 3;
            // 
            // labelRoom
            // 
            this.labelRoom.AutoSize = true;
            this.labelRoom.Location = new System.Drawing.Point(13, 74);
            this.labelRoom.Name = "labelRoom";
            this.labelRoom.Size = new System.Drawing.Size(35, 13);
            this.labelRoom.TabIndex = 2;
            this.labelRoom.Text = "Room";
            // 
            // textLoop
            // 
            this.textLoop.Location = new System.Drawing.Point(118, 133);
            this.textLoop.Name = "textLoop";
            this.textLoop.Size = new System.Drawing.Size(48, 20);
            this.textLoop.TabIndex = 17;
            // 
            // labelLoop
            // 
            this.labelLoop.AutoSize = true;
            this.labelLoop.Location = new System.Drawing.Point(13, 139);
            this.labelLoop.Name = "labelLoop";
            this.labelLoop.Size = new System.Drawing.Size(71, 13);
            this.labelLoop.TabIndex = 16;
            this.labelLoop.Text = "Loop Number";
            // 
            // labelAddLength
            // 
            this.labelAddLength.AutoSize = true;
            this.labelAddLength.Location = new System.Drawing.Point(13, 106);
            this.labelAddLength.Name = "labelAddLength";
            this.labelAddLength.Size = new System.Drawing.Size(104, 13);
            this.labelAddLength.TabIndex = 15;
            this.labelAddLength.Text = "Additional Length (ft)";
            // 
            // textAddLength
            // 
            this.textAddLength.Location = new System.Drawing.Point(134, 100);
            this.textAddLength.Name = "textAddLength";
            this.textAddLength.Size = new System.Drawing.Size(32, 20);
            this.textAddLength.TabIndex = 14;
            // 
            // cmdOk
            // 
            this.cmdOk.Location = new System.Drawing.Point(27, 181);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(75, 23);
            this.cmdOk.TabIndex = 13;
            this.cmdOk.Text = "&Ok";
            this.cmdOk.UseVisualStyleBackColor = true;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(108, 181);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 12;
            this.cmdCancel.Text = "&Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // LoopMarkerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(212, 225);
            this.ControlBox = false;
            this.Controls.Add(this.textLoop);
            this.Controls.Add(this.labelLoop);
            this.Controls.Add(this.labelAddLength);
            this.Controls.Add(this.textAddLength);
            this.Controls.Add(this.cmdOk);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.textRoom);
            this.Controls.Add(this.labelRoom);
            this.Controls.Add(this.textManifold);
            this.Controls.Add(this.ManifoldLabel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoopMarkerForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Loop";
            this.Load += new System.EventHandler(this.LoopMarkerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ManifoldLabel;
        private System.Windows.Forms.TextBox textManifold;
        private System.Windows.Forms.TextBox textRoom;
        private System.Windows.Forms.Label labelRoom;
        private System.Windows.Forms.TextBox textLoop;
        private System.Windows.Forms.Label labelLoop;
        private System.Windows.Forms.Label labelAddLength;
        private System.Windows.Forms.TextBox textAddLength;
        private System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.Button cmdCancel;
    }
}