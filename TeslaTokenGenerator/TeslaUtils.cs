using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace TeslaTokenGenerator {
    class TeslaUtils {

        public const string CLIENT_ID = "81527cff06843c8634fdc09e8ac0abefb46ac849f38fe1e431c2ef2106796384";
        public const string CLIENT_SECRET = "c7257eb71a564034f9419ee651c7d0e5f7aa6bfbd18bafb5c5c033b093bb2fa3";

        public const string GRANT_TYPE_PASSWORD = "password";

        public const string BASE_AUTH_URI = @"https://owner-api.teslamotors.com/oauth/";
        public const string BASE_REVOKE_URI = @"https://owner-api.teslamotors.com/oauth/revoke"; 
        public const string BASE_VEHICLES_URI = @"https://owner-api.teslamotors.com/api/1/vehicles";
        public const string STATE_VEHICLE_URI = @"https://owner-api.teslamotors.com/api/1/vehicles/{0}/data_request/vehicle_state";
        public const string STATE_CHARGE_URI = @"https://owner-api.teslamotors.com/api/1/vehicles/{0}/data_request/charge_state";
        public const string STATE_MOBILE_ENABLED_URI = @"https://owner-api.teslamotors.com/api/1/vehicles/{0}/mobile_enabled";
        public const string STATE_CLIMATESETTING_URI = @"https://owner-api.teslamotors.com/api/1/vehicles/{0}/data_request/climate_state";
        public const string STATE_DRIVING_POSITION_URI = @"https://owner-api.teslamotors.com/api/1/vehicles/{0}/data_request/drive_state";
        public const string STATE_GUI_SETTING_URI = @"https://owner-api.teslamotors.com/api/1/vehicles/{0}/data_request/gui_settings";
        public const string ACTION_WAKEUP_VEHICLE_URI = @"https://owner-api.teslamotors.com/api/1/vehicles/{0}/wake_up";
        public const string ACTION_STOP_CHARGING_URI = @"https://owner-api.teslamotors.com/api/1/vehicles/{0}/command/charge_stop";
        public const string ACTION_START_CHARGING_URI = @"https://owner-api.teslamotors.com/api/1/vehicles/{0}/command/charge_start";
        public const string ACTION_FLASH_LIGHTS_URI = @"https://owner-api.teslamotors.com/api/1/vehicles/{0}/command/flash_lights";
        public const string ACTION_HONK_HORN_URI = @"https://owner-api.teslamotors.com/api/1/vehicles/{0}/command/honk_horn";


        string loginEmail;
        string loginPassword;
        string lastError;

        bool loggedIn = false;
        bool vehicleAwake = false;

        TeslaAccessToken token;
        List<VehicleDetails> vehicles;

        public string LoginEmail {
            get {
                return this.loginEmail;
            }
            set {
                if (IsValidEmail(value))
                    this.loginEmail = value;
                else
                    throw new Exception("Invalid email address");
            }
        }

        public bool VehicleAwake {
            get {
                return this.vehicleAwake;
            }
        }

        public string LoginPassword {
            get {
                return this.loginPassword;
            }
            set {
                if (value.Length > 1) {
                    this.loginPassword = value;
                }
                else
                    throw new Exception("Password cannot be blank");
            }
        }

        public TeslaAccessToken AccessToken {
            get {
                return this.token;
            }
            set {
                this.token = value;
                this.loggedIn = true;
            }
        }

        public bool RevokeToken(TeslaAccessToken token) {
            return this.RevokeToken(token.AccessToken);
        }

        public bool RevokeToken(string token) {
            bool tokenRevoked = false;

            if (token is null || token.Trim().Length < 1)
                throw new ArgumentNullException("token");

            var client = new RestClient(BASE_REVOKE_URI);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Authorization", "Bearer " + token);
            var response = client.Execute(request);

            if (!response.IsSuccessful) {
                lastError = "Could not revoke token:" + token;
                tokenRevoked = false;
                throw new Exception(lastError, new Exception(response.ToString()));
            }

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                tokenRevoked = true;
            
            return tokenRevoked;
        }

        public bool LoggedIn {
            get {
                return this.loggedIn;
            }
        }

        public List<VehicleDetails> Vehicles {
            get {
                return this.vehicles;
            }
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

        public bool Login(string email, string password) {
            this.LoginEmail = email;
            this.LoginPassword = password;

            return Login();
        }

        public bool Login() {
            RestClient client = new RestClient(BASE_AUTH_URI);
            var request = new RestRequest("token");
            request.RequestFormat = DataFormat.Json;
            request.AddBody(
                new {
                    grant_type = GRANT_TYPE_PASSWORD,
                    client_id = CLIENT_ID,
                    client_secret = CLIENT_SECRET,
                    email = loginEmail,
                    password = loginPassword
                }
            );

            try {
                var response = client.Post<TeslaAccessToken>(request);

                if(response.Data is null) {
                    loggedIn = false;
                    lastError = response.ErrorMessage;
                    throw response.ErrorException;
                }

                token = response.Data;

                if (response.StatusCode != System.Net.HttpStatusCode.OK) {
                    loggedIn = false;
                    lastError = response.StatusDescription;
                }
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.ToString());
            }

            this.loggedIn = true;

            return true;
        }


        public string LastError {
            get {
                return this.lastError;
            }
        }

        TeslaAccessToken GetNewToken(string email, string password) {
            this.LoginEmail = email;
            this.LoginPassword = password;

            if (Login())
                return this.AccessToken;
            else
                return null;
        }

        public List<VehicleDetails> LoadVehicles(string AccessToken) {
            if (AccessToken is null)
                throw new ArgumentNullException("AccessToken");

            RestClient client = new RestClient(BASE_VEHICLES_URI);
            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Authorization", "Bearer " + AccessToken);

            var response = client.Execute(request);

            if (!response.IsSuccessful) {
                lastError = "Could not Load Vehicles using Access token: " + AccessToken;
                throw new Exception(lastError, new Exception(response.ToString()));
            }

            JObject jsonResponse = (JObject)JsonConvert.DeserializeObject(response.Content);
            vehicles = JsonConvert.DeserializeObject<List<VehicleDetails>>(jsonResponse["response"].ToString());

            return vehicles;
        }

        public List<VehicleDetails> LoadVehicles() {
            if (!loggedIn) {
                lastError = "Not logged in. Please login first, by calling the Login() method";
                throw new Exception(lastError);
            }

            return LoadVehicles(this.token.AccessToken);
        }

        public string GetModel(VehicleDetails vehicle) {
            string model = "Unknown";
            
            if (vehicle is null) {
                throw new ArgumentNullException("vehicle");
            }

            if (vehicle.OptionCodes.Contains("MDLX"))
                model = "Model X";
            else if (vehicle.OptionCodes.Contains("MDL3")) {
                model = "Model 3";
            }
            else
                model = "Model S";

            return model;
        }

       
        public bool WakeUpVechile(string VehicleID, string AccessToken) {
            if (VehicleID is null)
                throw new ArgumentNullException("vehicleID");

            if (AccessToken is null)
                throw new ArgumentNullException("AccessToken");

            string url = String.Format(ACTION_WAKEUP_VEHICLE_URI, VehicleID);
            List<VehicleDetails> v;
            bool vehicleAwake = false;

            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Authorization", "Bearer " + AccessToken);
            var response = client.Execute(request);

            if (!response.IsSuccessful) {
                lastError = "Could not get Vehicle State details for Vehicle ID:" + VehicleID;
                throw new Exception(lastError, new Exception(response.ToString()));
            }

            JObject jsonResponse = (JObject)JsonConvert.DeserializeObject(response.Content);

            //If the call is successful, it returns a VehicleDetails; else it returns a VehicleWakeUp
            //this isn't very clean and possibly can be done better to check type
            try {
                v = JsonConvert.DeserializeObject<List<VehicleDetails>>(jsonResponse["response"].ToString());
            }
            catch (Newtonsoft.Json.JsonSerializationException serializationEx) {
                vehicleAwake = false;
                Debug.WriteLine("Wake up Vehicle, tried to serialize response to <VehicleDetails>\n" + serializationEx.Message);
                throw;
            }

            if (!(v is null) && (v.Count > 0)) {
                vehicleAwake = true;
            }

            return vehicleAwake;
        }

        public VehicleState GetVehicleState(string VehicleID, string AccessToken) {
            if (VehicleID is null)
                throw new ArgumentNullException("VehicleID");

            if (AccessToken is null)
                throw new ArgumentNullException("AccessToken");
              
            if (!vehicleAwake)
                vehicleAwake = WakeUpVechile(VehicleID, AccessToken);

            string url = String.Format(STATE_VEHICLE_URI, VehicleID);
            VehicleState state;

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Authorization", "Bearer " + AccessToken);
            var response = client.Execute(request);

            if (!response.IsSuccessful) {
                lastError = "Could not get Vehicle State details for Vehicle ID:" + VehicleID;
                throw new Exception(lastError, new Exception(response.ToString()));
            }

            JObject jsonResponse = (JObject)JsonConvert.DeserializeObject(response.Content);
            state = JsonConvert.DeserializeObject<VehicleState>(jsonResponse["response"].ToString());
            
            Debug.WriteLine(response.Content);
            Debug.WriteLine(state.ToString());

            return state;
        }
        
        public VehicleClimateState GetVehicleClimateState(string VehicleID, string AccessToken) {
            if (VehicleID is null)
                throw new ArgumentNullException("VehicleID");

            if (AccessToken is null)
                throw new ArgumentNullException("AccessToken");

            if (!vehicleAwake)
                vehicleAwake = WakeUpVechile(VehicleID, AccessToken);

            string url = String.Format(STATE_CLIMATESETTING_URI, VehicleID);
            VehicleClimateState climateState;

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Authorization", "Bearer " + AccessToken);
            var response = client.Execute(request);

            if (!response.IsSuccessful) {
                lastError = "Could not get Vehicle State details for Vehicle ID:" + VehicleID;
                throw new Exception(lastError, new Exception(response.ToString()));
            }

            JObject jsonResponse = (JObject)JsonConvert.DeserializeObject(response.Content);
            climateState = JsonConvert.DeserializeObject<VehicleClimateState>(jsonResponse["response"].ToString());

            Debug.WriteLine(response.ToString());
            Debug.WriteLine(climateState.ToString());

            return climateState;
        }

        public VehicleDriveState GetVehicleDriveState(string VehicleID, string AccessToken) {
            if (VehicleID is null)
                throw new ArgumentNullException("VehicleID");

            if (AccessToken is null)
                throw new ArgumentNullException("AccessToken");

            if (!vehicleAwake)
                vehicleAwake = WakeUpVechile(VehicleID, AccessToken);

            string url = String.Format(STATE_DRIVING_POSITION_URI, VehicleID);
            VehicleDriveState driveState;

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Authorization", "Bearer " + AccessToken);
            var response = client.Execute(request);

            if (!response.IsSuccessful) {
                lastError = "Could not get Vehicle State details for Vehicle ID:" + VehicleID;
                throw new Exception(lastError, new Exception(response.ToString()));
            }

            JObject jsonResponse = (JObject)JsonConvert.DeserializeObject(response.Content);
            driveState = JsonConvert.DeserializeObject<VehicleDriveState>(jsonResponse["response"].ToString());

            Debug.WriteLine(response.ToString());
            Debug.WriteLine(driveState.ToString());

            return driveState;
        }

        public VehicleGUISettings GetVehicleGUIState(string VehicleID, string AccessToken) {
            if (VehicleID is null)
                throw new ArgumentNullException("VehicleID");

            if (AccessToken is null)
                throw new ArgumentNullException("AccessToken");

            if (!vehicleAwake)
                vehicleAwake = WakeUpVechile(VehicleID, AccessToken);

            string url = String.Format(STATE_GUI_SETTING_URI, VehicleID);
            VehicleGUISettings guiState;

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Authorization", "Bearer " + AccessToken);
            var response = client.Execute(request);

            if (!response.IsSuccessful) {
                lastError = "Could not get Vehicle State details for Vehicle ID:" + VehicleID;
                throw new Exception(lastError, new Exception(response.ToString()));
            }

            JObject jsonResponse = (JObject)JsonConvert.DeserializeObject(response.Content);
            guiState = JsonConvert.DeserializeObject<VehicleGUISettings>(jsonResponse["response"].ToString());

            Debug.WriteLine(response.ToString());
            Debug.WriteLine(guiState.ToString());

            return guiState;
        }
    }
}
