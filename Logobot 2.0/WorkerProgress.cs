using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logobot2_0
{
    public partial class WorkerProgress : Form
    {

        public WorkerProgress()
        {
            InitializeComponent();
        }

        public void setLabelText(string text)
        {
            this.labelWorkerProgress.Text = text;
        }

        public void setProgressBarValue(int value)
        {
            this.progressBarWorkerProgress.Value = value;
        }

        public void setProgressBarMin(int min)
        {
            this.progressBarWorkerProgress.Minimum = min;
        }

        public void setProgressBarMax(int max)
        {
            this.progressBarWorkerProgress.Maximum = max;
        }

        public void setProgressStyle(int style)
        {
            if (style == 0)
            {
                this.progressBarWorkerProgress.Style = ProgressBarStyle.Continuous;
            }
            else if (style == 1)
            {
                this.progressBarWorkerProgress.Style = ProgressBarStyle.Marquee;
            }
        }

        private void buttonWorkerProgressCancel_Click(object sender, EventArgs e)
        {
            LogobotPaneMain parent = (LogobotPaneMain)this.Owner;
            parent.backgroundWorkerSearchCancel();
            parent.backgroundWorkerScreenCancel();
            parent.backgroundWorkerUserCancel();
        }
    }
}
