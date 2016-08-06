
using System;
using System.IO;
using System.Windows.Forms;

namespace Logobot2_0
{
    public partial class ArrangeResize : Form
    {
        public ArrangeResize()
        {
            InitializeComponent();
        }

        private void checkBoxRemember_CheckedChanged(object sender, EventArgs e)
        {
            LogobotPaneMain parent = (LogobotPaneMain)this.Owner;

            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            path = Path.Combine(path, "Logobot");
            string pathXmlDoc = Path.Combine(path, "LogobotSettings.xml");

            if (checkBoxRemember.Checked)
            {
                parent.getSetSHOW_ARRANGE_TIP = false;
                LogobotSettings userSettings = new LogobotSettings();
                userSettings.Load(pathXmlDoc);
                userSettings.Update("showArrangeTip", false);
                userSettings.Save(pathXmlDoc);
            }
        }

        private void buttonArrangeClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
