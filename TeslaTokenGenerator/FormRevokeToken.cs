using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace TeslaTokenGenerator {
    public partial class FormRevokeToken : Form {
        const string CLIENT_ID = "81527cff06843c8634fdc09e8ac0abefb46ac849f38fe1e431c2ef2106796384";
        const string CLIENT_SECRET = "c7257eb71a564034f9419ee651c7d0e5f7aa6bfbd18bafb5c5c033b093bb2fa3";
        const string AUTH_URI = @"https://owner-api.teslamotors.com/oauth/";
        const string REVOKE_URI = @"https://owner-api.teslamotors.com/oauth/revoke";

        TeslaUtils tesla;
        string vehicleID;
        TeslaAccessToken token;
        string accessToken;
        
        public FormRevokeToken() {
            InitializeComponent();
            tesla = new TeslaUtils();
        }

        public FormRevokeToken(string AccessToken) {
            if (AccessToken.Length < 1)
                throw new ArgumentException("Access token revoke to authenticate", "AccessToken");

            InitializeComponent();
            tesla = new TeslaUtils();


            accessToken = AccessToken;
        }

        private void buttonRevoke_Click(object sender, EventArgs e) {
            bool tokenRevoked = tesla.RevokeToken(token);

            if (tokenRevoked) {
                MessageBox.Show("Successfully revoked the following token:\n" + token.AccessToken, "Token Revoked", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        
        private void FormRevokeToken_Load(object sender, EventArgs e) {
            textBoxNote.BackColor = Color.Azure;
        }

        static void SetCursorToDefault() {
            Cursor.Current = Cursors.Default;
        }

        static void SetCursorToWait() {
            Cursor.Current = Cursors.WaitCursor;
        }

        private void buttonTestToken_Click(object sender, EventArgs e) {
            List<VehicleDetails> vehicles;

            textBoxToken.Text = textBoxToken.Text.Trim();
            if (textBoxToken.Text.Length < 1) {
                MessageBox.Show("Please enter a token.", "Missing token", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try {
                SetCursorToWait();
                token = new TeslaAccessToken(textBoxToken.Text);
                tesla.AccessToken = token;

                vehicles = tesla.LoadVehicles();

                StringBuilder details = new StringBuilder();

                details.AppendLine("Total " + vehicles.Count.ToString() + " vehicle(s) found.");
                foreach (var vehicle in vehicles) {
                    string s = "NAME: " + vehicle.DisplayName + ", MODEL: " + tesla.GetModel(vehicle) + ", VIN: " + vehicle.VIN + " STATE: " + vehicle.State;
                    details.AppendLine(s);

                    Debug.Write(vehicle.ToString());
                }

                if (vehicles.Count == 1) {
                    this.vehicleID = vehicles[0].ID;
                }

                VehicleData data = new VehicleData();
                data = tesla.GetAllVehicleData(this.vehicleID, token.AccessToken);
                Debug.WriteLine(data.ToString());

                SetCursorToDefault();
                MessageBox.Show(details.ToString(), "Vehicle details", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) {
                SetCursorToDefault();
                Debug.WriteLine(ex.ToString());
                MessageBox.Show("Following error occured:\n" + ex.ToString());
            }
        }
    }
}
