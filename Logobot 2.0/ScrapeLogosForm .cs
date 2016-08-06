using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Logobot2_0
{
    public partial class ScrapeLogosForm : Form
    {
        LogoGetAndLoad scrapedLogos;
        private LogobotPaneInstance logobotPaneMain;

        public ScrapeLogosForm()
        {
            InitializeComponent();
        }

        public ScrapeLogosForm(LogoGetAndLoad scrapedLogos, LogobotPaneInstance LogobotPaneMain)
        {
            InitializeComponent();
            this.scrapedLogos = scrapedLogos;
            this.logobotPaneMain = LogobotPaneMain;
            
        }

        private void ScrapeLogosForm_Load(object sender, EventArgs e)
        {         
            loadLogos(scrapedLogos);
            Cursor.Current = Cursors.Default;
        }

        private void buttonMoveAll_Click(object sender, EventArgs e)
        {
            int startRow = logobotPaneMain.PaneDataGridViewBlotter.Rows.Count;

            for (int i = 0; i < dataGridViewScrapeLogosBlotter.Rows.Count; i++)
            {
                logobotPaneMain.PaneDataGridViewBlotter.Rows.Add();
                logobotPaneMain.PaneDataGridViewBlotter[0, startRow].Value = dataGridViewScrapeLogosBlotter.Rows[i].Cells[0].Value;
                logobotPaneMain.PaneDataGridViewBlotter[1, startRow].Value = dataGridViewScrapeLogosBlotter.Rows[i].Cells[1].Value;
                startRow = logobotPaneMain.PaneDataGridViewBlotter.Rows.Count;
            }
        }

        private void buttonMoveSelected_Click(object sender, EventArgs e)
        {
            int startRow = logobotPaneMain.PaneDataGridViewBlotter.Rows.Count;

            if (dataGridViewScrapeLogosBlotter.SelectedRows.Count != 0)
            {
                for (int i = 0; i < dataGridViewScrapeLogosBlotter.SelectedRows.Count; i++)
                {
                    logobotPaneMain.PaneDataGridViewBlotter.Rows.Add();
                    logobotPaneMain.PaneDataGridViewBlotter[0, startRow].Value = dataGridViewScrapeLogosBlotter.SelectedRows[i].Cells[0].Value;
                    logobotPaneMain.PaneDataGridViewBlotter[1, startRow].Value = dataGridViewScrapeLogosBlotter.SelectedRows[i].Cells[1].Value;
                    startRow = logobotPaneMain.PaneDataGridViewBlotter.Rows.Count;
                }
            }
            else
            {
                MessageBox.Show("You didn't select any rows.");
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewScrapeLogosBlotter_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                PictureBox zoomPic = new PictureBox();
                zoomPic.Image = dataGridViewScrapeLogosBlotter[0, e.RowIndex].Value as Image;

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
            int logoRows = dataGridViewScrapeLogosBlotter.Rows.Count;

            foreach (var logoRow in logoGAL.SearchListGAL)
            {
                dataGridViewScrapeLogosBlotter.Rows.Add();
                dataGridViewScrapeLogosBlotter[0, logoRows].Value = logoRow.SearchLogoImage;
                dataGridViewScrapeLogosBlotter[1, logoRows].Value = logoRow.SearchLogoDetailsStr;
                logoRows++;
            }
        }

        private void clearAllDGV()
        {
            for (int i = dataGridViewScrapeLogosBlotter.Rows.Count - 1; i >= 0; i--)
            {
                dataGridViewScrapeLogosBlotter.Rows.Remove(dataGridViewScrapeLogosBlotter.Rows[i]);
            }
        }
    }
}
