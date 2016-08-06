using System;
using System.Drawing;
using System.Windows.Forms;
using AddinExpress.PP;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Data;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using System.Drawing.Imaging;
using System.ComponentModel;
using Logobot2_0.Properties;
using Excel;
using System.Net.Http;
using ImageProcessor;

namespace Logobot2_0
{
    public partial class LogobotPaneMain : AddinExpress.PP.ADXPowerPointTaskPane
    {
        private static DataTable companyDT;
        private static ScreenSources SCREEN_DATA_SOURCE;
        private static bool SCREEN_NO_LOGO = false;
        private static bool SHOW_ARRANGE_TIP = true;
        private LogobotUser currentUser;

        WorkerProgressMarquee wpm;
        AddinModule module;

        // Global variable to indicate if it is the first search (false) or search for more results (true)
        private bool MORE_RESULTS_SEARCH = false;
        private bool SECOND_SEARCH = false;
        private string SEARCH_TEXT = "";

        public LogobotPaneMain()
        {
            InitializeComponent();
        }

        public static ScreenSources getSetSCREEN_DATA_SOURCE
        {
            get { return SCREEN_DATA_SOURCE; }
            set { SCREEN_DATA_SOURCE = value; }
        }

        public static bool getSetSCREEN_NO_LOGO
        {
            get { return SCREEN_NO_LOGO; }
            set { SCREEN_NO_LOGO = value; }
        }

        public bool getSetSHOW_ARRANGE_TIP
        {
            get { return SHOW_ARRANGE_TIP; }
            set { SHOW_ARRANGE_TIP = value; }
        }

        public ImageList getImageListTreeViews
        {
            get { return this.imageListTreeViews; }
        }

        private void LogobotPaneMain_Load(object sender, EventArgs e)
        {
            module = AddinExpress.MSO.ADXAddinModule.CurrentInstance as Logobot2_0.AddinModule;
            currentUser = module.getSetCurrentUser;

            treeViewScreen.ImageList = imageListTreeViews;
            treeViewScreen.ImageIndex = 2;
            treeViewScreen.SelectedImageIndex = 2;
            treeViewScreen.ExpandAll();

            treeViewUser.ImageList = imageListTreeViews;
            treeViewUser.ImageIndex = 3;
            treeViewUser.SelectedImageIndex = 3;
            treeViewUser.ExpandAll();

            if (!Logobot2_0.AddinModule.CurrentInstance.noInternetConnection)
            {
                if (Logobot2_0.AddinModule.CurrentInstance.disableLogobotPane)
                {
                    SCREEN_DATA_SOURCE = module.getSetSCREEN_DATA_SOURCES[0];
                    getScreenCategories(this.treeViewScreen, SCREEN_DATA_SOURCE);

                    if (currentUser != null)
                    {
                        setUserBlotters(this.treeViewUser, currentUser.Username);
                    }

                    getSetSCREEN_NO_LOGO = module.getSCREEN_NO_LOGO;
                    getSetSHOW_ARRANGE_TIP = module.getSHOW_ARRANGE_TIP;

                    setAutoComplete();
                    this.Enabled = false;
                }
                else
                {
                    module = AddinExpress.MSO.ADXAddinModule.CurrentInstance as Logobot2_0.AddinModule;
                    SCREEN_DATA_SOURCE = module.getSetSCREEN_DATA_SOURCES[0];

                    getScreenCategories(this.treeViewScreen, SCREEN_DATA_SOURCE);

                    if (currentUser != null)
                    {
                        setUserBlotters(this.treeViewUser, currentUser.Username);
                    }

                    getSetSCREEN_NO_LOGO = module.getSCREEN_NO_LOGO;
                    getSetSHOW_ARRANGE_TIP = module.getSHOW_ARRANGE_TIP;
                    setAutoComplete();
                }
            }
            else
            {
                module = AddinExpress.MSO.ADXAddinModule.CurrentInstance as Logobot2_0.AddinModule;
                SCREEN_DATA_SOURCE = module.getSetSCREEN_DATA_SOURCES[0];
                getSetSCREEN_NO_LOGO = module.getSCREEN_NO_LOGO;
                getSetSHOW_ARRANGE_TIP = module.getSHOW_ARRANGE_TIP;
                this.Enabled = false;
            }
        }

        public static void setUserBlotters(TreeView treeViewUser, String username)
        {
            getUserBlotters(treeViewUser, username);
        }

        public void setAutoComplete()
        {
            companyDT = module.getCOMPANYDT;

            //declare source.
            var source = new AutoCompleteStringCollection();

            //fill database records to source
            for (int i = 0; i < companyDT.Rows.Count; i++)
            {
                source.Add(companyDT.Rows[i][0].ToString());
            }

            textBoxSearch.AutoCompleteCustomSource = source;
            textBoxSearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBoxSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void textBoxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SEARCH_TEXT = textBoxSearch.Text.Trim();
                MORE_RESULTS_SEARCH = false;
                runLogoSearch();
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            SEARCH_TEXT = textBoxSearch.Text.Trim();
            MORE_RESULTS_SEARCH = false;
            runLogoSearch();
        }

        private void buttonMoreSearch_Click(object sender, EventArgs e)
        {
            if (SEARCH_TEXT != textBoxSearch.Text.Trim())
            {
                MORE_RESULTS_SEARCH = false;
                runLogoSearch();
            }
            else
            {
                MORE_RESULTS_SEARCH = true;
                runLogoSearch();
            }
        }

        private void runLogoSearch()
        {
            if (textBoxSearch.Text.Trim() != "")
            {
                if (module.checkForInternetConnection())
                {
                    dataGridViewSearch.SuspendLayout();
                    if (!MORE_RESULTS_SEARCH)
                    {
                        clearAllDGV(dataGridViewSearch);
                    }

                    LogoGetAndLoad logoGAL = new LogoGetAndLoad();
                    logoGAL.CurrentUser = currentUser.Username;
                    logoGAL.SendingDGVProperty = LogoGetAndLoad.SendingDGV.Search;
                    logoGAL.SearchCompanyStr = textBoxSearch.Text.Trim().ToLower();
                    logoGAL.SearchStr = textBoxSearch.Text.Trim() + " logo";
                    logoGAL.MoreResults = MORE_RESULTS_SEARCH;

                    wpm = new WorkerProgressMarquee();
                    wpm.setLabelText("Logobot is getting your logos:");
                    logoGAL.WPM = wpm;
                    getLogos(logoGAL);
                }
                else
                {
                    MessageBox.Show("There doesn't seem to be an internet connection.  Check you have internet access and click the Activate button.");
                    this.Enabled = false;
                    module.disableLogobot(false);
                }
            }
            else
            {
                MessageBox.Show("You didn't enter a company name?");
                textBoxSearch.Focus();
            }
        }

        private void treeViewScreen_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            buttonScreen_Click(sender, e);
        }

