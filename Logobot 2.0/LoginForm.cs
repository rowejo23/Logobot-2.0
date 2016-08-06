using Logobot2_0.Properties;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace Logobot2_0
{
    public partial class LoginForm : Form
    {
        private bool cancelledForm = false;
        AddinModule addinModule;

        public bool CancelledForm
        {
            get { return cancelledForm; }

            set { cancelledForm = value; }
        }

        public LoginForm()
        {
            InitializeComponent();
            this.textBoxComputerName.Text = Environment.MachineName;
        }

        public LoginForm(AddinModule addinModule)
        {
            InitializeComponent();
            this.textBoxComputerName.Text = Environment.MachineName;
            this.addinModule = addinModule;
        }

        private void textBoxUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LogobotUser currentUser;

                string username = textBoxUsername.Text.Trim();
                string password = textBoxPassword.Text.Trim();

                if (username == "" || password == "")
                {
                    MessageBox.Show("You must enter a username and password.");
                }
                else
                {
                    Cursor.Current = Cursors.WaitCursor;
                    currentUser = new LogobotUser(username, password);
                    loginLogobot(currentUser);
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        private void textBoxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LogobotUser currentUser;

                string username = textBoxUsername.Text.Trim();
                string password = textBoxPassword.Text.Trim();

                if (username == "" || password == "")
                {
                    MessageBox.Show("You must enter a username and password.");
                }
                else
                {
                    Cursor.Current = Cursors.WaitCursor;
                    currentUser = new LogobotUser(username, password);
                    loginLogobot(currentUser);
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            LogobotUser currentUser;

            string username = textBoxUsername.Text.Trim();
            string password = textBoxPassword.Text.Trim();

            if(username == "" || password == "")
            {
                MessageBox.Show("You must enter a username and password.");
            }
            else
            { 
                Cursor.Current = Cursors.WaitCursor;
                currentUser = new LogobotUser(username, password);
                loginLogobot(currentUser);
                Cursor.Current = Cursors.Default;
            }
        }

        private void loginLogobot(LogobotUser currentUser)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            path = Path.Combine(path, "Logobot");
            string pathXmlDoc = Path.Combine(path, "LogobotSettings.xml");

            loginUser(currentUser);

            if (!currentUser.LoginResult)
            {
                this.labelLoginResult.Visible = true;
                this.labelLoginResult.Text = "Username not valid. Try again.";
            }
            else
            {
                //string hashedPassword = Security.HashSHA1(currentUser.Password + currentUser.UserGUID);
                bool passwordMatch = CryptSharp.PhpassCrypter.CheckPassword(currentUser.Password, currentUser.HashPassword);
                string machineName = Environment.MachineName;

                if (passwordMatch) // (currentUser.HashPassword == hashedPassword)
                {
                    // First check if this is the first time a user has logged in.  This will only happen if the 
                    // install date and computer name are both null in the database.  If this happens
                    // then update the database with current date as install date and current machine name
                    // as the users computer name.
                    if (currentUser.InstallDate == DateTime.MinValue && (currentUser.ComputerName == null || currentUser.ComputerName == ""))
                    {
                        currentUser.InstallDate = DateTime.Now;
                        currentUser.ComputerName = machineName;

                        if (updateUserDetails(currentUser))
                        {
                            if (currentUser.PaidSubscriber) // Test first if the user has an annual paid subscription
                            {
                                if ((DateTime.Now - currentUser.PaidDate).TotalDays > 365)
                                {
                                    MessageBox.Show("Your Logobot license has expired.  Click the My Account button to buy another year's subscription.");
                                    addinModule.disableLogobot(false);
                                    this.Close();
                                }
                                else
                                {
                                    this.labelLoginResult.Visible = true;
                                    this.labelLoginResult.Text = "Login successful.";
                                    addinModule.enableLogobot(currentUser.Username);
                                    saveUserLoginDetails(path, pathXmlDoc, currentUser);
                                    addinModule.getUserSettings.Load(pathXmlDoc);
                                    addinModule.getSetCurrentUser = currentUser;
                                    this.Close();
                                }
                            }
                            else
                            {
                                // If the user does not have a paid subscription, then they have a trial version.
                                // The trial version expires in 30 days from the install date, so first test to see
                                // if the user is within the trial period.
                                string trialMessage;
                                if ((DateTime.Now - currentUser.InstallDate).TotalDays <= 30)
                                {
                                    trialMessage = "Your Logobot trial version will expire in " + (30 - Convert.ToInt32((DateTime.Now - currentUser.InstallDate).TotalDays)) + " days.  Click the My Account button to buy a full version.";
                                    TrialPopup trial = new TrialPopup(trialMessage);
                                    trial.ShowDialog();
                                    addinModule.enableLogobot(currentUser.Username);
                                    saveUserLoginDetails(path, pathXmlDoc, currentUser);
                                    addinModule.getUserSettings.Load(pathXmlDoc);
                                    addinModule.getSetCurrentUser = currentUser;
                                    this.Close();
                                }
                                else
                                {
                                    trialMessage = "Your Logobot trial version has expired.  Click the My Account button to buy a full version.";
                                    TrialPopup trial = new TrialPopup(trialMessage);
                                    trial.ShowDialog();
                                    addinModule.disableLogobot(false);
                                    this.Close();
                                }
                            }
                        }
                    }
                    // This is not the first time a user has logged in so check if machine name matches, 
                    // then if the machine name matches check if it is a trial user and if so, whether that has expired.
                    // If a paid subscriber then check that license is still valid and not expired.
                    else
                    {
                        if (currentUser.ComputerName != machineName)
                        {
                            MessageBox.Show("Your username and password are not registered to work on this computer");
                            addinModule.disableLogobot(false);
                            this.Close();
                        }
                        else
                        {
                            if (currentUser.PaidSubscriber)
                            {
                                if ((DateTime.Now - currentUser.PaidDate).TotalDays > 365)
                                {
                                    MessageBox.Show("Your Logobot license has expired.  Click the My Account button to buy another year's subscription.");
                                    addinModule.disableLogobot(false);
                                    this.Close();
                                }
                                else
                                {
                                    this.labelLoginResult.Visible = true;
                                    this.labelLoginResult.Text = "Login successful.";
                                    addinModule.enableLogobot(currentUser.Username);
                                    saveUserLoginDetails(path, pathXmlDoc, currentUser);
                                    addinModule.getUserSettings.Load(pathXmlDoc);
                                    addinModule.getSetCurrentUser = currentUser;
                                    this.Close();
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
                                    addinModule.enableLogobot(currentUser.Username);
                                    saveUserLoginDetails(path, pathXmlDoc, currentUser);
                                    addinModule.getUserSettings.Load(pathXmlDoc);
                                    addinModule.getSetCurrentUser = currentUser;

                                    this.Close();
                                }
                                else
                                {
                                    trialMessage = "Your Logobot trial version has expired.  Click the My Account button to buy a full version.";
                                    TrialPopup trial = new TrialPopup(trialMessage);
                                    trial.ShowDialog();
                                    addinModule.disableLogobot(false);
                                    this.Close();
                                }
                            }
                        }
                    }
                }
                else
                {
                    this.labelLoginResult.Visible = true;
                    this.labelLoginResult.Text = "Password is not valid. Try again.";
                    addinModule.disableLogobot(false);
                }
            }
        }

        private void saveUserLoginDetails(string path, string pathXmlDoc, LogobotUser currentUser)
        {
            // Create folder if it doesn't already exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // Delete any previous file if it exists
            if (File.Exists(pathXmlDoc))
            {
                File.Delete(pathXmlDoc);
            }

            LogobotSettings userSettings = new LogobotSettings();
            userSettings.Create();
            userSettings.Write("username", currentUser.Username);
            userSettings.Write("password", currentUser.HashPassword);
            userSettings.Write("noLogo", false);
            userSettings.Write("showArrangeTip", true);
            userSettings.Save(pathXmlDoc);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.cancelledForm = true;
            this.Close();
        }

        private void loginUser(LogobotUser currentUser)
        {
            currentUser.LoginResult = false;
            LogobotAPIClient apiClient = new LogobotAPIClient();

            using (var client = apiClient.getHTTPClient())
            {
                HttpResponseMessage response = client.PostAsJsonAsync<LogobotUser>("api/LogobotUser/usernamelogin", currentUser).Result;
                if (response.IsSuccessStatusCode)
                {
                    LogobotUser tmpUser = new LogobotUser();
                    tmpUser = response.Content.ReadAsAsync<LogobotUser>().Result;
                    if (tmpUser != null)
                    {
                        currentUser.LoginResult = tmpUser.LoginResult;
                        currentUser.HashPassword = tmpUser.HashPassword;
                        currentUser.UserGUID = tmpUser.UserGUID;
                        currentUser.ComputerName = tmpUser.ComputerName;
                        currentUser.PaidSubscriber = tmpUser.PaidSubscriber;
                        currentUser.InstallDate = tmpUser.InstallDate;
                        currentUser.PaidDate = tmpUser.PaidDate;
                    }
                }
            }
        }

        private bool updateUserDetails(LogobotUser currentUser)
        {
            bool updateSuccess = false;
            LogobotAPIClient apiClient = new LogobotAPIClient();

            using (var client = apiClient.getHTTPClient())
            {
                HttpResponseMessage response = client.PostAsJsonAsync<LogobotUser>("api/LogobotUser/updateuserdetails", currentUser).Result;
                if (response.IsSuccessStatusCode)
                {
                    updateSuccess = response.Content.ReadAsAsync<bool>().Result;
                }
            }

            return updateSuccess;
        }
    }

    public class Security
    {
        public static string HashSHA1(string value)
        {
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var inputBytes = Encoding.ASCII.GetBytes(value);
            var hash = sha1.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
