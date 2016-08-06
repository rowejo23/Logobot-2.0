using System;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Windows.Forms;
using AddinExpress.MSO;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using AddinExpress.PP;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using System.Net;
using ImageProcessor;
using ImageProcessor.Imaging;
using System.Net.Http;
using System.Data;

namespace Logobot2_0
{
    /// <summary>
    ///   Add-in Express Add-in Module
    /// </summary>
    [GuidAttribute("6067D0AA-03A4-4318-A3DE-D20101BF506E"), ProgId("Logobot2_0.AddinModule")]
    public partial class AddinModule : AddinExpress.MSO.ADXAddinModule
    {
        public LogobotPaneInstance lgbtPane;
        public int arrangePadding = 0;
        public string arrangeShape = "Box - Uniform";
        public string logobot_email = "info@logobot.co.uk";
        WorkerProgressMarquee wp;
        private static List<ScreenSources> screenSources;
        private static List<string> screenCountries;
        private static List<string> screenSectors;
        private static bool SCREEN_NO_LOGO = false;
        private static bool SHOW_ARRANGE_TIP = true;
        private static DataTable companyDT;
        private static List<Image> selectedLogosForArrangement;
        public bool disableLogobotPane = false;
        public bool noInternetConnection = false;
        public bool suppressErrors = true;

        private string username;
        private string password;
        private string machineName;

        private LogobotSettings userSettings = new LogobotSettings();
        private string path;
        private string pathXmlDoc;
        private LogobotUser currentUser;

        public AddinModule()
        {
            Application.EnableVisualStyles();
            InitializeComponent();
            // Please add any initialization code to the AddinInitialize event handler
        }

        #region Add-in Express automatic code

        // Required by Add-in Express - do not modify
        // the methods within this region

        public override System.ComponentModel.IContainer GetContainer()
        {
            if (components == null)
                components = new System.ComponentModel.Container();
            return components;
        }

        [ComRegisterFunctionAttribute]
        public static void AddinRegister(Type t)
        {
            AddinExpress.MSO.ADXAddinModule.ADXRegister(t);
        }

        [ComUnregisterFunctionAttribute]
        public static void AddinUnregister(Type t)
        {
            AddinExpress.MSO.ADXAddinModule.ADXUnregister(t);
        }

        public override void UninstallControls()
        {
            base.UninstallControls();
        }

        #endregion

        public static new AddinModule CurrentInstance
        {
            get
            {
                return AddinExpress.MSO.ADXAddinModule.CurrentInstance as AddinModule;
            }
        }

        public PowerPoint._Application PowerPointApp
        {
            get
            {
                return (HostApplication as PowerPoint._Application);
            }
        }

        public List<ScreenSources> getSetSCREEN_DATA_SOURCES
        {
            get { return screenSources; }
        }

        public List<string> getSetSCREEN_COUNTRIES
        {
            get { return screenCountries; }
        }

        public List<string> getSetSCREEN_SECTORS
        {
            get { return screenSectors; }
        }

        public bool getSCREEN_NO_LOGO
        {
            get { return SCREEN_NO_LOGO; }
        }

        public bool getSHOW_ARRANGE_TIP
        {
            get { return SHOW_ARRANGE_TIP; }
        }

        public DataTable getCOMPANYDT
        {
            get { return companyDT; }
        }

        public LogobotUser getSetCurrentUser
        {
            get { return currentUser; }
            set
            {
                currentUser = value;
                username = currentUser.Username;
                password = currentUser.HashPassword;
                machineName = currentUser.ComputerName;
            }
        }

        public LogobotSettings getUserSettings
        {
            get { return userSettings; }
        }

