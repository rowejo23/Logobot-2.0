namespace Logobot2_0
{
    partial class WorkerProgress
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
            this.progressBarWorkerProgress = new System.Windows.Forms.ProgressBar();
            this.labelWorkerProgress = new System.Windows.Forms.Label();
            this.buttonWorkerProgressCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // progressBarWorkerProgress
            // 
            this.progressBarWorkerProgress.Location = new System.Drawing.Point(12, 34);
            this.progressBarWorkerProgress.Name = "progressBarWorkerProgress";
            this.progressBarWorkerProgress.Size = new System.Drawing.Size(242, 23);
            this.progressBarWorkerProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarWorkerProgress.TabIndex = 0;
            // 
            // labelWorkerProgress
            // 
            this.labelWorkerProgress.AutoSize = true;
            this.labelWorkerProgress.Location = new System.Drawing.Point(9, 13);
            this.labelWorkerProgress.Name = "labelWorkerProgress";
            this.labelWorkerProgress.Size = new System.Drawing.Size(70, 13);
            this.labelWorkerProgress.TabIndex = 1;
            this.labelWorkerProgress.Text = "Please wait...";
            // 
            // buttonWorkerProgressCancel
            // 
            this.buttonWorkerProgressCancel.Location = new System.Drawing.Point(178, 64);
            this.buttonWorkerProgressCancel.Name = "buttonWorkerProgressCancel";
            this.buttonWorkerProgressCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonWorkerProgressCancel.TabIndex = 2;
            this.buttonWorkerProgressCancel.Text = "Cancel";
            this.buttonWorkerProgressCancel.UseVisualStyleBackColor = true;
            this.buttonWorkerProgressCancel.Click += new System.EventHandler(this.buttonWorkerProgressCancel_Click);
            // 
            // WorkerProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(265, 95);
            this.ControlBox = false;
            this.Controls.Add(this.buttonWorkerProgressCancel);
            this.Controls.Add(this.labelWorkerProgress);
            this.Controls.Add(this.progressBarWorkerProgress);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "WorkerProgress";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBarWorkerProgress;
        private System.Windows.Forms.Label labelWorkerProgress;
        private System.Windows.Forms.Button buttonWorkerProgressCancel;
    }
}