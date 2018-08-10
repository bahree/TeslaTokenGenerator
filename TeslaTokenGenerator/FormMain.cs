using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp;

namespace TeslaTokenGenerator
{
    public partial class FormMain : Form
    {
        TeslaUtils tesla;
        string vehicleID;
        bool vehicleAwake = false;
        
        public FormMain()
        {
            InitializeComponent();

            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            tesla = new TeslaUtils();
        }

        static void SetCursorToDefault() {
            Cursor.Current = Cursors.Default;
        }

        static void SetCursorToWait() {
            Cursor.Current = Cursors.WaitCursor;
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e) {
            SetCursorToDefault();

            // Log the exception, display it, etc
            Debug.WriteLine(e.Exception.Message);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
            SetCursorToWait();

            // Log the exception, display it, etc
            Debug.WriteLine((e.ExceptionObject as Exception).Message);
        }

        private void buttonGetToken_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(ValidateInput()))
                {
                    MessageBox.Show("Please enter valid email and password to login", "Mssing credentials", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                
                tesla.LoginEmail = textBoxEmail.Text;
                tesla.LoginPassword = textBoxPassword.Text;

                SetCursorToWait();
           
                if (!tesla.Login(textBoxEmail.Text, textBoxPassword.Text)) {
                    SetCursorToDefault();
                    ButtonStatus(false);
                    toolStripStatusLabel1.Text = "Login failed";
                    MessageBox.Show("Login failed with the following error. Please check credentials and try again. \n\n" +
                        tesla.LastError, "Login error",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    CleanUp();

                    return;
                }

                if (tesla.AccessToken.AccessToken != null) {
                    toolStripStatusLabel1.Text = "Login Successful; token generated";
                    ButtonStatus(true);

                    textBoxToken.Text = tesla.AccessToken.AccessToken;
                    textBoxRefreshToken.Text = tesla.AccessToken.RefreshToken;
                    textBoxTokenCreation.Text = Utils.ConvertUnixTimeToLocalTime(tesla.AccessToken.CreatedAt).ToString();
                    textBoxTokenExpiry.Text = Utils.ConvertUnixTimeToLocalTime(tesla.AccessToken.CreatedAt + tesla.AccessToken.ExpiresIn).ToString();
                }
                else {
                    ButtonStatus(false);
                    toolStripStatusLabel1.Text = "Login failed";
                }

                SetCursorToDefault();
            }
            catch (Exception ex)
            {
                SetCursorToDefault();
                Debug.WriteLine(ex.ToString());
                MessageBox.Show("Following error occured:\n" + ex.ToString(), "Error Occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            return (IsValidEmail(textBoxEmail.Text) && (textBoxPassword.Text.Length > 0));
        }

        bool IsValidEmail(string email) {
            try {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch {
                return false;
            }
        }

        private void checkBoxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            textBoxPassword.PasswordChar = checkBoxShowPassword.Checked ? '\0' : '*';
        }

        private void textBoxEmail_Validating(object sender, CancelEventArgs e)
        {
            textBoxEmail.Text = textBoxEmail.Text.Trim();
        }

        private void textBoxPassword_Validating(object sender, CancelEventArgs e)
        {
            textBoxPassword.Text = textBoxPassword.Text.Trim();
        }

        private void buttonClipboardCopy_Click(object sender, EventArgs e)
        {
            StringBuilder s = new StringBuilder();
            s.AppendLine("AccessToken: " + tesla.AccessToken.AccessToken);
            s.AppendLine("Created at: " + textBoxTokenCreation.Text);
            s.AppendLine("Expires at: " + textBoxTokenExpiry.Text);
            s.AppendLine("Refresh AccessToken: " + tesla.AccessToken.RefreshToken);

            if (!checkBoxCopyAllTokenDetails.Checked) {
                Clipboard.SetText(tesla.AccessToken.AccessToken);
                toolStripStatusLabel1.Text = "Access token copied to the clipboard";
            }
            else {
                Clipboard.SetText(s.ToString());
                toolStripStatusLabel1.Text = "All access token details copied to the clipboard";
            }
        }

        private void CleanUp()
        {
            textBoxRefreshToken.Text = "";
            textBoxToken.Text = "";
            textBoxTokenCreation.Text = "";
            textBoxTokenExpiry.Text = "";
            SetCursorToDefault();
            ButtonStatus(false);
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxEmail.Text = "";
            textBoxPassword.Text = "";
            CleanUp();
            toolStripStatusLabel1.Text = "Cleared all values";
        }

        private void buttonGenerateCuRL_Click(object sender, EventArgs e)
        {
            if(!ValidateInput())
            {
                MessageBox.Show("Login email and/or password missing. Please enter and try again.", "Login details missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string curl = "curl -X POST -H \"Cache-Control: no-cache\" -H \"Content-Type: multipart/form-data; " +
                "boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW\" -F \"grant_type=password\" -F \"client_id=" +
                TeslaUtils.CLIENT_ID + "\" -F \"client_secret=" + TeslaUtils.CLIENT_SECRET + "\" -F \"email=" +
                textBoxEmail.Text + "\" -F \"password=" + textBoxPassword.Text + 
                "\" \"https://owner-api.teslamotors.com/oauth/token\"";

            Debug.WriteLine(curl);
            Clipboard.SetText(curl);
            toolStripStatusLabel1.Text = "cURL details copied to clipboard";
            MessageBox.Show("cURL details copied to clipboard", "All done", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void ButtonStatus(bool enabled) {
            buttonClipboardCopy.Enabled = enabled;
            buttonTestToken.Enabled = enabled;
        }
        
        private void buttonTestToken_Click(object sender, EventArgs e) {
            List<VehicleDetails> vehicles;

            try {
                SetCursorToWait();
                vehicles = tesla.LoadVehicles();
                
                StringBuilder details = new StringBuilder();

                details.AppendLine("Total " + vehicles.Count.ToString() + " vehicle(s) found.");
                foreach (var vehicle in vehicles) {
                    string temp = "NAME: " + vehicle.DisplayName + ", MODEL: " + tesla.GetModel(vehicle) + ", VIN: " + vehicle.VIN + " STATE: " + vehicle.State;
                    details.AppendLine(temp);

                    Debug.Write(vehicle.ToString());
                }

                if (vehicles.Count == 1) {
                    this.vehicleID = vehicles[0].ID;
                    if (vehicles[0].State == "online")
                        this.vehicleAwake = true;
                    else
                        this.vehicleAwake = false;
                }

                SetCursorToDefault();
                MessageBox.Show(details.ToString(), "Vehicle details", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex) {
                SetCursorToDefault();
                Debug.WriteLine(ex.ToString());
                MessageBox.Show("Following error occured:\n" + ex.ToString());
            }
        }

        private void buttonRevokeToken_Click(object sender, EventArgs e) {
            FormRevokeToken form = new FormRevokeToken();
            form.ShowDialog();
        }

        private void exitToolStripMenuItemExit_Click(object sender, EventArgs e) {
            if (Application.AllowQuit)
                Application.Exit();
            else
                MessageBox.Show("Application not allowed to quit.");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
            //MessageBox.Show("Tesla Token Generator\n" + Application.ProductVersion + "\n\n (c) Amit Bahree, 2018, https://desigeek.com");
            AboutBox formAboutBox = new AboutBox();
            formAboutBox.ShowDialog();
        }
    }
}
