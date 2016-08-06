namespace Logobot2_0
{
    partial class ArrangeResize
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ArrangeResize));
            this.buttonArrangeClose = new System.Windows.Forms.Button();
            this.labelArrangeMessage = new System.Windows.Forms.Label();
            this.checkBoxRemember = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // buttonArrangeClose
            // 
            this.buttonArrangeClose.Location = new System.Drawing.Point(267, 75);
            this.buttonArrangeClose.Name = "buttonArrangeClose";
            this.buttonArrangeClose.Size = new System.Drawing.Size(75, 23);
            this.buttonArrangeClose.TabIndex = 4;
            this.buttonArrangeClose.Text = "Close";
            this.buttonArrangeClose.UseVisualStyleBackColor = true;
            this.buttonArrangeClose.Click += new System.EventHandler(this.buttonArrangeClose_Click);
            // 
            // labelArrangeMessage
            // 
            this.labelArrangeMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelArrangeMessage.AutoSize = true;
            this.labelArrangeMessage.Location = new System.Drawing.Point(12, 15);
            this.labelArrangeMessage.Name = "labelArrangeMessage";
            this.labelArrangeMessage.Size = new System.Drawing.Size(320, 52);
            this.labelArrangeMessage.TabIndex = 3;
            this.labelArrangeMessage.Text = resources.GetString("labelArrangeMessage.Text");
            // 
            // checkBoxRemember
            // 
            this.checkBoxRemember.AutoSize = true;
            this.checkBoxRemember.Location = new System.Drawing.Point(12, 78);
            this.checkBoxRemember.Name = "checkBoxRemember";
            this.checkBoxRemember.Size = new System.Drawing.Size(130, 17);
            this.checkBoxRemember.TabIndex = 5;
            this.checkBoxRemember.Text = "Don\'t show this again.";
            this.checkBoxRemember.UseVisualStyleBackColor = true;
            this.checkBoxRemember.CheckedChanged += new System.EventHandler(this.checkBoxRemember_CheckedChanged);
            // 
            // ArrangeResize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(357, 111);
            this.ControlBox = false;
            this.Controls.Add(this.checkBoxRemember);
            this.Controls.Add(this.buttonArrangeClose);
            this.Controls.Add(this.labelArrangeMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ArrangeResize";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonArrangeClose;
        private System.Windows.Forms.Label labelArrangeMessage;
        private System.Windows.Forms.CheckBox checkBoxRemember;
    }
}