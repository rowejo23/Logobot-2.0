using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logobot2_0
{
    public partial class MultipleSearchForm : Form
    {
        List<LogoGetAndLoad> multiSearchResults;
        private LogobotPaneMain logobotPaneMain;

        public MultipleSearchForm()
        {
            InitializeComponent();
        }

        public MultipleSearchForm(List<LogoGetAndLoad> multiSearchResults, LogobotPaneMain logobotPaneMain)
        {
            InitializeComponent();
            this.multiSearchResults = multiSearchResults;
            this.logobotPaneMain = logobotPaneMain;
        }

        private void MultipleSearchForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < multiSearchResults.Count; i++)
            {
                LogoGetAndLoad tmpGAL = multiSearchResults[i];
                this.listBoxMultiSearch.Items.Add(tmpGAL.SearchCompanyStr);
            }
            
            loadLogos(multiSearchResults[0]);
        }

        private void buttonMoveAllMulti_Click(object sender, EventArgs e)
        {
            logobotPaneMain.MoveAll(dataGridViewMultiBlotter);
        }

        private void buttonMoveSelectedMulti_Click(object sender, EventArgs e)
        {
            logobotPaneMain.MoveSelected(dataGridViewMultiBlotter);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewMultiBlotter_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                PictureBox zoomPic = new PictureBox();
                zoomPic.Image = dataGridViewMultiBlotter[0, e.RowIndex].Value as Image;

                Zoom_Popup pictureZoom = new Zoom_Popup(zoomPic);
                pictureZoom.Show();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void loadLogos(LogoGetAndLoad logoGAL)
        {
            int logoRows = dataGridViewMultiBlotter.Rows.Count;

            foreach (var logoRow in logoGAL.SearchListGAL)
            {
                dataGridViewMultiBlotter.Rows.Add();
                dataGridViewMultiBlotter[0, logoRows].Value = logoRow.SearchLogoImage;
                dataGridViewMultiBlotter[1, logoRows].Value = logoRow.SearchLogoDetailsStr;
                logoRows++;
            }
        }

        private void clearAllDGV()
        {
            for (int i = dataGridViewMultiBlotter.Rows.Count - 1; i >= 0; i--)
            {
                dataGridViewMultiBlotter.Rows.Remove(dataGridViewMultiBlotter.Rows[i]);
            }
        }

        private void listBoxMultiSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            String companyName = listBoxMultiSearch.SelectedItem.ToString();
            int index = listBoxMultiSearch.SelectedIndex;
            clearAllDGV();
            loadLogos(multiSearchResults[index]);
        }
    }
}