        private void AddinModule_AddinInitialize(object sender, EventArgs e)
        {
            // Get "My Documents" folder
            path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            path = Path.Combine(path, "Logobot");
            pathXmlDoc = Path.Combine(path, "LogobotSettings.xml");

            // Check for an Internet Connection
            if (!checkForInternetConnection())
            {
                disableLogobot(false);
                noInternetConnection = true;
            }
            else
            {
                // Select certain items in drop-down menus
                this.adxRibbonDropDownLogoSize.SelectedItemIndex = 0;
                this.adxRibbonDropDownArrangeSpacing.SelectedItemIndex = 2;
                this.adxRibbonDropDownArrangeShape.SelectedItemIndex = 1;

                // Load up Screen Menu Items into a List of Objects
                getScreenSources();
                getScreenCountries();
                getScreenSectors();

                // Load up DataTable for the AutoComplete in Search menu
                companyDT = new DataTable();
                companyDT = fillAutoCompleteFromSqlServer();

                // Create folder if it doesn't already exist
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    disableLogobot(false);
                }
                else
                {
                    if (File.Exists(pathXmlDoc))
                    {
                        userSettings.Load(pathXmlDoc);
                        username = userSettings.Read("username", null);
                        password = userSettings.Read("password", null);
                        SCREEN_NO_LOGO = userSettings.Read("noLogo", false);
                        SHOW_ARRANGE_TIP = userSettings.Read("showArrangeTip", true);

                        currentUser = new LogobotUser(username, password);

                        // Check for SCREEN_NO_LOGO values
                        if (SCREEN_NO_LOGO)
                        {
                            this.adxRibbonCheckBoxNoLogos.Pressed = SCREEN_NO_LOGO;
                        }

                        if (username != null && password != null)
                        {
                            loginUser(currentUser);

                            if (!currentUser.LoginResult)
                            {
                                disableLogobot(false);
                            }
                            else
                            {
                                if (currentUser.ComputerName != Environment.MachineName)
                                {
                                    disableLogobot(false);
                                }
                                else
                                {
                                    if (currentUser.PaidSubscriber)
                                    {
                                        if ((DateTime.Now - currentUser.PaidDate).TotalDays > 365)
                                        {
                                            string licenseMessage = "Your Logobot license has expired.  Click the My Account button to buy another year's subscription.";
                                            TrialPopup trial = new TrialPopup(licenseMessage);
                                            trial.ShowDialog();
                                            disableLogobot(false);
                                        }
                                    }
                                    else
                                    {
                                        string trialMessage;
                                        if ((DateTime.Now - currentUser.InstallDate).TotalDays <= 30)
                                        {
                                            trialMessage = "Your Logobot trial version will expire in " + (30 - Convert.ToInt32((DateTime.Now - currentUser.InstallDate).TotalDays)) + " days.  Click the My Account button to buy a full version.";
                                            TrialPopup trial = new TrialPopup(trialMessage);
                                            trial.ShowDialog();
                                        }
                                        else
                                        {
                                            trialMessage = "Your Logobot trial version has expired.  Click the My Account button to buy a full version.";
                                            TrialPopup trial = new TrialPopup(trialMessage);
                                            trial.ShowDialog();
                                            disableLogobot(false);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            disableLogobot(false);
                        }
                    }
                    else
                    {
                        disableLogobot(false);
                    }
                }
            }
        }

        private void getScreenSources()
        {
            screenSources = new List<ScreenSources>();
            LogobotAPIClient apiClient = new LogobotAPIClient();

            using (var client = apiClient.getHTTPClient())
            {
                HttpResponseMessage response = client.GetAsync("api/LogoScreen/screensources").Result;
                if (response.IsSuccessStatusCode)
                {
                    screenSources = response.Content.ReadAsAsync<List<ScreenSources>>().Result;
                }
            }
        }

        private void getScreenCountries()
        {
            screenCountries = new List<string>();
            LogobotAPIClient apiClient = new LogobotAPIClient();

            using (var client = apiClient.getHTTPClient())
            {
                HttpResponseMessage response = client.GetAsync("api/LogoScreen/screencountries").Result;
                if (response.IsSuccessStatusCode)
                {
                    screenCountries = response.Content.ReadAsAsync<List<string>>().Result;
                }
            }
        }

        private void getScreenSectors()
        {
            screenSectors = new List<string>();
            LogobotAPIClient apiClient = new LogobotAPIClient();

            using (var client = apiClient.getHTTPClient())
            {
                HttpResponseMessage response = client.GetAsync("api/LogoScreen/screensectors").Result;
                if (response.IsSuccessStatusCode)
                {
                    screenSectors = response.Content.ReadAsAsync<List<string>>().Result;
                }
            }
        }

        private DataTable fillAutoCompleteFromSqlServer()
        {
            DataTable dt = new DataTable();
            LogobotAPIClient apiClient = new LogobotAPIClient();

            using (var client = apiClient.getHTTPClient())
            {
                HttpResponseMessage response = client.GetAsync("api/LogoScreen/searchautocomplete").Result;
                if (response.IsSuccessStatusCode)
                {
                    dt = response.Content.ReadAsAsync<DataTable>().Result;
                }
                else
                {
                    dt = null;
                }
            }
            return dt;
        }

        public bool checkForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead("http://www.google.com"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        private void loginLogobot()
        {
            Cursor.Current = Cursors.WaitCursor;

            if (!checkForInternetConnection())
            {
                disableLogobot(false);
                MessageBox.Show("There doesn't seem to be an internet connection.  Check you have internet access and click the Activate button.");
                Cursor.Current = Cursors.Default;
            }
            else
            {
                if (username != null && password != null)
                {
                    currentUser = new LogobotUser(username, password);
                    loginUser(currentUser);

                    if (!currentUser.LoginResult)
                    {
                        Cursor.Current = Cursors.Default;
                        LoginForm frmLogin = new LoginForm(this);
                        frmLogin.ShowDialog();
                        if (frmLogin.CancelledForm)
                        {
                            disableLogobot(false);
                        }
                    }
                    else if(currentUser.InstallDate == DateTime.MinValue && (currentUser.ComputerName == null || currentUser.ComputerName == ""))
                    {
                        Cursor.Current = Cursors.Default;
                        LoginForm frmLogin = new LoginForm(this);
                        frmLogin.ShowDialog();
                        if (frmLogin.CancelledForm)
                        {
                            disableLogobot(false);
                        }
                    }
                    else
                    {
                        if (currentUser.ComputerName != Environment.MachineName)
                        {
                            MessageBox.Show("Your username and password are not registered to work on this computer");
                            disableLogobot(false);
                            Cursor.Current = Cursors.Default;
                        }
                        else
                        {
                            if (currentUser.PaidSubscriber)
                            {
                                if ((DateTime.Now - currentUser.PaidDate).TotalDays > 365)
                                {
                                    string licenseMessage = "Your Logobot license has expired.  Click the My Account button to buy another year's subscription.";
                                    TrialPopup trial = new TrialPopup(licenseMessage);
                                    trial.ShowDialog();
                                    disableLogobot(false);
                                }
                                else
                                {
                                    enableLogobot(currentUser.Username);
                                    Cursor.Current = Cursors.Default;
                                }
                            }
                            else
                            {
                                string trialMessage;
                                if ((DateTime.Now - currentUser.InstallDate).TotalDays <= 30)
                                {
                                    trialMessage = "Your Logobot trial version will expire in " + (30 - Convert.ToInt32((DateTime.Now - currentUser.InstallDate).TotalDays)) + " days.  Click the My Account button to buy a full version.";
                                    TrialPopup trial = new TrialPopup(trialMessage);
                                    trial.ShowDialog();
                                    enableLogobot(currentUser.Username);
                                    Cursor.Current = Cursors.Default;
                                }
                                else
                                {
                                    trialMessage = "Your Logobot trial version has expired.  Click the My Account button to buy a full version.";
                                    TrialPopup trial = new TrialPopup(trialMessage);
                                    trial.ShowDialog();
                                    disableLogobot(false);
                                }
                            }
                        }
                    }
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    LoginForm frmLogin = new LoginForm(this);
                    frmLogin.ShowDialog();
                    if (frmLogin.CancelledForm)
                    {
                        disableLogobot(false);
                    }
                }
            }
        }

        private void loginUser(LogobotUser currentUser)
        {
            currentUser.LoginResult = false;
            LogobotAPIClient apiClient = new LogobotAPIClient();

            using (var client = apiClient.getHTTPClient())
            {
                HttpResponseMessage response = client.PostAsJsonAsync<LogobotUser>("api/LogobotUser/userlogin", currentUser).Result;
                if (response.IsSuccessStatusCode)
                {
                    LogobotUser tmpUser = new LogobotUser();
                    tmpUser = response.Content.ReadAsAsync<LogobotUser>().Result;
                    if (tmpUser != null)
                    {
                        currentUser.LoginResult = tmpUser.LoginResult;
                        currentUser.ComputerName = tmpUser.ComputerName;
                        currentUser.PaidSubscriber = tmpUser.PaidSubscriber;
                        currentUser.InstallDate = tmpUser.InstallDate;
                        currentUser.PaidDate = tmpUser.PaidDate;
                    }
                }
            }
        }

        public void disableLogobot(bool disableAll)
        {
            ADXRibbonTab adxTab = adxRibbonTabLogobotMain.AsRibbonTab;
            foreach (var group in adxTab.Controls)
            {
                ADXRibbonGroup grp = (ADXRibbonGroup)group;
                foreach (var item in grp.Controls)
                {
                    if (item.GetType() == typeof(ADXRibbonButton))
                    {
                        ADXRibbonButton button = item as ADXRibbonButton;

                        if (disableAll)
                        {
                            button.Enabled = false;
                        }
                        else
                        {
                            if (button.Caption != "Activate" && button.Caption != "My Account")
                            {
                                button.Enabled = false;
                            }
                        }
                    }
                    else if (item.GetType() == typeof(ADXRibbonComboBox))
                    {
                        ADXRibbonComboBox comboBox = item as ADXRibbonComboBox;
                        comboBox.Enabled = false;
                    }
                    else if (item.GetType() == typeof(ADXRibbonCheckBox))
                    {
                        ADXRibbonCheckBox checkBox = item as ADXRibbonCheckBox;
                        checkBox.Enabled = false;
                    }
                    else if (item.GetType() == typeof(ADXRibbonBox))
                    {
                        ADXRibbonBox ribbonBox = item as ADXRibbonBox;
                        foreach (var boxItem in ribbonBox.Controls)
                        {
                            if (boxItem.GetType() == typeof(ADXRibbonMenu))
                            {
                                ADXRibbonMenu menuItem = boxItem as ADXRibbonMenu;
                                menuItem.Enabled = false;
                            }
                        }
                    }
                    else if (item.GetType() == typeof(ADXRibbonDropDown))
                    {
                        ADXRibbonDropDown ribbonMenu = item as ADXRibbonDropDown;
                        ribbonMenu.Enabled = false;
                    }

                    else if (item.GetType() == typeof(ADXRibbonMenu))
                    {
                        ADXRibbonMenu ribbonMenu = item as ADXRibbonMenu;
                        foreach (var menuItem in ribbonMenu.Controls)
                        {
                            if (menuItem.GetType() == typeof(ADXRibbonButton))
                            {
                                ADXRibbonButton buttonItem = menuItem as ADXRibbonButton;
                                buttonItem.Enabled = false;
                            }
                        }
                    }
                }
            }
            disableLogobotPane = true;
        }

        public void enableLogobot(string username)
        {
            if (lgbtPane == null)
            {
                lgbtPane = new LogobotPaneInstance(this.adxPowerPointTaskPanesCollectionItemLogobotPane.TaskPaneInstance);
            }

            ADXRibbonTab adxTab = adxRibbonTabLogobotMain.AsRibbonTab;
            foreach (var group in adxTab.Controls)
            {
                ADXRibbonGroup grp = (ADXRibbonGroup)group;
                foreach (var item in grp.Controls)
                {
                    if (item.GetType() == typeof(ADXRibbonButton))
                    {
                        ADXRibbonButton button = item as ADXRibbonButton;
                        button.Enabled = true;
                    }
                    else if (item.GetType() == typeof(ADXRibbonComboBox))
                    {
                        ADXRibbonComboBox comboBox = item as ADXRibbonComboBox;
                        comboBox.Enabled = true;
                    }
                    else if (item.GetType() == typeof(ADXRibbonCheckBox))
                    {
                        ADXRibbonCheckBox checkBox = item as ADXRibbonCheckBox;
                        checkBox.Enabled = true;
                    }
                    else if (item.GetType() == typeof(ADXRibbonBox))
                    {
                        ADXRibbonBox ribbonBox = item as ADXRibbonBox;
                        foreach (var boxItem in ribbonBox.Controls)
                        {
                            if (boxItem.GetType() == typeof(ADXRibbonMenu))
                            {
                                ADXRibbonMenu menuItem = boxItem as ADXRibbonMenu;
                                menuItem.Enabled = true;
                            }
                        }
                    }
                    else if (item.GetType() == typeof(ADXRibbonDropDown))
                    {
                        ADXRibbonDropDown ribbonMenu = item as ADXRibbonDropDown;
                        ribbonMenu.Enabled = true;
                    }
                    else if (item.GetType() == typeof(ADXRibbonMenu))
                    {
                        ADXRibbonMenu ribbonMenu = item as ADXRibbonMenu;
                        foreach (var menuItem in ribbonMenu.Controls)
                        {
                            if (menuItem.GetType() == typeof(ADXRibbonButton))
                            {
                                ADXRibbonButton buttonItem = menuItem as ADXRibbonButton;
                                buttonItem.Enabled = true;
                            }
                        }
                    }
                }
            }
            this.adxPowerPointTaskPanesCollectionItemLogobotPane.TaskPaneInstance.Enabled =  true;
            LogobotPaneMain.setUserBlotters(lgbtPane.PaneTreeViewUser, username);
        }

        private void adxRibbonButtonActivate_OnClick(object sender, IRibbonControl control, bool pressed)
        {
            loginLogobot();
        }

        private void adxRibbonButtonMyAccount_OnClick(object sender, IRibbonControl control, bool pressed)
        { 
            if(username != null && password != null)
            {
                // Navigate to a logobot account page because no username and password are available.
                Process.Start("http://www.logobot.co.uk/login/");
            }
            else
            {
                // Navigate to a logobot home page because no username and password are available.
                Process.Start("http://www.logobot.co.uk/");
            }
        }

        private void adxRibbonButtonInfo_OnClick(object sender, IRibbonControl control, bool pressed)
        {
            MessageBox.Show("Under construction - coming soon!");
        }

        private void adxRibbonButtonContact_OnClick(object sender, IRibbonControl control, bool pressed)
        {
            if (username != null)
            {
                Process.Start(String.Format("mailto:{0}?subject={1}", logobot_email, "User: " + username));
            }
        }

        private void adxRibbonButtonShowHide_OnClick(object sender, AddinExpress.MSO.IRibbonControl control, bool pressed)
        {
            ShowHidePaneButton();
        }

        public void ShowHidePaneButton()
        {
            if (this.adxRibbonButtonShowHide.Tag == 0)
            {
                this.adxPowerPointTaskPanesCollectionItemLogobotPane.TaskPaneInstance.Hide();
                this.adxRibbonButtonShowHide.Tag = 1;
                this.adxRibbonButtonShowHide.Caption = "Show Pane";
            }
            else
            {
                this.adxPowerPointTaskPanesCollectionItemLogobotPane.TaskPaneInstance.Show();
                this.adxRibbonButtonShowHide.Tag = 0;
                this.adxRibbonButtonShowHide.Caption = "Hide Pane";
            }
        }

        private void adxRibbonMenuScreenPopular_OnCreate(object sender, ADXRibbonCreateMenuEventArgs e)
        {
            e.Clear(); // this removes exisiting controls 

            if (screenSources != null)
            {
                foreach (var source in screenSources)
                {
                    ScreenSources tmpSource = source as ScreenSources;

                    ADXRibbonButton newButton = new ADXRibbonButton(this.components);
                    newButton.Caption = tmpSource.SourceName;
                    newButton.Id = tmpSource.SourceName;
                    //newButton.Image = 3;
                    newButton.ImageMso = "Folder";
                    newButton.ImageTransparentColor = Color.Transparent;
                    newButton.Ribbons = ADXRibbons.msrPowerPointPresentation;
                    newButton.OnClick += new ADXRibbonOnAction_EventHandler(buttonSource_OnClick);
                    e.AddControl(newButton);
                }
            }
        }

        private void buttonSource_OnClick(object sender, IRibbonControl control, bool pressed)
        {
            // use a control's ID to identify it 
            Cursor.Current = Cursors.WaitCursor;
            ScreenSources selectedSource;

            foreach (var src in screenSources)
            {
                ScreenSources tmpSrc = src as ScreenSources;
                if(tmpSrc.SourceName == control.Id)
                {
                    selectedSource = new ScreenSources(tmpSrc.SourceName, tmpSrc.SourceCategoryTable, tmpSrc.SourceCategoryLogoTable);
                    updateScreenSourceCategories(selectedSource);
                }
            }
            Cursor.Current = Cursors.Default;
        }

        private void adxRibbonMenuPublicCountry_OnCreate(object sender, ADXRibbonCreateMenuEventArgs e)
        {
            e.Clear(); // this removes exisiting controls 
            int counter = 0;
            if (screenCountries != null)
            {
                foreach (var country in screenCountries)
                {
                    string tmpCountry = country as string;
                    ADXRibbonButton newButtonCountry = new ADXRibbonButton(this.components);
                    newButtonCountry.Caption = tmpCountry;
                    newButtonCountry.Id = "country"+ counter;
                    newButtonCountry.ImageMso = "Folder";
                    newButtonCountry.ImageTransparentColor = Color.Transparent;
                    newButtonCountry.Ribbons = ADXRibbons.msrPowerPointPresentation;
                    newButtonCountry.OnClick += new ADXRibbonOnAction_EventHandler(buttonCountry_OnClick);
                    e.AddControl(newButtonCountry);
                    counter++;
                }
            }
        }

        private void buttonCountry_OnClick(object sender, IRibbonControl control, bool pressed)
        {
            Cursor.Current = Cursors.WaitCursor;
            ADXRibbonButton btnCountry = (ADXRibbonButton)sender;
            string country = btnCountry.Caption;
            updateScreenCategories("Public", country);
            Cursor.Current = Cursors.Default;
        }

        private void adxRibbonMenuPublicIndustry_OnCreate(object sender, ADXRibbonCreateMenuEventArgs e)
        {
            e.Clear(); // this removes exisiting controls 
            int counter = 0;
            if (screenSectors != null)
            {
                foreach (var sector in screenSectors)
                {
                    string tmpSector = sector as string;

                    ADXRibbonButton newButtonSector = new ADXRibbonButton(this.components);
                    newButtonSector.Caption = tmpSector;
                    newButtonSector.Id = "sector" + counter;
                    newButtonSector.ImageMso = "Folder";
                    newButtonSector.ImageTransparentColor = Color.Transparent;
                    newButtonSector.Ribbons = ADXRibbons.msrPowerPointPresentation;
                    newButtonSector.OnClick += new ADXRibbonOnAction_EventHandler(buttonIndustry_OnClick);
                    e.AddControl(newButtonSector);
                    counter++;
                }
            }
        }

        private void buttonIndustry_OnClick(object sender, IRibbonControl control, bool pressed)
        {
            // use a control's ID to identify it 
            Cursor.Current = Cursors.WaitCursor;
            ADXRibbonButton btnIndustry = (ADXRibbonButton)sender;
            string industry = btnIndustry.Caption;
            updateScreenCategories("Industry", industry);
            Cursor.Current = Cursors.Default;
        }

        private void adxRibbonComboBoxCountry_OnChange(object sender, IRibbonControl Control, string text)
        {
            Cursor.Current = Cursors.WaitCursor;
            updateScreenCategories("Public", text);
            Cursor.Current = Cursors.Default;
        }

        private void adxRibbonComboBoxIndustry_OnChange(object sender, IRibbonControl Control, string text)
        {
            Cursor.Current = Cursors.WaitCursor;
            updateScreenCategories("Industry", text);
            Cursor.Current = Cursors.Default;
        }

        private void adxRibbonCheckBoxNoLogos_OnClick(object sender, IRibbonControl control, bool pressed)
        {
            if (pressed)
            {
                LogobotPaneMain.getSetSCREEN_NO_LOGO = pressed;
                //MessageBox.Show("Logobot.xml file: " + pathXmlDoc);
                userSettings.Update("noLogo", true);
                userSettings.Save(pathXmlDoc);
            }
            else
            {
                LogobotPaneMain.getSetSCREEN_NO_LOGO = pressed;
                //MessageBox.Show("Logobot.xml file: " + pathXmlDoc);
                userSettings.Update("noLogo", false);
                userSettings.Save(pathXmlDoc);
            }
        }

        public void updateScreenSourceCategories(ScreenSources selectedSource)
        {
            if (lgbtPane == null)
            {
                lgbtPane = new LogobotPaneInstance(this.adxPowerPointTaskPanesCollectionItemLogobotPane.TaskPaneInstance);
            }

            lgbtPane.PaneTabControl.SelectedIndex = 1;

            if (this.checkForInternetConnection())
            {
                LogobotPaneMain.getSetSCREEN_DATA_SOURCE = selectedSource;
                LogobotPaneMain.getScreenCategories(lgbtPane.PaneTreeViewScreen, selectedSource);
                lgbtPane.PaneTreeViewScreen.Visible = true;
                lgbtPane.PaneTreeViewScreen.ExpandAll();
                lgbtPane.PaneDataGridViewScreen.Visible = false;
                lgbtPane.PaneScreenBack.Visible = false;
                lgbtPane.PaneScreen.Enabled = true;
                lgbtPane.PaneScreenMoveAll.Visible = false;
                lgbtPane.PaneScreenMoveSelected.Visible = false;
            }
            else
            {
                MessageBox.Show("There doesn't seem to be an internet connection.  Check you have internet access and click the Activate button.");
                this.adxPowerPointTaskPanesCollectionItemLogobotPane.TaskPaneInstance.Enabled = false;
                this.disableLogobot(false);
            }
        }

        public void updateScreenCategories(string source, string countryIndustryName)
        {
            if (lgbtPane == null)
            {
                lgbtPane = new LogobotPaneInstance(this.adxPowerPointTaskPanesCollectionItemLogobotPane.TaskPaneInstance);
            }

            lgbtPane.PaneTabControl.SelectedIndex = 1;

            if (this.checkForInternetConnection())
            {
                switch (source)
                {
                    case "Public":
                        {
                            ScreenSources selectedSource = new ScreenSources("Public", "PublicCompaniesCategories", "PublicCompanies");
                            LogobotPaneMain.getSetSCREEN_DATA_SOURCE = selectedSource;
                            LogobotPaneMain.getPublicCompaniesCategories(lgbtPane.PaneTreeViewScreen, countryIndustryName, selectedSource);
                            lgbtPane.PaneTreeViewScreen.Visible = true;
                            lgbtPane.PaneDataGridViewScreen.Visible = false;
                            lgbtPane.PaneScreenBack.Visible = false;
                            lgbtPane.PaneScreen.Enabled = true;
                            lgbtPane.PaneScreenMoveAll.Visible = false;
                            lgbtPane.PaneScreenMoveSelected.Visible = false;
                            break;
                        }
                    case "Industry":
                        {
                            ScreenSources selectedSource = new ScreenSources("Industry", "PublicCompaniesCategories", "PublicCompanies");
                            LogobotPaneMain.getSetSCREEN_DATA_SOURCE = selectedSource;
                            LogobotPaneMain.getPublicCompaniesIndustryCategories(lgbtPane.PaneTreeViewScreen, countryIndustryName, selectedSource);
                            lgbtPane.PaneTreeViewScreen.Visible = true;
                            lgbtPane.PaneDataGridViewScreen.Visible = false;
                            lgbtPane.PaneScreenBack.Visible = false;
                            lgbtPane.PaneScreen.Enabled = true;
                            lgbtPane.PaneScreenMoveAll.Visible = false;
                            lgbtPane.PaneScreenMoveSelected.Visible = false;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            else
            {
                MessageBox.Show("There doesn't seem to be an internet connection.  Check you have internet access and click the Activate button.");
                this.adxPowerPointTaskPanesCollectionItemLogobotPane.TaskPaneInstance.Enabled = false;
                this.disableLogobot(false);
            }
        }

        private void adxRibbonButtonSaveBlotter_OnClick(object sender, IRibbonControl control, bool pressed)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (lgbtPane == null)
            {
                lgbtPane = new LogobotPaneInstance(this.adxPowerPointTaskPanesCollectionItemLogobotPane.TaskPaneInstance);
            }

            if (lgbtPane.PaneDataGridViewBlotter.Rows.Count != 0)
            {
                if (this.checkForInternetConnection())
                {
                    frmSaveBlotter saveBlotter = new frmSaveBlotter(lgbtPane, username);
                    saveBlotter.ShowDialog();
                    LogobotPaneMain.getUserBlotters(lgbtPane.PaneTreeViewUser, username);
                }
                else
                {
                    MessageBox.Show("There doesn't seem to be an internet connection.  Check you have internet access and click the Activate button.");
                    this.adxPowerPointTaskPanesCollectionItemLogobotPane.TaskPaneInstance.Enabled = false;
                    this.disableLogobot(false);
                }
            }
            else
            {
                MessageBox.Show("You cannot save a Logo Blotter that has no logos.  Add at least one logo to your Logo Blotter and try again.");
            }
            Cursor.Current = Cursors.Default;
        }

        private void adxRibbonComboBoxResultPages_OnChange(object sender, IRibbonControl Control, string text)
        {
            MessageBox.Show(text);
        }

        private void adxRibbonDropDownLogoSize_OnAction(object sender, IRibbonControl Control, string selectedId, int selectedIndex)
        {
            updateLogoRowSize(selectedId);
        }

        public void updateLogoRowSize(string size)
        {
            if (lgbtPane == null)
            {
                lgbtPane = new LogobotPaneInstance(this.adxPowerPointTaskPanesCollectionItemLogobotPane.TaskPaneInstance);
            }
            LogobotPaneMain.setDataGridViewRowSize(lgbtPane.PaneDataGridViewUser, size);
            LogobotPaneMain.setDataGridViewRowSize(lgbtPane.PaneDataGridViewScreen, size);
            LogobotPaneMain.setDataGridViewRowSize(lgbtPane.PaneDataGridViewSearch, size);
            LogobotPaneMain.setDataGridViewRowSize(lgbtPane.PaneDataGridViewBlotter, size);
        }

        private void adxRibbonDropDownArrangeSpacing_OnAction(object sender, IRibbonControl Control, string selectedId, int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 0:
                    arrangePadding = 0;
                    break;
                case 1:
                    arrangePadding = 2;
                    break;
                case 2:
                    arrangePadding = 5;
                    break;
                case 3:
                    arrangePadding = 10;
                    break;
                case 4:
                    arrangePadding = 20;
                    break;
                default:
                    break;
            }
        }

        private void adxRibbonDropDownArrangeShape_OnAction(object sender, IRibbonControl Control, string selectedId, int selectedIndex)
        {
            arrangeShape = selectedId;
        }

        public void PrepareLogoRectangleArrangement(object shape, bool fillAllBlotter)
        {
            PowerPoint.Shape eventShape = (PowerPoint.Shape)shape;

            if (lgbtPane == null)
            {
                lgbtPane = new LogobotPaneInstance(this.adxPowerPointTaskPanesCollectionItemLogobotPane.TaskPaneInstance);
            }

            if (eventShape.Name == "LGBT_BLOTTER")
            {
                int logoCount = lgbtPane.PaneDataGridViewBlotter.Rows.Count;

                if (logoCount == 1)
                {
                    MessageBox.Show("You cannot arrange only one logo.  Add more logos to arrange, or just drag and drop the logo onto the slide.");
                    eventShape.Delete();
                }
                else
                {
                    this.adxPowerPointTaskPanesCollectionItemLogobotPane.TaskPaneInstance.Enabled = false;

                    ArrangeLogos arrLogos = new ArrangeLogos();
                    arrLogos.LogobotTaskPane = lgbtPane;
                    arrLogos.EventShape = eventShape;
                    arrLogos.FillAllBlotter = fillAllBlotter;

                    wp = new WorkerProgressMarquee();
                    wp.setLabelText("Logobot is starting to arrange your logos:");
                    wp.Show();

                    System.Threading.SynchronizationContext.SetSynchronizationContext(new WindowsFormsSynchronizationContext());
                    backgroundWorkerArrange.RunWorkerAsync(arrLogos);
                }
            }
        }

        public void PrepareLogoRectangleArrangement(object shape, List<Image> selectedLogosForArrangement)
        {
            PowerPoint.Shape eventShape = (PowerPoint.Shape)shape;

            if (lgbtPane == null)
            {
                lgbtPane = new LogobotPaneInstance(this.adxPowerPointTaskPanesCollectionItemLogobotPane.TaskPaneInstance);
            }

            if (eventShape.Name == "LGBT_BLOTTER")
            {
                int logoCount = selectedLogosForArrangement.Count;

                if (logoCount == 1)
                {
                    MessageBox.Show("You cannot arrange only one logo.  Select more logos to arrange.");
                    eventShape.Delete();
                }
                else
                {
                    this.adxPowerPointTaskPanesCollectionItemLogobotPane.TaskPaneInstance.Enabled = false;

                    ArrangeLogos arrLogos = new ArrangeLogos();
                    arrLogos.SelectImages = selectedLogosForArrangement;
                    arrLogos.UseSelectedImages = true;
                    arrLogos.EventShape = eventShape;

                    wp = new WorkerProgressMarquee();
                    wp.setLabelText("Logobot is starting to arrange your logos:");
                    wp.Show();

                    System.Threading.SynchronizationContext.SetSynchronizationContext(new WindowsFormsSynchronizationContext());
                    backgroundWorkerArrange.RunWorkerAsync(arrLogos);
                }
            }
        }

        private void backgroundWorkerArrange_DoWork(object sender, DoWorkEventArgs e)
        {
            ArrangeLogos arrLogos = (ArrangeLogos)e.Argument;
            BackgroundWorker sendingWorker = (BackgroundWorker)sender;
            PowerPoint.Shape eventShape = arrLogos.EventShape;
            LogobotPaneInstance lgbtPane = arrLogos.LogobotTaskPane;

            String tempFilePath = Path.GetTempPath();
            PowerPoint.Presentation currentPresentation = PowerPointApp.ActivePresentation;
            PowerPoint.Slide currentSlide = (PowerPoint.Slide)PowerPointApp.ActiveWindow.View.Slide;

            SortedDictionary<string, Image> logoHash = new SortedDictionary<string, Image>();
            SortedDictionary<string, PositionLogo> placementHash = new SortedDictionary<string, PositionLogo>();

            int pad = arrangePadding;

            float shapeLeft = eventShape.Left;
            float shapeTop = eventShape.Top;
            float shapeOriginalHeight = eventShape.Height;
            float shapeOriginalWidth = eventShape.Width;
            float shapeHeightPixels = (eventShape.Height * (96 / 72));
            float shapeWidthPixels = (eventShape.Width * (96 / 72));

            float shapePixelArea = (shapeHeightPixels * shapeWidthPixels);
            float areaPerLogo = 0;
            if (arrLogos.UseSelectedImages)
            {
                areaPerLogo = (shapePixelArea / arrLogos.SelectImages.Count);
            }
            else
            {
                if(arrLogos.FillAllBlotter)
                {
                    areaPerLogo = (shapePixelArea / lgbtPane.PaneDataGridViewBlotter.Rows.Count);
                }
                else
                {
                    areaPerLogo = (shapePixelArea / lgbtPane.PaneDataGridViewBlotter.Rows.GetRowCount(DataGridViewElementStates.Selected));
                }
            }
                
            int squareSideLogo = Convert.ToInt32(Math.Sqrt(areaPerLogo));
            int logosPerWidth = Convert.ToInt32(Math.Ceiling(shapeWidthPixels / squareSideLogo));

            sendingWorker.ReportProgress(1);

            if(arrLogos.UseSelectedImages)
            {
                foreach(Image logo in arrLogos.SelectImages)
                {
                    ImageFactory resizeImage = new ImageFactory(preserveExifData: true);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        logo.Save(ms, ImageFormat.Png);
                        resizeImage.Load(ms.ToArray());
                    }

                    ResizeLayer resizeLayer;
                    if (arrangeShape == "Box - Uniform")
                    {
                        resizeLayer = new ResizeLayer(new Size(squareSideLogo, squareSideLogo), ResizeMode.Pad);
                        resizeLayer.Upscale = true;
                        resizeImage.Resize(resizeLayer);
                        resizeImage.BackgroundColor(Color.White);
                    }
                    else
                    {
                        resizeLayer = new ResizeLayer(new Size(squareSideLogo, 0));// Only resize the width and image will scale accordingly
                        resizeLayer.Upscale = true;
                        resizeImage.Resize(resizeLayer);
                    }

                    Guid guid = Guid.NewGuid();
                    String uniqueStr = "LGBT_" + guid.ToString();

                    if (!File.Exists(tempFilePath + uniqueStr))
                    {
                        resizeImage.Image.Save(tempFilePath + uniqueStr, ImageFormat.Png);
                    }

                    logoHash.Add(uniqueStr, resizeImage.Image);
                }
            }
            else
            {
                if (arrLogos.FillAllBlotter)
                {
                    for (int i = 0; i < lgbtPane.PaneDataGridViewBlotter.Rows.Count; i++)
                    {
                        Image tmpImage = (Image)lgbtPane.PaneDataGridViewBlotter.Rows[i].Cells[0].Value;

                        ImageFactory resizeImage = new ImageFactory(preserveExifData: true);

                        using (MemoryStream ms = new MemoryStream())
                        {
                            tmpImage.Save(ms, ImageFormat.Png);
                            resizeImage.Load(ms.ToArray());
                        }

                        ResizeLayer resizeLayer;
                        if (arrangeShape == "Box - Uniform")
                        {
                            resizeLayer = new ResizeLayer(new Size(squareSideLogo, squareSideLogo), ResizeMode.Pad);
                            resizeLayer.Upscale = true;
                            resizeImage.Resize(resizeLayer);
                            resizeImage.BackgroundColor(Color.White);
                        }
                        else
                        {
                            resizeLayer = new ResizeLayer(new Size(squareSideLogo, 0));// Only resize the width and image will scale accordingly
                            resizeLayer.Upscale = true;
                            resizeImage.Resize(resizeLayer);
                        }

                        Guid guid = Guid.NewGuid();
                        String uniqueStr = "LGBT_" + guid.ToString();

                        if (!File.Exists(tempFilePath + uniqueStr))
                        {
                            resizeImage.Image.Save(tempFilePath + uniqueStr, ImageFormat.Png);
                        }

                        logoHash.Add(uniqueStr, resizeImage.Image);
                    }
                }
                else
                {
                    for (int i = 0; i < lgbtPane.PaneDataGridViewBlotter.Rows.Count; i++)
                    {
                        if (lgbtPane.PaneDataGridViewBlotter.Rows[i].Selected)
                        {
                            Image tmpImage = (Image)lgbtPane.PaneDataGridViewBlotter.Rows[i].Cells[0].Value;

                            ImageFactory resizeImage = new ImageFactory(preserveExifData: true);

                            using (MemoryStream ms = new MemoryStream())
                            {
                                tmpImage.Save(ms, ImageFormat.Png);
                                resizeImage.Load(ms.ToArray());
                            }

                            ResizeLayer resizeLayer;
                            if (arrangeShape == "Box - Uniform")
                            {
                                resizeLayer = new ResizeLayer(new Size(squareSideLogo, squareSideLogo), ResizeMode.Pad);
                                resizeLayer.Upscale = true;
                                resizeImage.Resize(resizeLayer);
                                resizeImage.BackgroundColor(Color.White);
                            }
                            else
                            {
                                resizeLayer = new ResizeLayer(new Size(squareSideLogo, 0));// Only resize the width and image will scale accordingly
                                resizeLayer.Upscale = true;
                                resizeImage.Resize(resizeLayer);
                            }

                            Guid guid = Guid.NewGuid();
                            String uniqueStr = "LGBT_" + guid.ToString();

                            if (!File.Exists(tempFilePath + uniqueStr))
                            {
                                resizeImage.Image.Save(tempFilePath + uniqueStr, ImageFormat.Png);
                            }

                            logoHash.Add(uniqueStr, resizeImage.Image);
                        }
                    }
                }
            }

            int logoCounter = 0;
            int logoRow = 0;
            int[] collectPreviousRowHeight = new int[(logosPerWidth)];
            int[] previousRowHeight = new int[(logosPerWidth)];
            float tmpX;
            float tmpY;

            sendingWorker.ReportProgress(2);

            foreach (var key in logoHash.Keys)
            {
                Image tmpImg = (Image)logoHash[key];
                //tmpY = (shapeTop + (logoRow * squareSideLogo) + (logoRow * pad));
                tmpY = (shapeTop + previousRowHeight[logoCounter]); //(/*logoRow * */previousRowHeight[logoCounter]) + (logoRow * pad)); //nextRowHeight
                tmpX = (shapeLeft + (logoCounter * squareSideLogo) + (logoCounter * pad));

                collectPreviousRowHeight[logoCounter] = (tmpImg.Height + collectPreviousRowHeight[logoCounter] + pad);

                if (logoCounter == (logosPerWidth - 1)) // When it gets to the end of a row
                {
                    logoCounter = 0;
                    logoRow++;
                    previousRowHeight = collectPreviousRowHeight;
                }
                else
                {
                    logoCounter++;
                }
                PositionLogo posLogo = new PositionLogo(tmpX, tmpY);
                placementHash.Add(key, posLogo);
            }

            arrLogos.LogoHash = logoHash;
            arrLogos.PlacementHash = placementHash;

            e.Result = arrLogos;
        }

        private void backgroundWorkerArrange_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch (e.ProgressPercentage)
            {
                case 1:
                    {
                        wp.setLabelText("Getting logos from your blotter");
                        break;
                    }
                case 2:
                    {
                        wp.setLabelText("Optimizing arrangement of logos on the slide");
                        break;
                    }
                case 3:
                    {
                        wp.setLabelText("Nearly done placing and grouping logos");
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void backgroundWorkerArrange_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            PowerPoint.Presentation currentPresentation = null;
            PowerPoint.Slide currentSlide = null;
            PowerPoint.Shape imageShape = null;
            PowerPoint.Shape grpShapes = null;
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                ArrangeLogos arrLogos = (ArrangeLogos)e.Result;
                currentPresentation = PowerPointApp.ActivePresentation;
                currentSlide = (PowerPoint.Slide)PowerPointApp.ActiveWindow.View.Slide;
                String tempFilePath = Path.GetTempPath();
                float shapeLeft = arrLogos.EventShape.Left;
                float shapeTop = arrLogos.EventShape.Top;
                float shapeOriginalWidth = arrLogos.EventShape.Width;

                if (e.Error != null)//Check if the worker has been canceled or if an error occurred
                {
                    MessageBox.Show("There was an error and your logos could not be arranged");
                    wp.Close();
                    this.adxPowerPointTaskPanesCollectionItemLogobotPane.TaskPaneInstance.Enabled = true;
                }
                else
                {
                    List<string> shpsArray = new List<string>();

                    foreach (var key in arrLogos.PlacementHash.Keys)
                    {
                        Image tmpImg = (Image)arrLogos.LogoHash[key];
                        PositionLogo posLogo = (PositionLogo)arrLogos.PlacementHash[key];
                        float x = (posLogo.X);
                        float y = (posLogo.Y);

                        imageShape = currentSlide.Shapes.AddPicture(tempFilePath + key, Microsoft.Office.Core.MsoTriState.msoFalse,
                        Microsoft.Office.Core.MsoTriState.msoTrue, x, y, tmpImg.Width, tmpImg.Height);

                        imageShape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        imageShape.Name = key.ToString();
                        shpsArray.Add(key.ToString());
                    }

                    grpShapes = currentSlide.Shapes.Range(shpsArray.ToArray()).Group();
                    grpShapes.LockAspectRatio = Microsoft.Office.Core.MsoTriState.msoTrue;
                    grpShapes.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;

                    Guid guid = Guid.NewGuid();
                    grpShapes.Name = "GROUP_LGBT_BLOTTER"; 
                    grpShapes.Width = shapeOriginalWidth;

                    currentSlide.Shapes.Range("LGBT_BLOTTER").Delete();

                    wp.Close();
                    this.adxPowerPointTaskPanesCollectionItemLogobotPane.TaskPaneInstance.Enabled = true;
                    Cursor.Current = Cursors.Default;
                }
            }
            finally
            {
                if (currentPresentation != null)
                    Marshal.ReleaseComObject(currentPresentation);
                if (currentSlide != null)
                    Marshal.ReleaseComObject(currentSlide);
                if (imageShape != null)
                    Marshal.ReleaseComObject(imageShape);
                if (grpShapes != null)
                    Marshal.ReleaseComObject(grpShapes);
            }
        }

        public void PrepareLogoOvalArrangement(object shape, bool fillAllBlotter)
        {
            PowerPoint.Shape eventShape = (PowerPoint.Shape)shape;

            if (lgbtPane == null)
            {
                lgbtPane = new LogobotPaneInstance(this.adxPowerPointTaskPanesCollectionItemLogobotPane.TaskPaneInstance);
            }
            int logoCount = lgbtPane.PaneDataGridViewBlotter.Rows.Count;

            if (logoCount == 1)
            {
                MessageBox.Show("You cannot arrange only one logo.  Add more logos to arrange, or just drag and drop the logo onto the slide.");
                eventShape.Delete();
            }
            else
            {
                if (eventShape.Name == "LGBT_BLOTTER")
                {
                    this.adxPowerPointTaskPanesCollectionItemLogobotPane.TaskPaneInstance.Enabled = false;

                    ArrangeLogos arrLogos = new ArrangeLogos();
                    arrLogos.LogobotTaskPane = lgbtPane;
                    arrLogos.EventShape = eventShape;
                    arrLogos.FillAllBlotter = fillAllBlotter;

                    wp = new WorkerProgressMarquee();
                    wp.setLabelText("Logobot is starting to arrange your logos:");
                    wp.Show();

                    System.Threading.SynchronizationContext.SetSynchronizationContext(new WindowsFormsSynchronizationContext());
                    backgroundWorkerArrangeCircle.RunWorkerAsync(arrLogos); ;
                }
            }
        }

        public void PrepareLogoOvalArrangement(object shape, List<Image> selectedLogosForArrangement)
        {
            PowerPoint.Shape eventShape = (PowerPoint.Shape)shape;

            if (lgbtPane == null)
            {
                lgbtPane = new LogobotPaneInstance(this.adxPowerPointTaskPanesCollectionItemLogobotPane.TaskPaneInstance);
            }
            int logoCount = lgbtPane.PaneDataGridViewBlotter.Rows.Count;

            if (logoCount == 1)
            {
                MessageBox.Show("You cannot arrange only one logo.  Add more logos to arrange, or just drag and drop the logo onto the slide.");
                eventShape.Delete();
            }
            else
            {
                if (eventShape.Name == "LGBT_BLOTTER")
                {
                    this.adxPowerPointTaskPanesCollectionItemLogobotPane.TaskPaneInstance.Enabled = false;

                    ArrangeLogos arrLogos = new ArrangeLogos();
                    arrLogos.SelectImages = selectedLogosForArrangement;
                    arrLogos.UseSelectedImages = true;
                    arrLogos.EventShape = eventShape;

                    wp = new WorkerProgressMarquee();
                    wp.setLabelText("Logobot is starting to arrange your logos:");
                    wp.Show();

                    System.Threading.SynchronizationContext.SetSynchronizationContext(new WindowsFormsSynchronizationContext());
                    backgroundWorkerArrangeCircle.RunWorkerAsync(arrLogos); ;
                }
            }
        }

        private void backgroundWorkerArrangeCircle_DoWork(object sender, DoWorkEventArgs e)
        {
            ArrangeLogos arrLogos = (ArrangeLogos)e.Argument;
            BackgroundWorker sendingWorker = (BackgroundWorker)sender;
            PowerPoint.Shape eventShape = arrLogos.EventShape;
            LogobotPaneInstance lgbtPane = arrLogos.LogobotTaskPane;

            String tempFilePath = Path.GetTempPath();
            PowerPoint.Presentation currentPresentation = PowerPointApp.ActivePresentation;
            PowerPoint.Slide currentSlide = (PowerPoint.Slide)PowerPointApp.ActiveWindow.View.Slide;

            SortedDictionary<string, Image> logoHash = new SortedDictionary<string, Image>();
            SortedDictionary<string, PositionLogo> placementHash = new SortedDictionary<string, PositionLogo>();

            int pad = arrangePadding;

            double mx = eventShape.Left; // X location on the slide
            double my = eventShape.Top; // Y loaction on the slide
            float shapeOriginalRadius = eventShape.Width / 2;
            double shapeRadiusPixels = (eventShape.Width / 2 * (96 / 72));
            double shapeDiameterPixels = (eventShape.Width * (96 / 72));

            int logoCount = 0;
            if (arrLogos.UseSelectedImages)
            {
                logoCount = arrLogos.SelectImages.Count;
            }
            else
            {
                logoCount = lgbtPane.PaneDataGridViewBlotter.Rows.Count;
            }

            double grossSquareSize = (Math.Pow(shapeDiameterPixels, 2.0) * Math.PI) / (4 * logoCount);
            double reductionFactor = (Math.Pow(((shapeDiameterPixels * Math.PI) / logoCount), 2.0)) * .5;
            double logoArea = grossSquareSize - reductionFactor;
            double squareSideLogo = Math.Sqrt(logoArea);

            List<PositionLogo> circleLogoLocations = new List<PositionLogo>();

            sendingWorker.ReportProgress(1);

            findOptimalRowColumnSize(circleLogoLocations, shapeRadiusPixels, squareSideLogo, logoCount, my, mx);
            int resizeSquareSideLogo = (int)circleLogoLocations[0].LogoSize;

            sendingWorker.ReportProgress(2);

            if (arrLogos.UseSelectedImages)
            {
                // Resize the logos to right square size
                foreach (Image logo in arrLogos.SelectImages)
                {
                    ImageFactory resizeImage = new ImageFactory(preserveExifData: true);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        logo.Save(ms, ImageFormat.Png);
                        resizeImage.Load(ms.ToArray());
                    }

                    ResizeLayer resizeLayer = new ResizeLayer(new Size(resizeSquareSideLogo, resizeSquareSideLogo), ResizeMode.Pad);
                    resizeLayer.Upscale = true;

                    resizeImage.Resize(resizeLayer);
                    resizeImage.BackgroundColor(Color.White);

                    Guid guid = Guid.NewGuid();
                    String uniqueStr = "LGBT_" + guid.ToString();

                    if (!File.Exists(tempFilePath + uniqueStr))
                    {
                        resizeImage.Image.Save(tempFilePath + uniqueStr, ImageFormat.Png);
                    }

                    logoHash.Add(uniqueStr, resizeImage.Image);
                }
            }
            else
            {
                if (arrLogos.FillAllBlotter)
                {
                    // Resize the logos to right square size
                    for (int i = 0; i < lgbtPane.PaneDataGridViewBlotter.Rows.Count; i++)
                    {
                        Image tmpImage = (Image)lgbtPane.PaneDataGridViewBlotter.Rows[i].Cells[0].Value;

                        ImageFactory resizeImage = new ImageFactory(preserveExifData: true);

                        using (MemoryStream ms = new MemoryStream())
                        {
                            tmpImage.Save(ms, ImageFormat.Png);
                            resizeImage.Load(ms.ToArray());
                        }

                        ResizeLayer resizeLayer = new ResizeLayer(new Size(resizeSquareSideLogo, resizeSquareSideLogo), ResizeMode.Pad);
                        resizeLayer.Upscale = true;

                        resizeImage.Resize(resizeLayer);
                        resizeImage.BackgroundColor(Color.White);

                        Guid guid = Guid.NewGuid();
                        String uniqueStr = "LGBT_" + guid.ToString();

                        if (!File.Exists(tempFilePath + uniqueStr))
                        {
                            resizeImage.Image.Save(tempFilePath + uniqueStr, ImageFormat.Png);
                        }

                        logoHash.Add(uniqueStr, resizeImage.Image);
                    }
                }
                else
                {
                    for (int i = 0; i < lgbtPane.PaneDataGridViewBlotter.Rows.Count; i++)
                    {
                        if (lgbtPane.PaneDataGridViewBlotter.Rows[i].Selected)
                        {
                            Image tmpImage = (Image)lgbtPane.PaneDataGridViewBlotter.Rows[i].Cells[0].Value;

                            ImageFactory resizeImage = new ImageFactory(preserveExifData: true);

                            using (MemoryStream ms = new MemoryStream())
                            {
                                tmpImage.Save(ms, ImageFormat.Png);
                                resizeImage.Load(ms.ToArray());
                            }

                            ResizeLayer resizeLayer = new ResizeLayer(new Size(resizeSquareSideLogo, resizeSquareSideLogo), ResizeMode.Pad);
                            resizeLayer.Upscale = true;

                            resizeImage.Resize(resizeLayer);
                            resizeImage.BackgroundColor(Color.White);

                            Guid guid = Guid.NewGuid();
                            String uniqueStr = "LGBT_" + guid.ToString();

                            if (!File.Exists(tempFilePath + uniqueStr))
                            {
                                resizeImage.Image.Save(tempFilePath + uniqueStr, ImageFormat.Png);
                            }

                            logoHash.Add(uniqueStr, resizeImage.Image);
                        }
                    }
                }
            }

            // Load the placementHash with X & Y positions
            int counter = 0;
            foreach (var key in logoHash.Keys)
            {
                placementHash.Add(key, circleLogoLocations[counter]);
                counter++;
            }

            arrLogos.LogoHash = logoHash;
            arrLogos.PlacementHash = placementHash;

            e.Result = arrLogos;
        }

        private void findOptimalRowColumnSize(List<PositionLogo> circleLogoLocations, double shapeRadiusPixels, double squareSideLogo, int logoCount, double my, double mx)
        {
            bool match = false;
            bool plusAdj = false;
            bool minusAdj = false;
            double adjFactor = 1.00;
            const double SHAPERADIUSPIXELS = 150;
            const double SQUARESIDELOGO = 50;
            const double MY = 100;
            const double MX = 200;

            shapeRadiusPixels = SHAPERADIUSPIXELS;
            squareSideLogo = SQUARESIDELOGO;
            my = MY;
            mx = MX;

            do
            {
                double rows = (2 * shapeRadiusPixels) / squareSideLogo;
                double yMin = my - squareSideLogo * .5 * ((2 * rows) / 2);
                double xMin;
                double x;
                int cols;
                //double cols;
                double colsD;
                double y;
                double logosPlaced = 0;

                for (int i = 1; i < rows; i++)
                {
                    y = yMin + (i - 1) * squareSideLogo;
                    if (i <= rows / 2)
                    {
                        int xyDiff = (int)((y - my) * (y - my));
                        colsD = (2 * Math.Pow((shapeRadiusPixels * shapeRadiusPixels - xyDiff), 0.5)) / squareSideLogo;
                        cols = Convert.ToInt32(Math.Floor(colsD));
                    }
                    else
                    {
                        colsD = (2 * Math.Pow((shapeRadiusPixels * shapeRadiusPixels - (y - my + squareSideLogo) * (y - my + squareSideLogo)), 0.5)) / squareSideLogo;
                        cols = Convert.ToInt32(Math.Floor(colsD));
                    }

                    xMin = mx - squareSideLogo * 0.5 * ((2 * cols) / 2);

                    for (int j = 1; j <= cols; j++)
                    {
                        x = xMin + (j - 1) * squareSideLogo;
                        PositionLogo posLogo = new PositionLogo(Convert.ToInt32(x), Convert.ToInt32(y), squareSideLogo);
                        circleLogoLocations.Add(posLogo);
                    }

                    logosPlaced = logosPlaced + cols;
                }

                adjFactor = (logosPlaced / logoCount);

                //Debug.WriteLine("Logos placed = " + logosPlaced + " Logo count = " + logoCount + " Adjustment Factor = " + adjFactor);
                if (logosPlaced > logoCount && !(adjFactor <= 1.05))
                {
                    if(minusAdj && !plusAdj)
                    {
                        squareSideLogo = squareSideLogo + 0.05;
                        //shapeRadiusPixels = shapeRadiusPixels + .05;
                        circleLogoLocations.Clear();
                        plusAdj = true;
                    }

                    else if(minusAdj && plusAdj)
                    {
                        squareSideLogo = squareSideLogo + 0.01;
                        //shapeRadiusPixels = shapeRadiusPixels + .01;
                        circleLogoLocations.Clear();
                        plusAdj = true;
                    }
                    else
                    {
                        squareSideLogo = squareSideLogo + 1.00;
                        //shapeRadiusPixels = shapeRadiusPixels + 1.00;
                        circleLogoLocations.Clear();
                        plusAdj = true;
                    }
                }
                else if (logosPlaced < logoCount && (adjFactor > 1.05 || adjFactor < 1.00))
                {
                    if (plusAdj && !minusAdj)
                    {
                        squareSideLogo = squareSideLogo - 0.05;
                        //shapeRadiusPixels = shapeRadiusPixels - .05;
                        circleLogoLocations.Clear();
                        minusAdj = true;
                    }
                    else if (minusAdj && plusAdj)
                    {
                        squareSideLogo = squareSideLogo - 0.01;
                        //shapeRadiusPixels = shapeRadiusPixels - .01;
                        circleLogoLocations.Clear();
                        minusAdj = true;
                    }
                    else
                    {
                        squareSideLogo = squareSideLogo - 1.00;
                        //shapeRadiusPixels = shapeRadiusPixels - 1.00;
                        circleLogoLocations.Clear();
                        minusAdj = true;
                    }
                }
                else if (logosPlaced >= logoCount && adjFactor <= 1.05)
                {
                    match = true;
                }

            } while (!match);
        }

        private void backgroundWorkerArrangeCircle_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch (e.ProgressPercentage)
            {
                case 1:
                    {
                        wp.setLabelText("Getting logos from your blotter");
                        break;
                    }
                case 2:
                    {
                        wp.setLabelText("Optimizing arrangement of logos on the slide");
                        break;
                    }
                case 3:
                    {
                        wp.setLabelText("Nearly done placing and grouping logos");
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void backgroundWorkerArrangeCircle_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            PowerPoint.Presentation currentPresentation = null;
            PowerPoint.Slide currentSlide = null;
            PowerPoint.Shape imageShape = null;
            PowerPoint.Shape grpShapes = null;
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                ArrangeLogos arrLogos = (ArrangeLogos)e.Result;
                currentPresentation = PowerPointApp.ActivePresentation;
                currentSlide = (PowerPoint.Slide)PowerPointApp.ActiveWindow.View.Slide;
                String tempFilePath = Path.GetTempPath();
                float shapeLeft = arrLogos.EventShape.Left;
                float shapeTop = arrLogos.EventShape.Top;
                float shapeOriginalWidth = arrLogos.EventShape.Width;

                if (e.Error != null)//Check if the worker has been canceled or if an error occurred
                {
                    MessageBox.Show("There was an error and your logos could not be arranged");
                    wp.Close();
                    this.adxPowerPointTaskPanesCollectionItemLogobotPane.TaskPaneInstance.Enabled = true;
                }
                else
                {
                    // Put all of the logos on the slide, resize and reposition
                    List<string> shpsArray = new List<string>();

                    foreach (var key in arrLogos.PlacementHash.Keys)
                    {
                        Image tmpImg = (Image)arrLogos.LogoHash[key];
                        PositionLogo posLogo = (PositionLogo)arrLogos.PlacementHash[key];
                        float tmpX = (posLogo.X);
                        float tmpY = (posLogo.Y);

                        imageShape = currentSlide.Shapes.AddPicture(tempFilePath + key, Microsoft.Office.Core.MsoTriState.msoFalse,
                        Microsoft.Office.Core.MsoTriState.msoTrue, tmpX, tmpY, tmpImg.Width, tmpImg.Height);
                        imageShape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                        imageShape.Name = key.ToString();
                        shpsArray.Add(key.ToString());
                    }

                    grpShapes = currentSlide.Shapes.Range(shpsArray.ToArray()).Group();
                    grpShapes.Name = "GROUP_LGBT_BLOTTER";
                    
                    grpShapes.LockAspectRatio = Microsoft.Office.Core.MsoTriState.msoTrue;
                    grpShapes.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;

                    grpShapes.Top = shapeTop;
                    grpShapes.Left = shapeLeft;
                    grpShapes.Width = arrLogos.EventShape.Width;
                    
                    currentSlide.Shapes.Range("LGBT_BLOTTER").Delete();

                    wp.Close();
                    this.adxPowerPointTaskPanesCollectionItemLogobotPane.TaskPaneInstance.Enabled = true;
                    Cursor.Current = Cursors.Default;
                }
            }
            finally
            {
                if (currentPresentation != null)
                    Marshal.ReleaseComObject(currentPresentation);
                if (currentSlide != null)
                    Marshal.ReleaseComObject(currentSlide);
                if (imageShape != null)
                    Marshal.ReleaseComObject(imageShape);
                if (grpShapes != null)
                    Marshal.ReleaseComObject(grpShapes);
            }
        }

        private void adxRibbonButtonScrapeSelected_OnClick(object sender, IRibbonControl control, bool pressed)
        {
            PowerPoint.Selection currentSelection = null;
            PowerPoint.Shape tmpShape = null;
            PowerPoint.Shape tmpGrpShape = null;

            try
            {
                currentSelection = PowerPointApp.ActiveWindow.Selection;

                LogoGetAndLoad logoGAL = new LogoGetAndLoad();
                Image tmpLogo;

                if (lgbtPane == null)
                {
                    lgbtPane = new LogobotPaneInstance(this.adxPowerPointTaskPanesCollectionItemLogobotPane.TaskPaneInstance);
                }

                Clipboard.Clear();

                Cursor.Current = Cursors.WaitCursor;

                foreach (var shape in currentSelection.ShapeRange)
                {
                    tmpShape = (PowerPoint.Shape)shape;

                    if (tmpShape.Type == Microsoft.Office.Core.MsoShapeType.msoGroup)
                    {
                        foreach (var grpShape in tmpShape.GroupItems)
                        {
                            tmpGrpShape = grpShape as PowerPoint.Shape;
                            if (tmpGrpShape.Type == Microsoft.Office.Core.MsoShapeType.msoAutoShape || tmpGrpShape.Type == Microsoft.Office.Core.MsoShapeType.msoPicture)
                            {
                                Guid guid = Guid.NewGuid();
                                string uniqueStr = "LGBT_" + guid.ToString();
                                tmpGrpShape.Copy();

                                tmpLogo = Clipboard.GetImage();

                                if (tmpLogo != null)
                                {
                                    tmpLogo.Tag = uniqueStr;
                                    string tmpDetails = "No Name \nWidth: " + tmpLogo.Width + "\nHeight: " + tmpLogo.Height;

                                    LogoGetAndLoad tmpLogoGAL = new LogoGetAndLoad(tmpLogo, tmpDetails);
                                    logoGAL.SearchListGAL.Add(tmpLogoGAL);
                                }
                            }
                            Clipboard.Clear();
                        }
                    }
                    else
                    {
                        if (tmpShape.Type == Microsoft.Office.Core.MsoShapeType.msoAutoShape || tmpShape.Type == Microsoft.Office.Core.MsoShapeType.msoPicture)
                        {
                            Guid guid = Guid.NewGuid();
                            string uniqueStr = "LGBT_" + guid.ToString();
                            tmpShape.Copy();

                            tmpLogo = Clipboard.GetImage();

                            if (tmpLogo != null)
                            {
                                tmpLogo.Tag = uniqueStr;
                                string tmpDetails = "No Name \nWidth: " + tmpLogo.Width + "\nHeight: " + tmpLogo.Height;

                                LogoGetAndLoad tmpLogoGAL = new LogoGetAndLoad(tmpLogo, tmpDetails);
                                logoGAL.SearchListGAL.Add(tmpLogoGAL);
                            }
                        }
                        Clipboard.Clear();
                    }
                }
                Cursor.Current = Cursors.Default;

                if(logoGAL.SearchListGAL.Count != 0)
                {
                    ScrapeLogosForm scrapeForm = new ScrapeLogosForm(logoGAL, lgbtPane);
                    scrapeForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("There were no images found.");
                }
            }
            catch (COMException ComEx)
            {
                MessageBox.Show("Are you sure you made a selection?  Try again.");
                Debug.WriteLine("Exception: " + ComEx.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                if (currentSelection != null)
                    Marshal.ReleaseComObject(currentSelection);
                if (tmpShape != null)
                    Marshal.ReleaseComObject(tmpShape);
            }
        }

        private void adxRibbonButtonScrapeSlide_OnClick(object sender, IRibbonControl control, bool pressed)
        {
            PowerPoint.Slide currentSlide = null;
            PowerPoint.Shape tmpShape = null;
            PowerPoint.Shape tmpGrpShape = null;

            try
            {
                currentSlide = (PowerPoint.Slide)PowerPointApp.ActiveWindow.View.Slide;
                LogoGetAndLoad logoGAL = new LogoGetAndLoad();
                Image tmpLogo;

                if (lgbtPane == null)
                {
                    lgbtPane = new LogobotPaneInstance(this.adxPowerPointTaskPanesCollectionItemLogobotPane.TaskPaneInstance);
                }

                if (currentSlide.Shapes.Count != 0)
                {
                    Clipboard.Clear();

                    Cursor.Current = Cursors.WaitCursor;

                    foreach (var shape in currentSlide.Shapes)
                    {
                        tmpShape = (PowerPoint.Shape)shape;

                        if (tmpShape.Type == Microsoft.Office.Core.MsoShapeType.msoGroup)
                        {
                            foreach (var grpShape in tmpShape.GroupItems)
                            {
                                tmpGrpShape = grpShape as PowerPoint.Shape;
                                if (tmpGrpShape.Type == Microsoft.Office.Core.MsoShapeType.msoAutoShape || tmpGrpShape.Type == Microsoft.Office.Core.MsoShapeType.msoPicture)
                                {
                                    Guid guid = Guid.NewGuid();
                                    string uniqueStr = "LGBT_" + guid.ToString();
                                    tmpGrpShape.Copy();

                                    tmpLogo = Clipboard.GetImage();

                                    if (tmpLogo != null)
                                    {
                                        tmpLogo.Tag = uniqueStr;
                                        string tmpDetails = "No Name \nWidth: " + tmpLogo.Width + "\nHeight: " + tmpLogo.Height;

                                        LogoGetAndLoad tmpLogoGAL = new LogoGetAndLoad(tmpLogo, tmpDetails);
                                        logoGAL.SearchListGAL.Add(tmpLogoGAL);
                                    }
                                }
                                Clipboard.Clear();
                            }
                        }
                        else
                        {
                            if (tmpShape.Type == Microsoft.Office.Core.MsoShapeType.msoAutoShape || tmpShape.Type == Microsoft.Office.Core.MsoShapeType.msoPicture)
                            {
                                Guid guid = Guid.NewGuid();
                                string uniqueStr = "LGBT_" + guid.ToString();
                                tmpShape.Copy();

                                tmpLogo = Clipboard.GetImage();

                                if (tmpLogo != null)
                                {
                                    tmpLogo.Tag = uniqueStr;
                                    string tmpDetails = "No Name \nWidth: " + tmpLogo.Width + "\nHeight: " + tmpLogo.Height;

                                    LogoGetAndLoad tmpLogoGAL = new LogoGetAndLoad(tmpLogo, tmpDetails);
                                    logoGAL.SearchListGAL.Add(tmpLogoGAL);
                                }
                            }
                            Clipboard.Clear();
                        }
                    }
                    Cursor.Current = Cursors.Default;

                    if (logoGAL.SearchListGAL.Count != 0)
                    {
                        ScrapeLogosForm scrapeForm = new ScrapeLogosForm(logoGAL, lgbtPane);
                        scrapeForm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("There were no images found.");
                    }
                }
                else
                {
                    MessageBox.Show("There are no recognizable logos on this slide.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                if (currentSlide != null)
                    Marshal.ReleaseComObject(currentSlide);
                if (tmpShape != null)
                    Marshal.ReleaseComObject(tmpShape);
            }
        }

        private void adxRibbonButtonScrapeAllSlides_OnClick(object sender, IRibbonControl control, bool pressed)
        {
            PowerPoint.Presentation currentPresentation = null;
            PowerPoint.Slide currentSlide = null;
            PowerPoint.Shape tmpShape = null;
            PowerPoint.Shape tmpGrpShape = null;

            try
            {
                LogoGetAndLoad logoGAL = new LogoGetAndLoad();
                Image tmpLogo;

                if (lgbtPane == null)
                {
                    lgbtPane = new LogobotPaneInstance(this.adxPowerPointTaskPanesCollectionItemLogobotPane.TaskPaneInstance);
                }

                Clipboard.Clear();

                Cursor.Current = Cursors.WaitCursor;

                currentPresentation = (PowerPoint.Presentation)PowerPointApp.ActivePresentation;

                foreach (var slide in currentPresentation.Slides)
                {
                    currentSlide = (PowerPoint.Slide)slide;

                    foreach (var shape in currentSlide.Shapes)
                    {
                        tmpShape = (PowerPoint.Shape)shape;
                        if (tmpShape.Type == Microsoft.Office.Core.MsoShapeType.msoGroup)
                        {
                            foreach (var grpShape in tmpShape.GroupItems)
                            {
                                tmpGrpShape = grpShape as PowerPoint.Shape;
                                if (tmpGrpShape.Type == Microsoft.Office.Core.MsoShapeType.msoAutoShape || tmpGrpShape.Type == Microsoft.Office.Core.MsoShapeType.msoPicture)
                                {
                                    Guid guid = Guid.NewGuid();
                                    string uniqueStr = "LGBT_" + guid.ToString();
                                    tmpGrpShape.Copy();

                                    tmpLogo = Clipboard.GetImage();

                                    if (tmpLogo != null)
                                    {
                                        tmpLogo.Tag = uniqueStr;
                                        string tmpDetails = "No Name \nWidth: " + tmpLogo.Width + "\nHeight: " + tmpLogo.Height;

                                        LogoGetAndLoad tmpLogoGAL = new LogoGetAndLoad(tmpLogo, tmpDetails);
                                        logoGAL.SearchListGAL.Add(tmpLogoGAL);
                                    }
                                }
                                Clipboard.Clear();
                            }
                        }
                        else
                        {
                            if (tmpShape.Type == Microsoft.Office.Core.MsoShapeType.msoAutoShape || tmpShape.Type == Microsoft.Office.Core.MsoShapeType.msoPicture)
                            {
                                Guid guid = Guid.NewGuid();
                                string uniqueStr = "LGBT_" + guid.ToString();
                                tmpShape.Copy();

                                tmpLogo = Clipboard.GetImage();

                                if (tmpLogo != null)
                                {
                                    tmpLogo.Tag = uniqueStr;
                                    string tmpDetails = "No Name \nWidth: " + tmpLogo.Width + "\nHeight: " + tmpLogo.Height;

                                    LogoGetAndLoad tmpLogoGAL = new LogoGetAndLoad(tmpLogo, tmpDetails);
                                    logoGAL.SearchListGAL.Add(tmpLogoGAL);
                                }
                            }
                            Clipboard.Clear();
                        }
                    }
                }

                if (logoGAL.SearchListGAL.Count != 0)
                {
                    ScrapeLogosForm scrapeForm = new ScrapeLogosForm(logoGAL, lgbtPane);
                    scrapeForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("There were no images found.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                if (currentPresentation != null)
                    Marshal.ReleaseComObject(currentPresentation);
                if (currentSlide != null)
                    Marshal.ReleaseComObject(currentSlide);
                if (tmpShape != null)
                    Marshal.ReleaseComObject(tmpShape);
            }
            Cursor.Current = Cursors.Default;
        }

        private void adxRibbonButtonRemoveWhite_OnClick(object sender, IRibbonControl control, bool pressed)
        {
            PowerPoint.Selection currentSelection = null;
            PowerPoint.Shape tmpShape = null;
            PowerPoint.Shape tmpGrpShape = null;
            PowerPoint.Slide currentSlide = null;
            PowerPoint.Shape imageShape = null;
            String tempFilePath = Path.GetTempPath();

            try
            {
                currentSelection = PowerPointApp.ActiveWindow.Selection;
                currentSlide = (PowerPoint.Slide)PowerPointApp.ActiveWindow.View.Slide;

                Image tmpLogo;

                Clipboard.Clear();

                Cursor.Current = Cursors.WaitCursor;

                foreach (var shape in currentSelection.ShapeRange)
                {
                    tmpShape = (PowerPoint.Shape)shape;

                    if (tmpShape.Type == Microsoft.Office.Core.MsoShapeType.msoGroup)
                    {
                        foreach (var grpShape in tmpShape.GroupItems)
                        {
                            tmpGrpShape = grpShape as PowerPoint.Shape;
                            float shapeLeft = tmpGrpShape.Left;
                            float shapeTop = tmpGrpShape.Top;

                            if (tmpGrpShape.Type == Microsoft.Office.Core.MsoShapeType.msoAutoShape || tmpGrpShape.Type == Microsoft.Office.Core.MsoShapeType.msoPicture)
                            {
                                tmpGrpShape.Copy();
                                tmpLogo = Clipboard.GetImage();

                                if (tmpLogo != null)
                                {
                                    ImageFactory removeWhitespaceImage = new ImageFactory(preserveExifData: true);

                                    using (MemoryStream ms = new MemoryStream())
                                    {
                                        tmpLogo.Save(ms, ImageFormat.Png);
                                        removeWhitespaceImage.Load(ms.ToArray());
                                    }
                                    removeWhitespaceImage.EntropyCrop(128);

                                    Guid guid = Guid.NewGuid();
                                    String uniqueStr = "LGBT_" + guid.ToString();

                                    if (!File.Exists(tempFilePath + uniqueStr))
                                    {
                                        removeWhitespaceImage.Image.Save(tempFilePath + uniqueStr, ImageFormat.Png);
                                    }

                                    imageShape = currentSlide.Shapes.AddPicture(tempFilePath + uniqueStr, Microsoft.Office.Core.MsoTriState.msoFalse,
                                    Microsoft.Office.Core.MsoTriState.msoTrue, shapeLeft, shapeTop, removeWhitespaceImage.Image.Width, removeWhitespaceImage.Image.Height);

                                    tmpGrpShape.Delete();
                                }
                            }
                            Clipboard.Clear();
                        }
                    }
                    else
                    {
                        if (tmpShape.Type == Microsoft.Office.Core.MsoShapeType.msoAutoShape || tmpShape.Type == Microsoft.Office.Core.MsoShapeType.msoPicture)
                        {
                            float shapeLeft = tmpShape.Left;
                            float shapeTop = tmpShape.Top;

                            tmpShape.Copy();
                            tmpLogo = Clipboard.GetImage();

                            if (tmpLogo != null)
                            {
                                ImageFactory removeWhitespaceImage = new ImageFactory(preserveExifData: true);

                                using (MemoryStream ms = new MemoryStream())
                                {
                                    tmpLogo.Save(ms, ImageFormat.Png);
                                    removeWhitespaceImage.Load(ms.ToArray());
                                }
                                removeWhitespaceImage.EntropyCrop(128);

                                Guid guid = Guid.NewGuid();
                                String uniqueStr = "LGBT_" + guid.ToString();

                                if (!File.Exists(tempFilePath + uniqueStr))
                                {
                                    removeWhitespaceImage.Image.Save(tempFilePath + uniqueStr, ImageFormat.Png);
                                }

                                imageShape = currentSlide.Shapes.AddPicture(tempFilePath + uniqueStr, Microsoft.Office.Core.MsoTriState.msoFalse,
                                Microsoft.Office.Core.MsoTriState.msoTrue, shapeLeft, shapeTop, removeWhitespaceImage.Image.Width, removeWhitespaceImage.Image.Height);

                                tmpShape.Delete();
                            }
                            Clipboard.Clear();
                        }
                    }
                    Cursor.Current = Cursors.Default;
                }
            }
            catch (COMException ComEx)
            {
                MessageBox.Show("Are you sure you made a selection?  Try again.");
                Debug.WriteLine("Exception: " + ComEx.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                if (currentSelection != null)
                    Marshal.ReleaseComObject(currentSelection);
                if (currentSlide != null)
                    Marshal.ReleaseComObject(currentSlide);
                if (tmpShape != null)
                    Marshal.ReleaseComObject(tmpShape);
            }
        }

        private void adxRibbonButtonResizeAll_OnClick(object sender, IRibbonControl control, bool pressed)
        {
            PowerPoint.Selection currentSelection = null;
            PowerPoint.Shape tmpShape = null;
            PowerPoint.Slide currentSlide = null;
            PowerPoint.Shape imageShape = null;
            float allHeights = 0;
            int averageHeight = 0;
            String tempFilePath = Path.GetTempPath();

            Image tmpLogo;

            try
            {
                currentSelection = PowerPointApp.ActiveWindow.Selection;
                currentSlide = (PowerPoint.Slide)PowerPointApp.ActiveWindow.View.Slide;

                Clipboard.Clear();

                Cursor.Current = Cursors.WaitCursor;

                foreach (var shape in currentSelection.ShapeRange)
                {
                    tmpShape = (PowerPoint.Shape)shape;

                    if (tmpShape.Type == Microsoft.Office.Core.MsoShapeType.msoAutoShape || tmpShape.Type == Microsoft.Office.Core.MsoShapeType.msoPicture)
                    {
                        allHeights += (tmpShape.Height * (96 / 72));
                    }
                }

                averageHeight = (int)(allHeights / currentSelection.ShapeRange.Count);

                foreach (var shape in currentSelection.ShapeRange)
                {
                    tmpShape = (PowerPoint.Shape)shape;
                    float shapeLeft = tmpShape.Left;
                    float shapeTop = tmpShape.Top;

                    tmpShape.Copy();
                    tmpLogo = Clipboard.GetImage();

                    if (tmpLogo != null)
                    {
                        ImageFactory resizeImage = new ImageFactory(preserveExifData: true);

                        using (MemoryStream ms = new MemoryStream())
                        {
                            tmpLogo.Save(ms, ImageFormat.Png);
                            resizeImage.Load(ms.ToArray());
                        }
                        resizeImage.Resize(new Size(0, averageHeight));

                        Guid guid = Guid.NewGuid();
                        String uniqueStr = "LGBT_" + guid.ToString();

                        if (!File.Exists(tempFilePath + uniqueStr))
                        {
                            resizeImage.Image.Save(tempFilePath + uniqueStr, ImageFormat.Png);
                        }

                        imageShape = currentSlide.Shapes.AddPicture(tempFilePath + uniqueStr, Microsoft.Office.Core.MsoTriState.msoFalse,
                                Microsoft.Office.Core.MsoTriState.msoTrue, shapeLeft, shapeTop, resizeImage.Image.Width, resizeImage.Image.Height);

                        tmpShape.Delete();
                    }
                    Clipboard.Clear();
                }

                Cursor.Current = Cursors.Default;
            }
            catch (COMException ComEx)
            {
                MessageBox.Show("Are you sure you made a selection?  Try again.");
                Debug.WriteLine("Exception: " + ComEx.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                if (currentSelection != null)
                    Marshal.ReleaseComObject(currentSelection);
                if (currentSlide != null)
                    Marshal.ReleaseComObject(currentSlide);
                if (tmpShape != null)
                    Marshal.ReleaseComObject(tmpShape);
            }
        }

        private void adxRibbonButtonArrangeSelected_OnClick(object sender, IRibbonControl control, bool pressed)
        {
            PowerPoint.Selection currentSelection = null;
            PowerPoint.Shape tmpShape = null;
            PowerPoint.Slide currentSlide = null;
            String tempFilePath = Path.GetTempPath();
            selectedLogosForArrangement = new List<Image>();

            Image tmpLogo;

            try
            {
                currentSelection = PowerPointApp.ActiveWindow.Selection;
                currentSlide = (PowerPoint.Slide)PowerPointApp.ActiveWindow.View.Slide;

                Clipboard.Clear();

                Cursor.Current = Cursors.WaitCursor;

                if (currentSelection.ShapeRange.Count != 0)
                {
                    foreach (var shape in currentSelection.ShapeRange)
                    {
                        tmpShape = (PowerPoint.Shape)shape;
                        float shapeLeft = tmpShape.Left;
                        float shapeTop = tmpShape.Top;
                        

                        tmpShape.Copy();
                        tmpLogo = Clipboard.GetImage();

                        if (tmpLogo != null)
                        {
                            selectedLogosForArrangement.Add(tmpLogo);
                        }
                    }

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
                        //arz.Show(this);
                    }
                }
                else
                {
                    MessageBox.Show("Are you sure you made a selection?  Try again.");
                }
                Cursor.Current = Cursors.Default;
            }
            catch (COMException ComEx)
            {
                MessageBox.Show("Are you sure you made a selection?  Try again.");
                Debug.WriteLine("Exception: " + ComEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem creating your logo fill box.  Are you sure you have an open slide?");
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (currentSelection != null)
                    Marshal.ReleaseComObject(currentSelection);
                if (currentSlide != null)
                    Marshal.ReleaseComObject(currentSlide);
                if (tmpShape != null)
                    Marshal.ReleaseComObject(tmpShape);
            }
        }

        private void adxRibbonButtonFillLogos_OnClick(object sender, IRibbonControl control, bool pressed)
        {
            PowerPoint.Selection currentSelection = null;
            PowerPoint.Slide currentSlide = null;
            String tempFilePath = Path.GetTempPath();

            try
            {
                currentSelection = PowerPointApp.ActiveWindow.Selection;
                currentSlide = (PowerPoint.Slide)PowerPointApp.ActiveWindow.View.Slide;

                if (currentSelection != null && currentSelection.Type == PowerPoint.PpSelectionType.ppSelectionShapes) //&& currentSelection.ShapeRange.i.Name == "LGBT_BLOTTER")
                {
                    if (currentSelection.ShapeRange.Count == 1)
                    {
                        foreach (var arrangeShape in currentSelection.ShapeRange)
                        {
                            PowerPoint.Shape eventShape = arrangeShape as PowerPoint.Shape;
                            if (eventShape.AutoShapeType == Microsoft.Office.Core.MsoAutoShapeType.msoShapeRectangle)
                            {
                                PrepareLogoRectangleArrangement(eventShape, selectedLogosForArrangement);
                            }
                            else if (eventShape.AutoShapeType == Microsoft.Office.Core.MsoAutoShapeType.msoShapeOval)
                            {
                                PrepareLogoOvalArrangement(eventShape, selectedLogosForArrangement);
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
            catch (COMException ComEx)
            {
                MessageBox.Show("Are you sure you made a selection?  Try again.");
                Debug.WriteLine("Exception: " + ComEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem creating your logo fill box.  Are you sure you have an open slide?");
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (currentSelection != null)
                    Marshal.ReleaseComObject(currentSelection);
                if (currentSlide != null)
                    Marshal.ReleaseComObject(currentSlide);
            }
            Cursor.Current = Cursors.Default;
        }

        private void adxPowerPointTaskPanesManagerLogobot_ADXError(object sender, AddinExpress.PP.ADXErrorEventArgs e)
        {
            e.Handled = suppressErrors;
        }

        private void AddinModule_OnError(AddinExpress.MSO.ADXErrorEventArgs e)
        {
            e.Handled = suppressErrors;
        }
    }

    public class PositionLogo
    {
        public PositionLogo(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public PositionLogo(float x, float y, double logoSize)
        {
            this.x = x;
            this.y = y;
            this.logoSize = logoSize;
        }

        private float y;

        private float x;

        private double logoSize;

        public float X
        {
            get { return this.x; }
            set { x = value; }
        }

        public float Y
        {
            get { return this.y; }
            set { y = value; }
        }

        public double LogoSize
        {
            get { return this.logoSize; }
            set { logoSize = value; }
        }
    }

    public class ArrangeLogos
    {
        private LogobotPaneInstance logobotTaskPane;

        private PowerPoint.Shape eventShape;

        private List<Image> selectImages;

        private bool useSelectedImages;

        private bool fillAllBlotter;

        private SortedDictionary<string, PositionLogo> placementHash;

        private SortedDictionary<string, Image> logoHash;

        public LogobotPaneInstance LogobotTaskPane
        {
            get { return this.logobotTaskPane; }
            set { logobotTaskPane = value; }
        }

        public PowerPoint.Shape EventShape
        {
            get { return this.eventShape; }
            set { eventShape = value; }
        }

        public SortedDictionary<string, PositionLogo> PlacementHash
        {
            get { return this.placementHash; }
            set { placementHash = value; }
        }

        public SortedDictionary<string, Image> LogoHash
        {
            get { return this.logoHash; }
            set { logoHash = value; }
        }

        public List<Image> SelectImages
        {
            get { return this.selectImages; }
            set { selectImages = value; }
        }

        public bool UseSelectedImages
        {
            get { return this.useSelectedImages; }
            set { useSelectedImages = value; }
        }

        public bool FillAllBlotter
        {
            get { return this.fillAllBlotter; }
            set { fillAllBlotter = value; }
        }
    }

    public class LogobotUser
    {
        private string username;

        private string password;

        private string hashPassword;

        private string user_guid;

        private string computer_name;

        private bool loginResult;

        private bool paidSubscriber;

        private bool isTrialVersion;

        private DateTime installDate;

        private DateTime paidDate;

        public LogobotUser(string username, string password, string user_guid)
        {
            this.username = username;
            this.password = password;
            this.user_guid = user_guid;
        }

        public LogobotUser(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public LogobotUser()
        {

        }

        public string Username
        {
            get { return this.username; }
            set { username = value; }
        }

        public string Password
        {
            get { return this.password; }
            set { password = value; }
        }

        public string HashPassword
        {
            get { return this.hashPassword; }
            set { hashPassword = value; }
        }

        public string UserGUID
        {
            get { return this.user_guid; }
            set { user_guid = value; }
        }

        public string ComputerName
        {
            get { return this.computer_name; }
            set { computer_name = value; }
        }

        public bool LoginResult
        {
            get { return this.loginResult; }
            set { loginResult = value; }
        }

        public bool PaidSubscriber
        {
            get { return this.paidSubscriber; }
            set { paidSubscriber = value; }
        }

        public bool IsTrialVersion
        {
            get { return this.isTrialVersion; }
            set { isTrialVersion = value; }
        }

        public DateTime InstallDate
        {
            get { return this.installDate; }
            set { installDate = value; }
        }

        public DateTime PaidDate
        {
            get { return this.paidDate; }
            set { paidDate = value; }
        }
    }

    public class ScreenSources
    {
        private string sourceName;

        private string sourceCategoryTable;

        private string sourceCategoryLogoTable;

        public ScreenSources(string sourceName, string sourceCategoryTable, string sourceCategoryLogoTable)
        {
            this.sourceName = sourceName;
            this.sourceCategoryTable = sourceCategoryTable;
            this.sourceCategoryLogoTable = sourceCategoryLogoTable;
        }

        public ScreenSources()
        {

        }

        public string SourceName
        {
            get { return this.sourceName; }
            set { sourceName = value; }
        }

        public string SourceCategoryTable
        {
            get { return this.sourceCategoryTable; }
            set { sourceCategoryTable = value; }
        }

        public string SourceCategoryLogoTable
        {
            get { return this.sourceCategoryLogoTable; }
            set { sourceCategoryLogoTable = value; }
        }
    }

    public class LogobotPaneInstance
    {
        private ADXPowerPointTaskPane LogobotTaskPane;

        private TreeView paneTreeViewUser;

        private TreeView paneTreeViewScreen;

        private DataGridView paneDataGridViewUser;

        private DataGridView paneDataGridViewScreen;

        private DataGridView paneDataGridViewSearch;

        private DataGridView paneDataGridViewBlotter;

        private TabControl paneTabControl;

        private Button paneScreen;

        private Button paneScreenBack;

        private Button paneScreenMoveAll;

        private Button paneScreenMoveSelected;

        private Button paneUser;

        private Button paneUserBack;

        private Button paneUserMoveAll;

        private Button paneUserMoveSelected;

        private ImageList imageListTreeViews;

        public LogobotPaneInstance(ADXPowerPointTaskPane pptTaskPane)
        {
            this.LogobotTaskPane = pptTaskPane;
            getDataGridViews();
            getScreenControls();
            getTabControl();
        }

        private void getDataGridViews()
        {
            foreach (Control ctrl in LogobotTaskPane.Controls)
            {
                if (ctrl.GetType() == typeof(TabControl))
                {
                    TabControl tmp = ctrl as TabControl;

                    foreach (Control ctrl1 in tmp.Controls)
                    {
                        foreach (Control pages in tmp.TabPages)
                        {
                            TabPage tmpPages = pages as TabPage;

                            for (int i = 0; i < tmpPages.Controls.Count; i++)
                            {
                                if (tmpPages.Controls[i].GetType() == typeof(DataGridView))
                                {
                                    switch (tmpPages.Controls[i].Name)
                                    {
                                        case "dataGridViewUser":
                                            {
                                                this.paneDataGridViewUser = (DataGridView)tmpPages.Controls[i];
                                                break;
                                            }
                                        case "dataGridViewScreen":
                                            {
                                                this.paneDataGridViewScreen = (DataGridView) tmpPages.Controls[i];
                                                break;
                                            }
                                        case "dataGridViewSearch":
                                            {
                                                this.paneDataGridViewSearch = (DataGridView)tmpPages.Controls[i];
                                                break;
                                            }
                                        case "dataGridViewBlotter":
                                            {
                                                this.PaneDataGridViewBlotter = (DataGridView)tmpPages.Controls[i];
                                                break;
                                            }
                                        default:
                                            {
                                                break;
                                            }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void getTabControl()
        {
            foreach (Control ctrl in LogobotTaskPane.Controls)
            {
                if (ctrl.GetType() == typeof(TabControl))
                {
                    TabControl tmp = ctrl as TabControl;
                    if (tmp.Name == "tabControl1")
                    {
                        this.PaneTabControl = tmp;
                    }
                }
            }
        }

        private void getScreenControls()
        {
            foreach (Control ctrl in LogobotTaskPane.Controls)
            {
                if (ctrl.GetType() == typeof(TabControl))
                {
                    TabControl tmp = ctrl as TabControl;
                    if (tmp.Name == "tabControl1")
                    {
                        for (int i = 0; i < tmp.TabPages[0].Controls.Count; i++)
                        {
                            if (tmp.TabPages[0].Controls[i].GetType() == typeof(TreeView))
                            {
                                this.paneTreeViewUser = (TreeView)tmp.TabPages[0].Controls[i];
                            }
                            else if (tmp.TabPages[0].Controls[i].GetType() == typeof(Button))
                            {
                                switch (tmp.TabPages[0].Controls[i].Name)
                                {
                                    case "buttonUser":
                                        {
                                            this.paneUser = (Button)tmp.TabPages[0].Controls[i];
                                            break;
                                        }
                                    case "buttonUserBack":
                                        {
                                            this.paneUserBack = (Button)tmp.TabPages[0].Controls[i];
                                            break;
                                        }
                                    case "buttonMoveAllUser":
                                        {
                                            this.paneUserMoveAll = (Button)tmp.TabPages[0].Controls[i];
                                            break;
                                        }
                                    case "buttonMoveSelectedUser":
                                        {
                                            this.paneUserMoveSelected = (Button)tmp.TabPages[0].Controls[i];
                                            break;
                                        }
                                    default:
                                        {
                                            break;
                                        }
                                }
                            }
                        }

                        for (int i = 0; i < tmp.TabPages[1].Controls.Count; i++)
                        {
                            if (tmp.TabPages[1].Controls[i].GetType() == typeof(TreeView))
                            {
                                switch (tmp.TabPages[1].Controls[i].Name)
                                {
                                    case "treeViewScreen":
                                        {
                                            this.paneTreeViewScreen = (TreeView)tmp.TabPages[1].Controls[i];
                                            break;
                                        }
                                    default:
                                        {
                                            break;
                                        }
                                }
                            }
                            else if (tmp.TabPages[1].Controls[i].GetType() == typeof(Button))
                            {
                                switch (tmp.TabPages[1].Controls[i].Name)
                                {

                                    case "buttonScreen":
                                        {
                                            this.PaneScreen = (Button)tmp.TabPages[1].Controls[i];
                                            break;
                                        }
                                    case "buttonScreenBack":
                                        {
                                            this.paneScreenBack = (Button)tmp.TabPages[1].Controls[i];
                                            break;
                                        }
                                    case "buttonMoveAllScreen":
                                        {
                                            this.paneScreenMoveAll = (Button)tmp.TabPages[1].Controls[i];
                                            break;
                                        }
                                    case "buttonMoveSelectedScreen":
                                        {
                                            this.PaneScreenMoveSelected = (Button)tmp.TabPages[1].Controls[i];
                                            break;
                                        }
                                    default:
                                        {
                                            break;
                                        }
                                }
                            }
                        }
                    }
                }
            }
        }

        public TreeView PaneTreeViewUser
        {
            get { return this.paneTreeViewUser; }
            set { paneTreeViewUser = value; }
        }

        public TreeView PaneTreeViewScreen
        {
            get { return this.paneTreeViewScreen; }
            set { paneTreeViewScreen = value; }
        }

        public DataGridView PaneDataGridViewUser
        {
            get { return this.paneDataGridViewUser; }
            set { paneDataGridViewUser = value; }
        }

        public DataGridView PaneDataGridViewScreen
        {
            get { return this.paneDataGridViewScreen; }
            set { paneDataGridViewScreen = value; }
        }

        public DataGridView PaneDataGridViewSearch
        {
            get { return this.paneDataGridViewSearch; }
            set { paneDataGridViewSearch = value; }
        }

        public DataGridView PaneDataGridViewBlotter
        {
            get { return this.paneDataGridViewBlotter; }
            set { paneDataGridViewBlotter = value; }
        }

        public TabControl PaneTabControl
        {
            get { return this.paneTabControl; }
            set { paneTabControl = value; }
        }

        public Button PaneUser
        {
            get { return this.paneUser; }
            set { paneUser = value; }
        }

        public Button PaneUserBack
        {
            get { return this.paneUserBack; }
            set { paneUserBack = value; }
        }

        public Button PaneUserMoveAll
        {
            get { return this.paneUserMoveAll; }
            set { paneUserMoveAll = value; }
        }

        public Button PaneUserMoveSelected
        {
            get { return this.paneUserMoveSelected; }
            set { paneUserMoveSelected = value; }
        }

        public Button PaneScreen
        {
            get { return this.paneScreen; }
            set { paneScreen = value; }
        }

        public Button PaneScreenBack
        {
            get { return this.paneScreenBack; }
            set { paneScreenBack = value; }
        }

        public Button PaneScreenMoveAll
        {
            get { return this.paneScreenMoveAll; }
            set { paneScreenMoveAll = value; }
        }

        public Button PaneScreenMoveSelected
        {
            get { return this.paneScreenMoveSelected; }
            set { paneScreenMoveSelected = value; }
        }

        public ImageList ImageListTreeViews
        {
            get { return this.imageListTreeViews; }
            set { imageListTreeViews = value; }
        }
    }
}
