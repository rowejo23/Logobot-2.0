using Logobot2_0.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Logobot2_0
{
    public partial class frmSaveBlotter : Form
    {
        LogobotPaneInstance lgbtPaneSave;
        string username;
        
        public frmSaveBlotter()
        {
            InitializeComponent();
        }

        public frmSaveBlotter(LogobotPaneInstance lgbtPane, string username)
        {
            InitializeComponent();
            lgbtPaneSave = lgbtPane;
            this.username = username;
        }

        private void frmSaveBlotter_Load(object sender, EventArgs e)
        {
            loadSavedBlotterSelectList();
            Cursor.Current = Cursors.Default;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string blotterName = textBoxSaveBlotterName.Text.Trim();
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");

            if (regexItem.IsMatch(blotterName))
            {
                handleSave(blotterName);
            }
            else
            {
                MessageBox.Show("There is an invalid character in your blotter name.");
            }
        }

        private void textBoxSaveBlotterName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string blotterName = textBoxSaveBlotterName.Text.Trim();
                var regexItem = new Regex("^[a-zA-Z0-9 ]*$");

                if (regexItem.IsMatch(blotterName))
                {
                    handleSave(blotterName);
                }
                else
                {
                    MessageBox.Show("There is an invalid character in your blotter name.");
                }
            }
        }

        private void handleSave(string blotterName)
        {
            Cursor.Current = Cursors.WaitCursor;
            LogobotUserBlotter lgbtUB = new LogobotUserBlotter();
            lgbtUB.Username = username;
            lgbtUB.BlotterName = blotterName;

            if (textBoxSaveBlotterName.Text.Trim() != "")
            {
                if (checkDuplicateSavedBlotterName(blotterName))
                {
                    DialogResult dialogResult = MessageBox.Show("The name you entered matches an existing saved Logo Blotter name.  Do you want to update the saved Logo Blotter?", "Duplicate Logo Blotter Name", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        if (deleteSavedBlotterLogos(lgbtUB))
                        {
                            if(updateSavedBlotterLogos(lgbtUB, lgbtPaneSave.PaneDataGridViewBlotter))
                            {
                                MessageBox.Show(lgbtUB.BlotterName + " updated", "Update Success", MessageBoxButtons.OK);
                            }
                            else
                            {
                                MessageBox.Show("Logo Blotter Delete Error", "Delete Failure", MessageBoxButtons.OK);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Logo Blotter Delete Error", "Delete Failure", MessageBoxButtons.OK);
                        }
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        this.textBoxSaveBlotterName.Focus();
                    }
                }
                else
                {
                    if(saveBlotter(lgbtUB))
                    {
                        if(saveBlotterLogos(lgbtUB, lgbtPaneSave.PaneDataGridViewBlotter))
                        {
                            this.listBoxSavedBlotterUpdate.Items.Clear();
                            loadSavedBlotterSelectList();
                            MessageBox.Show("Logo Blotter Saved", "Save Success", MessageBoxButtons.OK);
                            this.textBoxSaveBlotterName.Focus();
                        }
                        else
                        {
                            MessageBox.Show("Logo Blotter Save Error", "Save Failure", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Logo Blotter Save Error", "Save Failure", MessageBoxButtons.OK);
                    }
                }
            }
            else
            {
                MessageBox.Show("You didn't type in a name for your Logo Blotter.  Try again.");
                textBoxSaveBlotterName.Focus();
            }
            Cursor.Current = Cursors.Default;
        }

        private bool checkDuplicateSavedBlotterName(string blotterName)
        {
            bool same = false;

            for(int i = 0; i< listBoxSavedBlotterUpdate.Items.Count; i++)
            {
                if (blotterName == (string)listBoxSavedBlotterUpdate.Items[i])
                {
                    same = true;
                }
            }
            return same;
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            LogobotUserBlotter lgbtUB = new LogobotUserBlotter();
            lgbtUB.Username = username;

            if (listBoxSavedBlotterUpdate.SelectedIndex != -1)
            {
                Cursor.Current = Cursors.WaitCursor;
                lgbtUB.BlotterName = listBoxSavedBlotterUpdate.SelectedItem.ToString();
                if(deleteSavedBlotterLogos(lgbtUB))
                {
                    if(updateSavedBlotterLogos(lgbtUB, lgbtPaneSave.PaneDataGridViewBlotter))
                    {
                        MessageBox.Show(lgbtUB.BlotterName + " updated", "Update Success", MessageBoxButtons.OK);
                        Cursor.Current = Cursors.Default;
                    }
                    else
                    {
                        MessageBox.Show("Logo Blotter Update Error", "Update Failure", MessageBoxButtons.OK);
                        Cursor.Current = Cursors.Default;
                    }
                }
                else
                {
                    MessageBox.Show("Logo Blotter Update Error", "Update Failure", MessageBoxButtons.OK);
                    Cursor.Current = Cursors.Default;
                }
            }
            else
            {
                MessageBox.Show("You didn't select a saved Logo Blotter to update. Try again.");
                listBoxSavedBlotterUpdate.Focus();
                Cursor.Current = Cursors.Default;
            }
        }

        private bool saveBlotter(LogobotUserBlotter lgbtUB)
        {
            bool saveSuccess = false;
            LogobotAPIClient apiClient = new LogobotAPIClient();

            using (var client = apiClient.getHTTPClient())
            {
                HttpResponseMessage response = client.PostAsJsonAsync("api/LogobotUser/saveblotter", lgbtUB).Result;
                if (response.IsSuccessStatusCode)
                {
                    saveSuccess = response.Content.ReadAsAsync<bool>().Result;
                }
            }
            return saveSuccess;
        }

        private bool saveBlotterLogos(LogobotUserBlotter lgbtUB, DataGridView lgbtBlotter)
        {
            bool saveSuccess = false;
            LogobotAPIClient apiClient = new LogobotAPIClient();

            for (int i = 0; i < lgbtBlotter.Rows.Count; i++)
            {
                LogobotUserBlotter tmpLgbtUB = new LogobotUserBlotter();
                
                tmpLgbtUB.CompanyName = (string)lgbtBlotter.Rows[i].Cells[1].Value;
                Image tmpImage = (Image)lgbtBlotter.Rows[i].Cells[0].Value;

                using (MemoryStream ms = new MemoryStream())
                {
                    tmpImage.Save(ms, ImageFormat.Png);
                    tmpLgbtUB.ImgArray = ms.ToArray();
                }

                lgbtUB.LstLGUB.Add(tmpLgbtUB);
            }

            using (var client = apiClient.getHTTPClient())
            {
                HttpResponseMessage response = client.PostAsJsonAsync("api/LogobotUser/saveblotterlogos", lgbtUB).Result;
                if (response.IsSuccessStatusCode)
                {
                    saveSuccess = response.Content.ReadAsAsync<bool>().Result;
                }
            }
            return saveSuccess;
        }

        private bool deleteSavedBlotterLogos(LogobotUserBlotter lgbtUB)
        {
            bool saveSuccess = false;
            LogobotAPIClient apiClient = new LogobotAPIClient();

            using (var client = apiClient.getHTTPClient())
            {
                HttpResponseMessage response = client.PostAsJsonAsync("api/LogobotUser/deletesavedblotterlogos", lgbtUB).Result;
                if (response.IsSuccessStatusCode)
                {
                    saveSuccess = response.Content.ReadAsAsync<bool>().Result;
                }
            }
            return saveSuccess;
        }

        private bool deleteSavedBlotter(LogobotUserBlotter lgbtUB)
        {
            bool saveSuccess = false;
            LogobotAPIClient apiClient = new LogobotAPIClient();

            using (var client = apiClient.getHTTPClient())
            {
                HttpResponseMessage response = client.PostAsJsonAsync("api/LogobotUser/deletesavedblotter", lgbtUB).Result;
                if (response.IsSuccessStatusCode)
                {
                    saveSuccess = response.Content.ReadAsAsync<bool>().Result;
                }
            }
            return saveSuccess;
        }

        private bool updateSavedBlotterLogos(LogobotUserBlotter lgbtUB, DataGridView lgbtBlotter)
        {
            bool saveSuccess = false;
            LogobotAPIClient apiClient = new LogobotAPIClient();

            for (int i = 0; i < lgbtBlotter.Rows.Count; i++)
            {
                LogobotUserBlotter tmpLgbtUB = new LogobotUserBlotter();
                tmpLgbtUB.CompanyName = (string)lgbtBlotter.Rows[i].Cells[1].Value;
                Image tmpImage = (Image)lgbtBlotter.Rows[i].Cells[0].Value;

                using (MemoryStream ms = new MemoryStream())
                {
                    tmpImage.Save(ms, ImageFormat.Png);
                    tmpLgbtUB.ImgArray = ms.ToArray();
                }
                lgbtUB.LstLGUB.Add(tmpLgbtUB);
            }

            using (var client = apiClient.getHTTPClient())
            {
                HttpResponseMessage response = client.PostAsJsonAsync("api/LogobotUser/updatesavedblotterlogos", lgbtUB).Result;
                if (response.IsSuccessStatusCode)
                {
                    saveSuccess = response.Content.ReadAsAsync<bool>().Result;
                }
            }
            return saveSuccess;
        }

        private void loadSavedBlotterSelectList()
        {
            List<string> listUserBlotters = new List<string>();
            LogobotAPIClient apiClient = new LogobotAPIClient();

            using (var client = apiClient.getHTTPClient())
            {
                HttpResponseMessage response = client.PostAsJsonAsync("api/LogobotUser/getuserblotters", username).Result;
                if (response.IsSuccessStatusCode)
                {
                    listUserBlotters = response.Content.ReadAsAsync<List<string>>().Result;
                }
            }

            foreach (var blotter in listUserBlotters)
            {
                string blotterName = blotter as string;
                this.listBoxSavedBlotterUpdate.Items.Add(blotterName);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            LogobotUserBlotter lgbtUB = new LogobotUserBlotter();
            lgbtUB.Username = username;

            if (listBoxSavedBlotterUpdate.SelectedIndex != -1)
            {
                Cursor.Current = Cursors.WaitCursor;
                lgbtUB.BlotterName = listBoxSavedBlotterUpdate.SelectedItem.ToString();

                if (deleteSavedBlotterLogos(lgbtUB))
                {
                    if(deleteSavedBlotter(lgbtUB))
                    {
                        this.listBoxSavedBlotterUpdate.Items.Clear();
                        loadSavedBlotterSelectList();
                        MessageBox.Show(lgbtUB.BlotterName + " deleted", "Blotter Deleted", MessageBoxButtons.OK);
                        Cursor.Current = Cursors.Default;
                    }
                    else
                    {
                        MessageBox.Show("Logo Blotter Delete Error", "Delete Failure", MessageBoxButtons.OK);
                        Cursor.Current = Cursors.Default;
                    }
                }
                else
                {
                    MessageBox.Show("Logo Blotter Delete Error", "Delete Failure", MessageBoxButtons.OK);
                    Cursor.Current = Cursors.Default;
                }
            }
            else
            {
                MessageBox.Show("You didn't select a saved Logo Blotter to delete. Try again.");
                listBoxSavedBlotterUpdate.Focus();
                Cursor.Current = Cursors.Default;
            }
        }
    }

    class LogobotUserBlotter
    {
        private string username = "";

        private string blotterName = "";

        private string companyName = "";

        private Image imgLogo;

        private byte[] imgArray;

        private List<LogobotUserBlotter> lstLGUB = new List<LogobotUserBlotter>();

        public LogobotUserBlotter()
        {
        }

        public Image ImgLogo
        {
            get { return this.imgLogo; }
            set { imgLogo = value; }
        }

        public byte[] ImgArray
        {
            get { return this.imgArray; }
            set { imgArray = value; }
        }

        public string Username
        {
            get { return this.username; }
            set { username = value; }
        }

        public string BlotterName
        {
            get { return this.blotterName; }
            set { blotterName = value; }
        }

        public string CompanyName
        {
            get { return this.companyName; }
            set { companyName = value; }
        }

        public List<LogobotUserBlotter> LstLGUB
        {
            get { return this.lstLGUB; }
            set { lstLGUB = value; }
        }
    }
}
