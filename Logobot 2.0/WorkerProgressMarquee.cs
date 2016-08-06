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
    public partial class WorkerProgressMarquee : Form
    {

        public WorkerProgressMarquee()
        {
            InitializeComponent();
        }

        public void setLabelText(string text)
        {
            this.labelWorkerProgressMarquee.Text = text;
        }

        private void buttonWorkerProgressMarqueeCancel_Click(object sender, EventArgs e)
        {
            LogobotPaneMain parent = (LogobotPaneMain)this.Owner;
            parent.backgroundWorkerSearchCancel();
            parent.backgroundWorkerScreenCancel();
            parent.backgroundWorkerUserCancel();
            parent.backgroundWorkerMultiSearchCancel();
        }
    }
}
