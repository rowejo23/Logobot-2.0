
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Logobot2_0
{
    public partial class TrialPopup : Form
    {
        public TrialPopup()
        {
            InitializeComponent();
        }

        public TrialPopup(string trialMessage)
        {
            InitializeComponent();
            labelTrialMessage.Text = trialMessage;
        }

        private void buttonTrialClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
