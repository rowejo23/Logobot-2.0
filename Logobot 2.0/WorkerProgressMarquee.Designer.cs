namespace Logobot2_0
{
    partial class WorkerProgressMarquee
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
            this.progressBarWorkerProgressMarquee = new System.Windows.Forms.ProgressBar();
            this.labelWorkerProgressMarquee = new System.Windows.Forms.Label();
            this.buttonWorkerProgressMarqueeCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // progressBarWorkerProgressMarquee
            // 
            this.progressBarWorkerProgressMarquee.Location = new System.Drawing.Point(12, 34);
            this.progressBarWorkerProgressMarquee.Name = "progressBarWorkerProgressMarquee";
            this.progressBarWorkerProgressMarquee.Size = new System.Drawing.Size(242, 23);
            this.progressBarWorkerProgressMarquee.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBarWorkerProgressMarquee.TabIndex = 0;
            // 
            // labelWorkerProgressMarquee
            // 
            this.labelWorkerProgressMarquee.AutoSize = true;
            this.labelWorkerProgressMarquee.Location = new System.Drawing.Point(9, 13);
            this.labelWorkerProgressMarquee.Name = "labelWorkerProgressMarquee";
            this.labelWorkerProgressMarquee.Size = new System.Drawing.Size(70, 13);
            this.labelWorkerProgressMarquee.TabIndex = 1;
            this.labelWorkerProgressMarquee.Text = "Please wait...";
            // 
            // buttonWorkerProgressMarqueeCancel
            // 
            this.buttonWorkerProgressMarqueeCancel.Location = new System.Drawing.Point(180, 63);
            this.buttonWorkerProgressMarqueeCancel.Name = "buttonWorkerProgressMarqueeCancel";
            this.buttonWorkerProgressMarqueeCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonWorkerProgressMarqueeCancel.TabIndex = 3;
            this.buttonWorkerProgressMarqueeCancel.Text = "Cancel";
            this.buttonWorkerProgressMarqueeCancel.UseVisualStyleBackColor = true;
            this.buttonWorkerProgressMarqueeCancel.Click += new System.EventHandler(this.buttonWorkerProgressMarqueeCancel_Click);
            // 
            // WorkerProgressMarquee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(265, 93);
            this.ControlBox = false;
            this.Controls.Add(this.buttonWorkerProgressMarqueeCancel);
            this.Controls.Add(this.labelWorkerProgressMarquee);
            this.Controls.Add(this.progressBarWorkerProgressMarquee);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "WorkerProgressMarquee";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBarWorkerProgressMarquee;
        private System.Windows.Forms.Label labelWorkerProgressMarquee;
        private System.Windows.Forms.Button buttonWorkerProgressMarqueeCancel;
    }
}