        private void buttonScreen_Click(object sender, EventArgs e)
        {
            List<string> checkedKeys = new List<string>();
            LogoGetAndLoad logoGAL = new LogoGetAndLoad();
            Boolean isChecked = false;
            CustomTreeNode smallNode = new CustomTreeNode();
            logoGAL.SendingDGVProperty = LogoGetAndLoad.SendingDGV.Screen;

            if (treeViewScreen.SelectedNode != null)
            {
                if (SCREEN_DATA_SOURCE.SourceName != "Public" && SCREEN_DATA_SOURCE.SourceName != "Industry" && treeViewScreen.SelectedNode.Parent != null)
                {
                    smallNode = (CustomTreeNode)treeViewScreen.SelectedNode;
                    checkedKeys.Add(smallNode.UniqueValueKey);
                    isChecked = true;
                }
                else if (!treeViewScreen.Nodes[0].IsSelected)
                {
                    smallNode = (CustomTreeNode)treeViewScreen.SelectedNode;
                    checkedKeys.Add(smallNode.UniqueValueKey);
                    isChecked = true;
                }
            }

            if (isChecked)
            {
                if (SCREEN_DATA_SOURCE.SourceName != "Public" && SCREEN_DATA_SOURCE.SourceName != "Industry")
                {
                    logoGAL.ScreenWhereStr = string.Join("','", checkedKeys);
                }
                else if (SCREEN_DATA_SOURCE.SourceName == "Public")
                {
                    string country = treeViewScreen.Nodes[0].Nodes[0].Text;

                    switch (treeViewScreen.SelectedNode.Level)
                    {
                        case 1:  // Country
                            {
                                logoGAL.ScreenWhereStr = "country = '" + string.Join("','", checkedKeys) + "'";
                                break;
                            }
                        case 2:  // Primary Sector
                            {
                                logoGAL.ScreenWhereStr = "country = '" + country + "' and primary_sector = '" + string.Join("','", checkedKeys) + "'";
                                break;
                            }
                        case 3:  // Primary Industry
                            {
                                logoGAL.ScreenWhereStr = "country = '" + country + "' and primary_industry = '" + string.Join("','", checkedKeys) + "'";
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }
                else if (SCREEN_DATA_SOURCE.SourceName == "Industry")
                {
                    string sector = treeViewScreen.Nodes[0].Nodes[0].Text;

                    switch (treeViewScreen.SelectedNode.Level)
                    {
                        case 1:  // Primary Sector
                            {
                                logoGAL.ScreenWhereStr = "primary_sector = '" + sector + "'";
                                break;
                            }
                        case 2:  // Primary Industry
                            {
                                logoGAL.ScreenWhereStr = "primary_sector = '" + sector + "' and primary_industry = '" + string.Join("','", checkedKeys) + "'";
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }

                if (module.checkForInternetConnection())
                {
                    clearAllDGV(this.dataGridViewScreen);

                    dataGridViewScreen.Visible = true;
                    dataGridViewScreen.SuspendLayout();
                    treeViewScreen.Visible = false;
                    wpm = new WorkerProgressMarquee();
                    wpm.setLabelText("Logobot is getting your logos:");
                    logoGAL.WPM = wpm;
                    getLogos(logoGAL);
                }
                else
                {
                    MessageBox.Show("There doesn't seem to be an internet connection.  Check you have internet access and click the Activate button.");
                    this.Enabled = false;
                    module.disableLogobot(false);
                }
            }
            else
            {
                MessageBox.Show("You didn't make a category selection.  You must select at least one category for get logos.");
                treeViewScreen.Focus();
            }
        }

        private void buttonScreenBack_Click(object sender, EventArgs e)
        {
            treeViewScreen.Visible = true;
            dataGridViewScreen.Visible = false;
            buttonScreenBack.Visible = false;
            buttonScreen.Enabled = true;
            buttonMoveAllScreen.Visible = false;
            buttonMoveSelectedScreen.Visible = false;
        }

        private void buttonUserBack_Click(object sender, EventArgs e)
        {
            treeViewUser.Visible = true;
            buttonUserRefresh.Visible = true;

            dataGridViewUser.Visible = false;
            buttonUserBack.Visible = false;
            buttonMoveAllUser.Visible = false;
            buttonMoveSelectedUser.Visible = false;

            buttonUser.Enabled = true;
        }

        private void buttonUser_Click(object sender, EventArgs e)
        {
            List<string> checkedKeys = new List<string>();
            LogoGetAndLoad logoGAL = new LogoGetAndLoad();
            Boolean isChecked = false;
            logoGAL.SendingDGVProperty = LogoGetAndLoad.SendingDGV.User;

            if (treeViewUser.SelectedNode != null && treeViewUser.SelectedNode.Parent != null)
            {
                CustomTreeNode smallNode = (CustomTreeNode)treeViewUser.SelectedNode;
                checkedKeys.Add(smallNode.UniqueValueKey);
                isChecked = true;
            }

            if (isChecked)
            {
                if (module.checkForInternetConnection())
                {
                    logoGAL.ScreenWhereStr = string.Join("','", checkedKeys);
                    clearAllDGV(this.dataGridViewUser);

                    dataGridViewUser.Visible = true;
                    dataGridViewUser.SuspendLayout();
                    treeViewUser.Visible = false;

                    wpm = new WorkerProgressMarquee();
                    wpm.setLabelText("Logobot is getting your logos:");
                    logoGAL.WPM = wpm;
                    getLogos(logoGAL);
                }
                else
                {
                    MessageBox.Show("There doesn't seem to be an internet connection.  Check you have internet access and click the Activate button.");
                    this.Enabled = false;
                    module.disableLogobot(false);
                }
            }
            else
            {
                MessageBox.Show("You didn't make a category selection.  You must select at least one category for get logos.");
                treeViewUser.Focus();
            }
        }

        private void getLogos(LogoGetAndLoad logoGAL)
        {
            int screenRowCount = 0;
            logoGAL.ScreenDataSource = SCREEN_DATA_SOURCE;
            logoGAL.ScreenNoLogo = SCREEN_NO_LOGO;

            switch (logoGAL.SendingDGVProperty)
            {
                case LogoGetAndLoad.SendingDGV.User: // User
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        wpm.Show(this);
                        this.Enabled = false;

                        System.Threading.SynchronizationContext.SetSynchronizationContext(new WindowsFormsSynchronizationContext());
                        backgroundWorkerUser.RunWorkerAsync(logoGAL);
                        break;
                    }
                case LogoGetAndLoad.SendingDGV.Screen: // Screen
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        wpm.Show(this);
                        this.Enabled = false;

                        System.Threading.SynchronizationContext.SetSynchronizationContext(new WindowsFormsSynchronizationContext());
                        backgroundWorkerScreen.RunWorkerAsync(logoGAL);
                        break;
                    }
                case LogoGetAndLoad.SendingDGV.Search: // Search
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        screenRowCount = getAllCompaniesImagesCount(logoGAL);

                        if (screenRowCount != 0 && !logoGAL.MoreResults) // First search, results are in database
                        {
                            logoGAL.SearchDBCount = screenRowCount;
                            logoGAL.SecondSearch = SECOND_SEARCH;
                        }
                        else
                        {
                            logoGAL.SearchDBCount = screenRowCount;
                            logoGAL.SecondSearch = SECOND_SEARCH;
                        }

                        wpm.Show(this);
                        this.Enabled = false;

                        System.Threading.SynchronizationContext.SetSynchronizationContext(new WindowsFormsSynchronizationContext());
                        backgroundWorkerSearch.RunWorkerAsync(logoGAL);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void loadLogos(LogoGetAndLoad logoGAL, DataGridView tmpDGV)
        {
            int logoRows = tmpDGV.Rows.Count;

            foreach (var logoRow in logoGAL.SearchListGAL)
            {
                tmpDGV.Rows.Add();
                tmpDGV[0, logoRows].Value = logoRow.SearchLogoImage;
                tmpDGV[1, logoRows].Value = logoRow.SearchLogoDetailsStr;
                logoRows++;
            }
        }

        private void backgroundWorkerSearch_DoWork(object sender, DoWorkEventArgs e)
        {
            LogoGetAndLoad logoGAL = (LogoGetAndLoad)e.Argument;
            BackgroundWorker sendingWorker = (BackgroundWorker)sender;

            if (logoGAL.SearchDBCount != 0 && !logoGAL.MoreResults)
            {
                getAllCompaniesImages(logoGAL, sendingWorker, e);
                e.Result = logoGAL;
            }
            else
            {
                List<string> imageURLs = GoogleSearch(logoGAL);

                for (int i = 0; i < imageURLs.Count; i++)
                {
                    if (!sendingWorker.CancellationPending)//At each iteration of the loop,                                       
                    {
                        LogoGetAndLoad tmpLogoGAL = getGoogleImages(logoGAL, imageURLs[i]);

                        if (tmpLogoGAL.ImgLogo != null)
                        {
                            logoGAL.SearchListGAL.Add(tmpLogoGAL);
                            e.Result = logoGAL;
                        }
                        else
                        {
                            e.Result = logoGAL;
                        }
                    }
                    else
                    {
                        e.Cancel = true;//If a cancellation request is pending, assign this flag a value of true
                    }
                }

                if (logoGAL.SecondSearch)
                {
                    logoGAL.SecondSearch = false;
                }
                else
                {
                    logoGAL.SecondSearch = true;
                }
            }
        }

        public void backgroundWorkerSearchCancel()
        {
            backgroundWorkerSearch.CancelAsync();
            backgroundWorkerSearch.Dispose();
        }

        private void backgroundWorkerSearch_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error == null)//Check if the worker has been canceled or if an error occurred
            {
                LogoGetAndLoad logoGAL = (LogoGetAndLoad)e.Result;
                if (logoGAL.SearchListGAL.Count > 0)
                {
                    if (logoGAL.MoreResults)
                    {
                        loadLogos(logoGAL, dataGridViewSearch);
                        dataGridViewSearch.ResumeLayout();
                        dataGridViewSearch.PerformLayout();
                        wpm.Close();
                        this.Enabled = true;

                        buttonMoreSearch.Visible = false;
                        MORE_RESULTS_SEARCH = false;
                        SECOND_SEARCH = logoGAL.SecondSearch;
                    }
                    else
                    {
                        loadLogos(logoGAL, dataGridViewSearch);
                        dataGridViewSearch.ResumeLayout();
                        dataGridViewSearch.PerformLayout();
                        wpm.Close();
                        this.Enabled = true;

                        buttonMoreSearch.Visible = true;
                        MORE_RESULTS_SEARCH = true;
                        SECOND_SEARCH = logoGAL.SecondSearch;
                    }
                }
                else
                {
                    MessageBox.Show("There were no logos matching your search.");
                    wpm.Close();
                    this.Enabled = true;

                    buttonMoreSearch.Visible = false;
                    MORE_RESULTS_SEARCH = false;
                    SECOND_SEARCH = false;
                }
            }
            else if (e.Cancelled)
            {
                wpm.Close();
                this.Enabled = true;
                textBoxSearch.Focus();

                buttonMoreSearch.Visible = false;
                MORE_RESULTS_SEARCH = false;
                SECOND_SEARCH = false;
            }
            else
            {
                wpm.Close();
                this.Enabled = true;
                textBoxSearch.Focus();

                buttonMoreSearch.Visible = false;
                MORE_RESULTS_SEARCH = false;
                SECOND_SEARCH = false;
            }

            Cursor.Current = Cursors.Default;
        }

        private LogoGetAndLoad getGoogleImages(LogoGetAndLoad logoGAL, string imageURL)
        {
            try
            {
                Guid guid = Guid.NewGuid();
                string uniqueStr = "LGBT_" + guid.ToString();

                Image tmpLogo = getImageFromUrl(imageURL);
                tmpLogo.Tag = uniqueStr;

                string tmpDetails = logoGAL.SearchCompanyStr + "\nWidth: " + tmpLogo.Width + "\nHeight: " + tmpLogo.Height;
                LogoGetAndLoad tmpLogoGAL = new LogoGetAndLoad(tmpLogo, tmpDetails);

                return tmpLogoGAL;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error occured: " + ex.Message);
                LogoGetAndLoad tmpLogoGAL = new LogoGetAndLoad(null, "No Logo");
                return tmpLogoGAL;
            }
        }

        private List<string> GoogleSearch(LogoGetAndLoad logoGAL)
        {
            List<string> imageURLs = new List<string>();
            LogoGetAndLoadJSON logoGALJ = new LogoGetAndLoadJSON();
            logoGALJ.CurrentUser = logoGAL.CurrentUser;
            logoGALJ.SearchStr = logoGAL.SearchStr;
            logoGALJ.SearchCompanyStr = logoGAL.SearchCompanyStr;
            logoGALJ.SecondSearch = logoGAL.SecondSearch;

            LogobotAPIClient apiClient = new LogobotAPIClient();

            using (var client = apiClient.getHTTPClient())
            {
                HttpResponseMessage response = client.PostAsJsonAsync("api/LogoSearch/googlesearch", logoGALJ).Result;
                if (response.IsSuccessStatusCode)
                {
                    imageURLs = response.Content.ReadAsAsync<List<string>>().Result;
                }
            }
            return imageURLs;
        }

        public static Image getImageFromUrl(string url)
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);

                using (HttpWebResponse httpWebReponse = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    using (Stream stream = httpWebReponse.GetResponseStream())
                    {
                        return Image.FromStream(stream);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error occured: " + ex.Message);
                return global::Logobot2_0.Properties.Resources.no_image;
            }
        }

        private void backgroundWorkerScreen_DoWork(object sender, DoWorkEventArgs e)
        {
            LogoGetAndLoad logoGAL = (LogoGetAndLoad)e.Argument;
            BackgroundWorker sendingWorker = (BackgroundWorker)sender;

            switch (logoGAL.ScreenDataSource.SourceName)
            {
                case "Public":  // Public Companies
                    {
                        getPublicCompaniesImages(logoGAL, sendingWorker, e);
                        break;
                    }
                case "Industry":  // Public Industries Companies
                    {
                        getPublicCompaniesImages(logoGAL, sendingWorker, e);
                        break;
                    }
                default:
                    {
                        getScreenImages(logoGAL, sendingWorker, e);
                        break;
                    }
            }
            e.Result = logoGAL;
        }

        public void backgroundWorkerScreenCancel()
        {
            backgroundWorkerScreen.CancelAsync();
            backgroundWorkerScreen.Dispose();
        }

        private void backgroundWorkerScreen_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error == null)//Check if the worker has been canceled or if an error occurred
            {
                LogoGetAndLoad logoGAL = (LogoGetAndLoad)e.Result;
                loadLogos(logoGAL, dataGridViewScreen);
                dataGridViewScreen.ResumeLayout();
                dataGridViewScreen.PerformLayout();
                wpm.Close();
                this.Enabled = true;
                buttonMoveAllScreen.Visible = true;
                buttonMoveSelectedScreen.Visible = true;
                buttonScreenBack.Visible = true;
                buttonScreen.Enabled = false;
            }
            else if (e.Cancelled)
            {
                wpm.Close();
                this.Enabled = true;
                treeViewScreen.Visible = true;
                dataGridViewScreen.Visible = false;
                dataGridViewScreen.ResumeLayout();
                dataGridViewScreen.PerformLayout();
                buttonScreenBack.Visible = false;
                buttonScreen.Enabled = true;
                buttonMoveAllScreen.Visible = false;
                buttonMoveSelectedScreen.Visible = false;
            }
            else
            {
                wpm.Close();
                this.Enabled = true;
                treeViewScreen.Visible = true;
                dataGridViewScreen.Visible = false;
                dataGridViewScreen.ResumeLayout();
                dataGridViewScreen.PerformLayout();
                buttonScreenBack.Visible = false;
                buttonScreen.Enabled = true;
                buttonMoveAllScreen.Visible = false;
                buttonMoveSelectedScreen.Visible = false;
            }
            Cursor.Current = Cursors.Default;
        }

        public static void getScreenCategories(TreeView tv, ScreenSources selectedSource)
        {
            tv.Visible = true;
            tv.Nodes.Clear();

            List<LogoGetAndLoadJSON> listLogoGALJ = new List<LogoGetAndLoadJSON>();
            LogoGetAndLoadJSON logoGALJ = new LogoGetAndLoadJSON();
            logoGALJ.ScreenDataSource = selectedSource;
            LogobotAPIClient apiClient = new LogobotAPIClient();

            TreeNode treeNodeSource = new TreeNode();
            treeNodeSource = tv.Nodes.Add(selectedSource.SourceName);
            treeNodeSource.ImageIndex = 3;
            treeNodeSource.SelectedImageIndex = 3;

            using (var client = apiClient.getHTTPClient())
            {
                HttpResponseMessage response = client.PostAsJsonAsync("api/LogoScreen/getscreencategories", logoGALJ).Result;
                if (response.IsSuccessStatusCode)
                {
                    listLogoGALJ = response.Content.ReadAsAsync<List<LogoGetAndLoadJSON>>().Result;
                }
            }

            foreach (var category in listLogoGALJ)
            {
                LogoGetAndLoadJSON tmpLogoGALJ = category as LogoGetAndLoadJSON;
                CustomTreeNode sourceCategory = new CustomTreeNode();
                sourceCategory.ImageIndex = 3;
                sourceCategory.SelectedImageIndex = 3;
                sourceCategory.Text = tmpLogoGALJ.Category + " (" + tmpLogoGALJ.CompaniesInCategory + ")";
                sourceCategory.UniqueValueKey = tmpLogoGALJ.Category;

                tv.Nodes[0].Nodes.Add(sourceCategory);
            }
            tv.ExpandAll();
        }

        private void getScreenImages(LogoGetAndLoad logoGAL, BackgroundWorker sendingWorker, DoWorkEventArgs e)
        {
            Image tmpLogo;
            int rowCount = 0;
            List<LogoGetAndLoadJSON> listLogoGALJ = new List<LogoGetAndLoadJSON>();
            LogoGetAndLoadJSON logoGALJ = new LogoGetAndLoadJSON();
            logoGALJ.ScreenNoLogo = logoGAL.ScreenNoLogo;
            logoGALJ.ScreenDataSource = logoGAL.ScreenDataSource;
            logoGALJ.ScreenWhereStr = logoGAL.ScreenWhereStr;

            if (!sendingWorker.CancellationPending)//At each iteration of the loop,                                       
            {
                LogobotAPIClient apiClient = new LogobotAPIClient();

                using (var client = apiClient.getHTTPClient())
                {
                    HttpResponseMessage response = client.PostAsJsonAsync("api/LogoScreen/getscreenlogos", logoGALJ).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        listLogoGALJ = response.Content.ReadAsAsync<List<LogoGetAndLoadJSON>>().Result;
                    }
                }

                foreach (var logo in listLogoGALJ)
                {
                    if (!sendingWorker.CancellationPending)
                    {
                        LogoGetAndLoadJSON tmpLogoGALJ = logo as LogoGetAndLoadJSON;

                        sendingWorker.ReportProgress(rowCount);

                        if (tmpLogoGALJ.ImageArray != null)
                        {
                            Guid guid = Guid.NewGuid();
                            string uniqueStr = "LGBT_" + guid.ToString();
                            MemoryStream ms = new MemoryStream(tmpLogoGALJ.ImageArray);
                            tmpLogo = Image.FromStream(ms);
                            tmpLogo.Tag = uniqueStr;
                        }
                        else
                        {
                            Guid guid = Guid.NewGuid();
                            string uniqueStr = "LGBT_" + guid.ToString();
                            tmpLogo = global::Logobot2_0.Properties.Resources.no_image;
                            tmpLogo.Tag = uniqueStr;
                        }
                        rowCount++;

                        string details = tmpLogoGALJ.CompanyDetails + "\nWidth: " + tmpLogo.Width + "\nHeight: " + tmpLogo.Height;
                        LogoGetAndLoad tmpLogoGAL = new LogoGetAndLoad(tmpLogo, details);
                        logoGAL.SearchListGAL.Add(tmpLogoGAL);
                    }
                    else
                    {
                        e.Cancel = true;
                        break;
                    }
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        public static void getPublicCompaniesCategories(TreeView tv, string countryName, ScreenSources selectedSource)
        {
            tv.Nodes.Clear();
            tv.Visible = true;

            List<PublicCompanyTree> sectorNames = new List<PublicCompanyTree>();
            List<LogoGetAndLoadJSON> listLogoGALJ = new List<LogoGetAndLoadJSON>();
            LogoGetAndLoadJSON logoGALJ = new LogoGetAndLoadJSON();
            logoGALJ.Country = countryName;
            logoGALJ.ScreenDataSource = selectedSource;
            LogobotAPIClient apiClient = new LogobotAPIClient();

            using (var client = apiClient.getHTTPClient())
            {
                HttpResponseMessage response = client.PostAsJsonAsync("api/LogoScreen/getscreencountrycategories", logoGALJ).Result;
                if (response.IsSuccessStatusCode)
                {
                    listLogoGALJ = response.Content.ReadAsAsync<List<LogoGetAndLoadJSON>>().Result;
                }
            }

            foreach (var sector in listLogoGALJ)
            {
                LogoGetAndLoadJSON tmpLogoGALJ = sector as LogoGetAndLoadJSON;
                PublicCompanyTree pct = new PublicCompanyTree();
                pct.Sector = tmpLogoGALJ.PrimarySector;
                pct.SectorCount = tmpLogoGALJ.PrimarySectorCount;
                sectorNames.Add(pct);
            }

            TreeNode treeNodePublicCompanies = new TreeNode();
            treeNodePublicCompanies = tv.Nodes.Add("Global Public Companies");
            treeNodePublicCompanies.ImageIndex = 4;
            treeNodePublicCompanies.SelectedImageIndex = 4;

            CustomTreeNode treeNodeCountry = new CustomTreeNode();
            treeNodeCountry.ImageIndex = 3;
            treeNodeCountry.SelectedImageIndex = 3;
            treeNodeCountry.Text = countryName;
            treeNodeCountry.UniqueValueKey = countryName;

            tv.Nodes[0].Nodes.Add(treeNodeCountry);

            for (int i = 0; i < sectorNames.Count; i++)
            {
                PublicCompanyTree tmp = (PublicCompanyTree)sectorNames[i];

                CustomTreeNode publicSector = new CustomTreeNode();
                publicSector.ImageIndex = 3;
                publicSector.SelectedImageIndex = 3;
                publicSector.Text = tmp.Sector + " (" + tmp.SectorCount + ")";
                publicSector.UniqueValueKey = tmp.Sector;

                tv.Nodes[0].Nodes[0].Nodes.Add(publicSector);

                logoGALJ.PrimarySector = tmp.Sector;
                List<LogoGetAndLoadJSON> listIndustryLogoGALJ = new List<LogoGetAndLoadJSON>();

                using (var client = apiClient.getHTTPClient())
                {
                    HttpResponseMessage response = client.PostAsJsonAsync("api/LogoScreen/getscreencountryindustrycategories", logoGALJ).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        listIndustryLogoGALJ = response.Content.ReadAsAsync<List<LogoGetAndLoadJSON>>().Result;
                    }
                }

                foreach (var industry in listIndustryLogoGALJ)
                {
                    LogoGetAndLoadJSON tmpInudstryLogoGALJ = industry as LogoGetAndLoadJSON;
                    CustomTreeNode publicIndustry = new CustomTreeNode();
                    publicIndustry.ImageIndex = 3;
                    publicIndustry.SelectedImageIndex = 3;
                    publicIndustry.Text = tmpInudstryLogoGALJ.PrimaryIndustry + " (" + tmpInudstryLogoGALJ.PrimaryIndustryCount + ")";
                    publicIndustry.UniqueValueKey = tmpInudstryLogoGALJ.PrimaryIndustry;

                    tv.Nodes[0].Nodes[0].Nodes[i].Nodes.Add(publicIndustry);
                }
            }
        }

        public static void getPublicCompaniesIndustryCategories(TreeView tv, string countryIndustryName, ScreenSources selectedSource)
        {
            tv.Nodes.Clear();
            tv.Visible = true;

            List<PublicCompanyTree> sectorNames = new List<PublicCompanyTree>();
            List<LogoGetAndLoadJSON> listLogoGALJ = new List<LogoGetAndLoadJSON>();
            LogoGetAndLoadJSON logoGALJ = new LogoGetAndLoadJSON();
            logoGALJ.PrimaryIndustry = countryIndustryName;
            logoGALJ.ScreenDataSource = selectedSource;
            LogobotAPIClient apiClient = new LogobotAPIClient();

            using (var client = apiClient.getHTTPClient())
            {
                HttpResponseMessage response = client.PostAsJsonAsync("api/LogoScreen/getscreenindustrycategories", logoGALJ).Result;
                if (response.IsSuccessStatusCode)
                {
                    listLogoGALJ = response.Content.ReadAsAsync<List<LogoGetAndLoadJSON>>().Result;
                }
            }

            foreach (var sector in listLogoGALJ)
            {
                LogoGetAndLoadJSON tmpLogoGALJ = sector as LogoGetAndLoadJSON;
                PublicCompanyTree pct = new PublicCompanyTree();
                pct.Sector = tmpLogoGALJ.PrimaryIndustry;
                pct.SectorCount = tmpLogoGALJ.PrimaryIndustryCount;
                sectorNames.Add(pct);
            }

            TreeNode treeNodePublicCompanies = new TreeNode();
            treeNodePublicCompanies = tv.Nodes.Add("Global Public Company Industries");
            treeNodePublicCompanies.ImageIndex = 4;
            treeNodePublicCompanies.SelectedImageIndex = 4;

            CustomTreeNode treeNodeSector = new CustomTreeNode();
            treeNodeSector.ImageIndex = 3;
            treeNodeSector.SelectedImageIndex = 3;
            treeNodeSector.Text = countryIndustryName;
            treeNodeSector.UniqueValueKey = countryIndustryName;

            tv.Nodes[0].Nodes.Add(treeNodeSector);

            for (int i = 0; i < sectorNames.Count; i++)
            {
                PublicCompanyTree tmp = (PublicCompanyTree)sectorNames[i];

                CustomTreeNode publicSector = new CustomTreeNode();
                publicSector.ImageIndex = 3;
                publicSector.SelectedImageIndex = 3;
                publicSector.Text = tmp.Sector + " (" + tmp.SectorCount + ")";
                publicSector.UniqueValueKey = tmp.Sector;

                tv.Nodes[0].Nodes[0].Nodes.Add(publicSector);
            }
        }

        private int getPublicCompaniesImagesCount(LogoGetAndLoad logoGAL)
        {
            int rowCount = 0;
            LogoGetAndLoadJSON logoGALJ = new LogoGetAndLoadJSON();
            logoGALJ.ScreenNoLogo = logoGAL.ScreenNoLogo;
            logoGALJ.ScreenDataSource = logoGAL.ScreenDataSource;
            logoGALJ.ScreenWhereStr = logoGAL.ScreenWhereStr;

            LogobotAPIClient apiClient = new LogobotAPIClient();

            using (var client = apiClient.getHTTPClient())
            {
                HttpResponseMessage response = client.PostAsJsonAsync("api/LogoScreen/getpubliccompaniesimagescount", logoGALJ).Result;
                if (response.IsSuccessStatusCode)
                {
                    rowCount = response.Content.ReadAsAsync<int>().Result;
                }
            }
            return rowCount;
        }

        private void getPublicCompaniesImages(LogoGetAndLoad logoGAL, BackgroundWorker sendingWorker, DoWorkEventArgs e)
        {
            Image tmpLogo;

            List<LogoGetAndLoadJSON> listLogoGALJ = new List<LogoGetAndLoadJSON>();
            LogoGetAndLoadJSON logoGALJ = new LogoGetAndLoadJSON();
            logoGALJ.ScreenNoLogo = logoGAL.ScreenNoLogo;
            logoGALJ.ScreenDataSource = logoGAL.ScreenDataSource;
            logoGALJ.ScreenWhereStr = logoGAL.ScreenWhereStr;

            if (!sendingWorker.CancellationPending)//At each iteration of the loop,                                       
            {
                LogobotAPIClient apiClient = new LogobotAPIClient();

                using (var client = apiClient.getHTTPClient())
                {
                    HttpResponseMessage response = client.PostAsJsonAsync("api/LogoScreen/getpubliccompanylogos", logoGALJ).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        listLogoGALJ = response.Content.ReadAsAsync<List<LogoGetAndLoadJSON>>().Result;
                    }
                }

                foreach (var logo in listLogoGALJ)
                {
                    if (!sendingWorker.CancellationPending)
                    {
                        LogoGetAndLoadJSON tmpLogoGALJ = logo as LogoGetAndLoadJSON;

                        if (tmpLogoGALJ.ImageArray != null)
                        {
                            Guid guid = Guid.NewGuid();
                            string uniqueStr = "LGBT_" + guid.ToString();
                            MemoryStream ms = new MemoryStream(tmpLogoGALJ.ImageArray);
                            tmpLogo = Image.FromStream(ms);
                            tmpLogo.Tag = uniqueStr;
                        }
                        else
                        {
                            Guid guid = Guid.NewGuid();
                            string uniqueStr = "LGBT_" + guid.ToString();
                            tmpLogo = global::Logobot2_0.Properties.Resources.no_image;
                            tmpLogo.Tag = uniqueStr;
                        }
                        string details = tmpLogoGALJ.CompanyDetails + "\nWidth: " + tmpLogo.Width + "\nHeight: " + tmpLogo.Height;
                        LogoGetAndLoad tmpLogoGAL = new LogoGetAndLoad(tmpLogo, details);
                        logoGAL.SearchListGAL.Add(tmpLogoGAL);
                    }
                    else
                    {
                        e.Cancel = true;
                        break;
                    }
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        private int getAllCompaniesImagesCount(LogoGetAndLoad logoGAL)
        {
            int rowCount = 0;
            LogoGetAndLoadJSON logoGALJ = new LogoGetAndLoadJSON();
            logoGALJ.ScreenNoLogo = logoGAL.ScreenNoLogo;
            logoGALJ.SearchCompanyStr = logoGAL.SearchCompanyStr;

            LogobotAPIClient apiClient = new LogobotAPIClient();

            using (var client = apiClient.getHTTPClient())
            {
                HttpResponseMessage response = client.PostAsJsonAsync("api/LogoScreen/getallcompaniesimagescount", logoGALJ).Result;
                if (response.IsSuccessStatusCode)
                {
                    rowCount = response.Content.ReadAsAsync<int>().Result;
                }
            }
            return rowCount;
        }

        private void getAllCompaniesImages(LogoGetAndLoad logoGAL, BackgroundWorker sendingWorker, DoWorkEventArgs e)
        {
            Image tmpLogo;
            int rowCount = 0;

            List<LogoGetAndLoadJSON> listLogoGALJ = new List<LogoGetAndLoadJSON>();
            LogoGetAndLoadJSON logoGALJ = new LogoGetAndLoadJSON();
            logoGALJ.ScreenNoLogo = logoGAL.ScreenNoLogo;
            logoGALJ.SearchCompanyStr = logoGAL.SearchCompanyStr;

            if (!sendingWorker.CancellationPending)//At each iteration of the loop,                                       
            {
                LogobotAPIClient apiClient = new LogobotAPIClient();

                using (var client = apiClient.getHTTPClient())
                {
                    HttpResponseMessage response = client.PostAsJsonAsync("api/LogoScreen/getallcompanylogos", logoGALJ).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        listLogoGALJ = response.Content.ReadAsAsync<List<LogoGetAndLoadJSON>>().Result;
                    }
                }

                foreach (var logo in listLogoGALJ)
                {
                    if (!sendingWorker.CancellationPending)
                    {
                        LogoGetAndLoadJSON tmpLogoGALJ = logo as LogoGetAndLoadJSON;

                        sendingWorker.ReportProgress(rowCount);

                        if (tmpLogoGALJ.ImageArray != null)
                        {
                            Guid guid = Guid.NewGuid();
                            string uniqueStr = "LGBT_" + guid.ToString();
                            MemoryStream ms = new MemoryStream(tmpLogoGALJ.ImageArray);
                            tmpLogo = Image.FromStream(ms);
                            tmpLogo.Tag = uniqueStr;
                        }
                        else
                        {
                            Guid guid = Guid.NewGuid();
                            string uniqueStr = "LGBT_" + guid.ToString();
                            tmpLogo = global::Logobot2_0.Properties.Resources.no_image;
                            tmpLogo.Tag = uniqueStr;
                        }
                        rowCount++;

                        string details = tmpLogoGALJ.CompanyDetails + "\nWidth: " + tmpLogo.Width + "\nHeight: " + tmpLogo.Height;
                        LogoGetAndLoad tmpLogoGAL = new LogoGetAndLoad(tmpLogo, details);
                        logoGAL.SearchListGAL.Add(tmpLogoGAL);
                    }
                    else
                    {
                        e.Cancel = true;
                        break;
                    }
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void backgroundWorkerUser_DoWork(object sender, DoWorkEventArgs e)
        {
            LogoGetAndLoad logoGAL = (LogoGetAndLoad)e.Argument;
            BackgroundWorker sendingWorker = (BackgroundWorker)sender;

            getUserBlotterLogos(logoGAL, sendingWorker, e);
            e.Result = logoGAL;
        }

        public void backgroundWorkerUserCancel()
        {
            backgroundWorkerUser.CancelAsync();
            backgroundWorkerUser.Dispose();
        }

        private void backgroundWorkerUser_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error == null)//Check if the worker has been canceled or if an error occurred
            {
                LogoGetAndLoad logoGAL = (LogoGetAndLoad)e.Result;
                loadLogos(logoGAL, dataGridViewUser);
                dataGridViewUser.ResumeLayout();
                dataGridViewUser.PerformLayout();
                wpm.Close();
                this.Enabled = true;
                buttonMoveAllUser.Visible = true;
                buttonMoveSelectedUser.Visible = true;
                buttonUserBack.Visible = true;
                buttonUserRefresh.Visible = false;
                buttonUser.Enabled = false;
            }
            else if (e.Cancelled)
            {
                wpm.Close();
                this.Enabled = true;
                treeViewUser.Visible = true;
                buttonUserRefresh.Visible = true;

                dataGridViewUser.Visible = false;
                dataGridViewUser.ResumeLayout();
                dataGridViewUser.PerformLayout();
                buttonUserBack.Visible = false;
                buttonMoveAllUser.Visible = false;
                buttonMoveSelectedUser.Visible = false;

                buttonUser.Enabled = true;
            }
            else
            {
                wpm.Close();
                this.Enabled = true;
                treeViewUser.Visible = true;
                buttonUserRefresh.Visible = true;

                dataGridViewUser.Visible = false;
                dataGridViewUser.ResumeLayout();
                dataGridViewUser.PerformLayout();
                buttonUserBack.Visible = false;
                buttonMoveAllUser.Visible = false;
                buttonMoveSelectedUser.Visible = false;

                buttonUser.Enabled = true;
            }
            Cursor.Current = Cursors.Default;
        }

        public static void getUserBlotters(TreeView tv, string userID)
        {
            tv.Visible = true;
            tv.Nodes.Clear();

            List<string> listUserBlotters = new List<string>();
            LogobotAPIClient apiClient = new LogobotAPIClient();

            using (var client = apiClient.getHTTPClient())
            {
                HttpResponseMessage response = client.PostAsJsonAsync("api/LogobotUser/getuserblotters", userID).Result;
                if (response.IsSuccessStatusCode)
                {
                    listUserBlotters = response.Content.ReadAsAsync<List<string>>().Result;
                }
            }
            TreeNode treeNodeUser = new TreeNode();
            treeNodeUser = tv.Nodes.Add("My Saved Blotters");
            treeNodeUser.ImageIndex = 3;
            treeNodeUser.SelectedImageIndex = 3;

            foreach (var blotter in listUserBlotters)
            {
                string tmpBlotter = blotter as string;
                CustomTreeNode userBlotter = new CustomTreeNode();
                userBlotter.ImageIndex = 3;
                userBlotter.SelectedImageIndex = 3;
                userBlotter.Text = tmpBlotter;
                userBlotter.UniqueValueKey = tmpBlotter;

                tv.Nodes[0].Nodes.Add(userBlotter);
            }
            tv.ExpandAll();
        }

        private void getUserBlotterLogos(LogoGetAndLoad logoGAL, BackgroundWorker sendingWorker, DoWorkEventArgs e)
        {
            Image tmpLogo;
            int rowCount = 0;
            List<LogoGetAndLoadJSON> listLogoGALJ = new List<LogoGetAndLoadJSON>();
            LogoGetAndLoadJSON logoGALJ = new LogoGetAndLoadJSON();
            logoGALJ.ScreenWhereStr = logoGAL.ScreenWhereStr;

            if (!sendingWorker.CancellationPending)//At each iteration of the loop,                                       
            {
                LogobotAPIClient apiClient = new LogobotAPIClient();

                using (var client = apiClient.getHTTPClient())
                {
                    HttpResponseMessage response = client.PostAsJsonAsync("api/LogobotUser/getuserblotterlogos", logoGALJ).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        listLogoGALJ = response.Content.ReadAsAsync<List<LogoGetAndLoadJSON>>().Result;
                    }
                }

                foreach (var logo in listLogoGALJ)
                {
                    if (!sendingWorker.CancellationPending)
                    {
                        LogoGetAndLoadJSON tmpLogoGALJ = logo as LogoGetAndLoadJSON;

                        sendingWorker.ReportProgress(rowCount);

                        if (tmpLogoGALJ.ImageArray != null)
                        {
                            Guid guid = Guid.NewGuid();
                            string uniqueStr = "LGBT_" + guid.ToString();
                            MemoryStream ms = new MemoryStream(tmpLogoGALJ.ImageArray);
                            tmpLogo = Image.FromStream(ms);
                            tmpLogo.Tag = uniqueStr;
                        }
                        else
                        {
                            Guid guid = Guid.NewGuid();
                            string uniqueStr = "LGBT_" + guid.ToString();
                            tmpLogo = global::Logobot2_0.Properties.Resources.no_image;
                            tmpLogo.Tag = uniqueStr;
                        }
                        rowCount++;

                        string details = tmpLogoGALJ.CompanyDetails + "\nWidth: " + tmpLogo.Width + "\nHeight: " + tmpLogo.Height;
                        LogoGetAndLoad tmpLogoGAL = new LogoGetAndLoad(tmpLogo, details);
                        logoGAL.SearchListGAL.Add(tmpLogoGAL);
                    }
                    else
                    {
                        e.Cancel = true;
                        break;
                    }
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void buttonUserRefresh_Click(object sender, EventArgs e)
        {
            if (currentUser == null)
            {
                currentUser = module.getSetCurrentUser;
            }

            getUserBlotters(this.treeViewUser, currentUser.Username);
        }

        private void buttonMoveAllUser_Click(object sender, EventArgs e)
        {
            MoveAll(dataGridViewUser);
        }

        private void buttonMoveAllScreen_Click(object sender, EventArgs e)
        {
            MoveAll(dataGridViewScreen);
        }

        private void buttonMoveAll_Click(object sender, EventArgs e)
        {
            MoveAll(dataGridViewSearch);
        }

        private void buttonMoveSelectedUser_Click(object sender, EventArgs e)
        {
            MoveSelected(dataGridViewUser);
        }

        private void buttonMoveSelectedScreen_Click(object sender, EventArgs e)
        {
            MoveSelected(dataGridViewScreen);
        }

        private void buttonMoveSelected_Click(object sender, EventArgs e)
        {
            MoveSelected(dataGridViewSearch);
        }

        public void MoveAll(DataGridView dgv)
        {
            int startRow = dataGridViewBlotter.Rows.Count;
            Cursor.Current = Cursors.WaitCursor;

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                dataGridViewBlotter.Rows.Add();
                dataGridViewBlotter[0, startRow].Value = dgv.Rows[i].Cells[0].Value;
                dataGridViewBlotter[1, startRow].Value = dgv.Rows[i].Cells[1].Value;
                startRow = dataGridViewBlotter.Rows.Count;
            }

            clearAllDGV(dgv);
            Cursor.Current = Cursors.Default;
        }

        public void MoveSelected(DataGridView dgv)
        {
            int startRow = dataGridViewBlotter.Rows.Count;
            Cursor.Current = Cursors.WaitCursor;
            if (dgv.SelectedRows.Count != 0)
            {
                for (int i = 0; i < dgv.SelectedRows.Count; i++)
                {
                    dataGridViewBlotter.Rows.Add();
                    dataGridViewBlotter[0, startRow].Value = dgv.SelectedRows[i].Cells[0].Value;
                    dataGridViewBlotter[1, startRow].Value = dgv.SelectedRows[i].Cells[1].Value;
                    startRow = dataGridViewBlotter.Rows.Count;
                }

                clearSelectedDGV(dgv);
                Cursor.Current = Cursors.Default;
            }
            else
            {
                MessageBox.Show("You didn't select any rows.");
                Cursor.Current = Cursors.Default;
            }
        }

        private void dataGridViewUser_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ZoomDoubleClick(dataGridViewUser, e);
        }

        private void dataGridViewSearch_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ZoomDoubleClick(dataGridViewSearch, e);
        }

        private void dataGridViewBlotter_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ZoomDoubleClick(dataGridViewBlotter, e);
        }

        private void dataGridViewScreen_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ZoomDoubleClick(dataGridViewScreen, e);
        }

        private void ZoomDoubleClick(DataGridView dgv, DataGridViewCellEventArgs e)
        {
            try
            {
                PictureBox zoomPic = new PictureBox();
                zoomPic.Image = dgv[0, e.RowIndex].Value as Image;

                Zoom_Popup pictureZoom = new Zoom_Popup(zoomPic);
                pictureZoom.ShowDialog();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void buttonClearAll_Click(object sender, EventArgs e)
        {
            clearAllDGV(dataGridViewBlotter);
        }

        private void buttonClearSelected_Click(object sender, EventArgs e)
        {
            clearSelectedDGV(dataGridViewBlotter);
        }

        private void clearAllDGV(DataGridView DGV)
        {
            for (int i = DGV.Rows.Count - 1; i >= 0; i--)
            {
                DGV.Rows.Remove(DGV.Rows[i]);
            }
        }

        private void clearSelectedDGV(DataGridView DGV)
        {
            Int32 selectedRowCount = DGV.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                for (int i = 0; i < selectedRowCount; i++)
                {
                    DGV.Rows.RemoveAt(DGV.SelectedRows[0].Index);
                }
            }
        }

        private void buttonCopyToSlide_Click(object sender, EventArgs e)
        {
            Int32 selectedRowCount = dataGridViewBlotter.Rows.GetRowCount(DataGridViewElementStates.Selected);
            String tempFilePath = Path.GetTempPath();
            PowerPoint._Application app = (PowerPoint._Application)this.PowerPointAppObj; 
            PowerPoint.DocumentWindow activeWindow = null;
            PowerPoint.View activeView = null;
            PowerPoint.Slide currentSlide = null;
            int xLoc = 50;
            int yLoc = 25;

            try
            {
                activeWindow = app.ActiveWindow;
                activeView = activeWindow.View;
                currentSlide = (PowerPoint.Slide)activeView.Slide;

                if (selectedRowCount > 0)
                {
                    for (int i = 0; i < dataGridViewBlotter.Rows.Count; i++)
                    {
                        if (dataGridViewBlotter.Rows[i].Selected)
                        {
                            Image tmpImage = (Image)dataGridViewBlotter.Rows[i].Cells[0].Value;

                            if (!File.Exists(tempFilePath + tmpImage.Tag))
                            {
                                tmpImage.Save(tempFilePath + tmpImage.Tag, ImageFormat.Png);
                            }

                            PowerPoint.Shape imageShape = currentSlide.Shapes.AddPicture(tempFilePath + tmpImage.Tag, Microsoft.Office.Core.MsoTriState.msoFalse,
                                    Microsoft.Office.Core.MsoTriState.msoTrue, xLoc, yLoc, tmpImage.Width, tmpImage.Height);
                        }
                        xLoc = xLoc + 10;
                        yLoc = yLoc + 10;
                    }
                }
                else
                {
                    MessageBox.Show("You didn't select any logos.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem creating your logo fill box.  Are you sure you have an open slide?");
                Debug.WriteLine(ex.Message);
            }
        }

        private void buttonFillLogos_Click(object sender, EventArgs e)
        {
            FillLogos(true);
        }

        private void buttonFillSelectedLogos_Click(object sender, EventArgs e)
        {
            FillLogos(false);
        }

        private void FillLogos(bool fillAllBlotter)
        {
            Int32 selectedRowCount = dataGridViewBlotter.Rows.GetRowCount(DataGridViewElementStates.Selected);
            String tempFilePath = Path.GetTempPath();
            Cursor.Current = Cursors.WaitCursor;

            PowerPoint.Selection currentSelection = null;
            PowerPoint._Application app = (PowerPoint._Application)this.PowerPointAppObj;
            PowerPoint.DocumentWindow activeWindow = null;
            PowerPoint.View activeView = null;
            PowerPoint.Slide currentSlide = null;

            try
            {
                activeWindow = app.ActiveWindow;
                activeView = activeWindow.View;
                currentSlide = (PowerPoint.Slide)activeView.Slide;

                if (selectedRowCount > 0)
                {
                    currentSelection = activeWindow.Selection;
                    if (currentSelection != null && currentSelection.Type == PowerPoint.PpSelectionType.ppSelectionShapes) //&& currentSelection.ShapeRange.i.Name == "LGBT_BLOTTER")
                    {
                        if (currentSelection.ShapeRange.Count == 1)
                        {
                            foreach (var arrangeShape in currentSelection.ShapeRange)
                            {
                                PowerPoint.Shape eventShape = arrangeShape as PowerPoint.Shape;
                                if (eventShape.AutoShapeType == Microsoft.Office.Core.MsoAutoShapeType.msoShapeRectangle)
                                {
                                    Logobot2_0.AddinModule.CurrentInstance.PrepareLogoRectangleArrangement(eventShape, fillAllBlotter);
                                }
                                else if (eventShape.AutoShapeType == Microsoft.Office.Core.MsoAutoShapeType.msoShapeOval)
                                {
                                    Logobot2_0.AddinModule.CurrentInstance.PrepareLogoOvalArrangement(eventShape, fillAllBlotter);
                                }

                            }
                        }
                        else
                        {
                            MessageBox.Show("You have selected more then one shape. Select only one shape to fill and try again.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("You have to select the Logobot arrangement shape to fill it with logos.");
                    }
                }
                else
                {
                    MessageBox.Show("You didn't select any logos.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem creating your logo fill box.  Are you sure you have an open slide?");
                Debug.WriteLine(ex.Message);
            }

            Cursor.Current = Cursors.Default;
        }

        private void buttonArrange_Click(object sender, EventArgs e)
        {
            string arrangeShape = Logobot2_0.AddinModule.CurrentInstance.arrangeShape;

            PowerPoint._Application app = (PowerPoint._Application)this.PowerPointAppObj;
            PowerPoint.DocumentWindow activeWindow = null;
            PowerPoint.View activeView = null;
            PowerPoint.Slide currentSlide = null;
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                activeWindow = app.ActiveWindow;
                activeView = activeWindow.View;
                currentSlide = (PowerPoint.Slide)activeView.Slide;

                if (arrangeShape == "Box - Uniform")
                {
                    PowerPoint.Shape selectShape = currentSlide.Shapes.AddShape(Microsoft.Office.Core.MsoAutoShapeType.msoShapeRectangle, 200, 100, 287, 100);
                    selectShape.BackgroundStyle = Microsoft.Office.Core.MsoBackgroundStyleIndex.msoBackgroundStylePreset1;
                    selectShape.Line.DashStyle = Microsoft.Office.Core.MsoLineDashStyle.msoLineDash;
                    selectShape.Line.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
                    selectShape.Line.ForeColor.RGB = Color.FromArgb(111, 154, 169).ToArgb();
                    selectShape.Name = "LGBT_BLOTTER";
                }
                else if (arrangeShape == "Box - Best Fit")
                {
                    PowerPoint.Shape selectShape = currentSlide.Shapes.AddShape(Microsoft.Office.Core.MsoAutoShapeType.msoShapeRectangle, 200, 100, 287, 100);
                    selectShape.BackgroundStyle = Microsoft.Office.Core.MsoBackgroundStyleIndex.msoBackgroundStylePreset1;
                    selectShape.Line.DashStyle = Microsoft.Office.Core.MsoLineDashStyle.msoLineDash;
                    selectShape.Line.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
                    selectShape.Line.ForeColor.RGB = Color.FromArgb(111, 154, 169).ToArgb();
                    selectShape.Name = "LGBT_BLOTTER";
                }

                else if (arrangeShape == "Circle")
                {
                    PowerPoint.Shape selectShape = currentSlide.Shapes.AddShape(Microsoft.Office.Core.MsoAutoShapeType.msoShapeOval, 200, 100, 300, 300);
                    selectShape.BackgroundStyle = Microsoft.Office.Core.MsoBackgroundStyleIndex.msoBackgroundStylePreset1;
                    selectShape.Line.DashStyle = Microsoft.Office.Core.MsoLineDashStyle.msoLineDash;
                    selectShape.Line.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
                    selectShape.Line.ForeColor.RGB = Color.FromArgb(111, 154, 169).ToArgb();
                    selectShape.Name = "LGBT_BLOTTER";
                }

                if (SHOW_ARRANGE_TIP)
                {
                    ArrangeResize arz = new ArrangeResize();
                    arz.Show(this);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("There was a problem creating your logo fill box.  Are you sure you have an open slide?");
                Debug.WriteLine(ex.Message);
            }
            Cursor.Current = Cursors.Default;
        }

        public static void setDataGridViewRowSize(DataGridView tmpDGV, string size)
        {
            switch (size)
            {
                case "Small": // 60
                    {
                        tmpDGV.RowTemplate.Height = 60;
                        tmpDGV.Update();
                        tmpDGV.Refresh();
                        break;
                    }
                case "Medium":  // 100
                    {
                        tmpDGV.RowTemplate.Height = 80;
                        tmpDGV.Update();
                        tmpDGV.Refresh();
                        break;
                    }
                case "Large":  // 120
                    {
                        tmpDGV.RowTemplate.Height = 120;
                        tmpDGV.Update();
                        tmpDGV.Refresh();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void LogobotPaneMain_ADXCloseButtonClick(object sender, ADXCloseButtonClickEventArgs e)
        {
            Logobot2_0.AddinModule.CurrentInstance.ShowHidePaneButton();
        }

        private void buttonSearchOpenFile_Click(object sender, EventArgs e)
        {
            if (module.checkForInternetConnection())
            {
                int resultsCount = 0;
                List<string> searchCompanyNames = new List<string>();
                DialogResult result = openFileDialogSearch.ShowDialog();
                if (result == DialogResult.OK) // Test result.
                {
                    string file = openFileDialogSearch.FileName;
                    try
                    {
                        FileStream stream = File.Open(file, FileMode.Open, FileAccess.Read);
                        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

                        excelReader.IsFirstRowAsColumnNames = false;
                        DataSet xlResult = excelReader.AsDataSet();
                        resultsCount = xlResult.Tables[0].Rows.Count;

                        if (resultsCount > 25)
                        {
                            DialogResult dialogResult = MessageBox.Show("You have over 25 company names in your file.  It could take a while to get all the logos.", "Warning", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                while (excelReader.Read())
                                {
                                    //Debug.WriteLine("Company Name: " + excelReader.GetString(0));
                                    if (excelReader.GetString(0) != "")
                                    {
                                        searchCompanyNames.Add(excelReader.GetString(0));
                                    }
                                }
                                excelReader.Close();
                                wpm = new WorkerProgressMarquee();
                                wpm.setLabelText("Logobot is getting your logos:");
                                wpm.Show(this);
                                this.Enabled = false;

                                System.Threading.SynchronizationContext.SetSynchronizationContext(new WindowsFormsSynchronizationContext());
                                backgroundWorkerMultiSearch.RunWorkerAsync(searchCompanyNames);
                            }
                        }
                        else
                        {
                            while (excelReader.Read())
                            {
                                if (excelReader.GetString(0) != "")
                                {
                                    searchCompanyNames.Add(excelReader.GetString(0));
                                }
                            }
                            excelReader.Close();
                            wpm = new WorkerProgressMarquee();
                            wpm.setLabelText("Logobot is getting your logos:");
                            wpm.Show(this);
                            this.Enabled = false;

                            System.Threading.SynchronizationContext.SetSynchronizationContext(new WindowsFormsSynchronizationContext());
                            backgroundWorkerMultiSearch.RunWorkerAsync(searchCompanyNames);
                        }
                    }
                    catch (IOException ex)
                    {
                        Debug.WriteLine("Error opening Excel: " + ex.Message);
                        MessageBox.Show("There has been a problem reading the Excel file you have chosen.  Make sure it is closed.");
                    }
                }
            }
            else
            {
                MessageBox.Show("There doesn't seem to be an internet connection.  Check you have internet access and click the Activate button.");
                this.Enabled = false;
                module.disableLogobot(false);
            }
        }

        private void backgroundWorkerMultiSearch_DoWork(object sender, DoWorkEventArgs e)
        {
            List<string> searchCompanyNames = (List<string>)e.Argument;
            List<LogoGetAndLoad> multiSearchResults = new List<LogoGetAndLoad>();
            BackgroundWorker sendingWorker = (BackgroundWorker)sender;

            for (int i = 0; i < searchCompanyNames.Count; i++)
            {
                if (!sendingWorker.CancellationPending)//At each iteration of the loop,                                       
                {
                    LogoGetAndLoad logoGAL = new LogoGetAndLoad();
                    logoGAL.SearchStr = searchCompanyNames[i];
                    logoGAL.SecondSearch = false;
                    List<string> imageURLs = GoogleSearch(logoGAL);
                    logoGAL.SearchCompanyStr = searchCompanyNames[i];

                    if (imageURLs.Count != 0)
                    {
                        foreach (string url in imageURLs)
                        {
                            LogoGetAndLoad tmpLogoGAL = getGoogleImages(logoGAL, url);

                            if (tmpLogoGAL.ImgLogo != null)
                            {
                                logoGAL.SearchListGAL.Add(tmpLogoGAL);
                            }
                        }
                    }
                    multiSearchResults.Add(logoGAL);
                }
                else
                {
                    e.Cancel = true;//If a cancellation request is pending, assign this flag a value of true
                }
            }
            e.Result = multiSearchResults;
        }

        public void backgroundWorkerMultiSearchCancel()
        {
            backgroundWorkerUser.CancelAsync();
            backgroundWorkerUser.Dispose();
        }

        private void backgroundWorkerMultiSearch_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error == null)//Check if the worker has been canceled or if an error occurred
            {
                List<LogoGetAndLoad> multiSearchResults = (List<LogoGetAndLoad>)e.Result;
                wpm.Close();
                this.Enabled = true;
                MultipleSearchForm multiForm = new MultipleSearchForm(multiSearchResults, this);
                multiForm.Show(this);
            }
            else if (e.Cancelled)
            {
                wpm.Close();
                this.Enabled = true;
            }
            else
            {
                wpm.Close();
                this.Enabled = true;
            }
            Cursor.Current = Cursors.Default;
        }

        private void dataGridViewBlotter_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridViewRow rw;
            Image dropImage;
            if (dataGridViewBlotter.SelectedRows.Count == 1)
            {
                if (e.Button == MouseButtons.Left && e.Clicks ==1)
                {
                    rw = dataGridViewBlotter.SelectedRows[0];
                    //rw.Cells[0].Style.BackColor = Color.Transparent;
                    Image tmpImage = (Image)rw.Cells[0].Value;

                    ImageFactory recolorImage = new ImageFactory(preserveExifData: true);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (MemoryStream outStream = new MemoryStream())
                        {
                            // Load, resize, set the format and quality and save an image.
                            tmpImage.Save(ms, ImageFormat.Png);
                            recolorImage.Load(ms.ToArray());
                            recolorImage.BackgroundColor(Color.White);
                            recolorImage.Save(outStream);
                            dropImage = Image.FromStream(outStream);
                        }
                    }
                    dataGridViewBlotter.DoDragDrop(dropImage, DragDropEffects.Copy);
                }
            }
        }

    }
}
