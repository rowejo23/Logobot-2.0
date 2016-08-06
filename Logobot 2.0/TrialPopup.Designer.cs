namespace Logobot2_0
{
    partial class TrialPopup
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
            this.buttonTrialClose = new System.Windows.Forms.Button();
            this.labelTrialMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonTrialClose
            // 
            this.buttonTrialClose.Location = new System.Drawing.Point(267, 75);
            this.buttonTrialClose.Name = "buttonTrialClose";
            this.buttonTrialClose.Size = new System.Drawing.Size(75, 23);
            this.buttonTrialClose.TabIndex = 4;
            this.buttonTrialClose.Text = "Close";
            this.buttonTrialClose.UseVisualStyleBackColor = true;
            this.buttonTrialClose.Click += new System.EventHandler(this.buttonTrialClose_Click);
            // 
            // labelTrialMessage
            // 
            this.labelTrialMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTrialMessage.Location = new System.Drawing.Point(12, 15);
            this.labelTrialMessage.MinimumSize = new System.Drawing.Size(325, 50);
            this.labelTrialMessage.Name = "labelTrialMessage";
            this.labelTrialMessage.Size = new System.Drawing.Size(325, 50);
            this.labelTrialMessage.TabIndex = 3;
            // 
            // TrialPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(357, 111);
            this.ControlBox = false;
            this.Controls.Add(this.buttonTrialClose);
            this.Controls.Add(this.labelTrialMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "TrialPopup";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonTrialClose;
        private System.Windows.Forms.Label labelTrialMessage;
    }
}