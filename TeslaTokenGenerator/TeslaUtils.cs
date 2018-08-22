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

        public const string AUTHENTICATE_URI = @"https://owner-api.teslamotors.com/oauth/";
        public const string REVOKE_AUTH_URI = @"https://owner-api.teslamotors.com/oauth/revoke"; 
        public const string VEHICLE_LIST_URI = @"https://owner-api.teslamotors.com/api/1/vehicles";
        public const string VEHICLE_SUMMARY_URI = @"https://owner-api.teslamotors.com/api/1/vehicles/{0}"; //new
        public const string STATE_VEHICLE_URI = @"https://owner-api.teslamotors.com/api/1/vehicles/{0}/data_request/vehicle_state";
        public const string STATE_CHARGE_URI = @"https://owner-api.teslamotors.com/api/1/vehicles/{0}/data_request/charge_state";
        public const string STATE_MOBILE_ENABLED_URI = @"https://owner-api.teslamotors.com/api/1/vehicles/{0}/mobile_enabled";
        public const string STATE_CLIMATESETTING_URI = @"https://owner-api.teslamotors.com/api/1/vehicles/{0}/data_request/climate_state";
        public const string STATE_DRIVING_POSITION_URI = @"https://owner-api.teslamotors.com/api/1/vehicles/{0}/data_request/drive_state";
        public const string STATE_GUI_SETTING_URI = @"https://owner-api.teslamotors.com/api/1/vehicles/{0}/data_request/gui_settings";
        public const string VEHICLE_DATA_URI = @"https://owner-api.teslamotors.com/api/1/vehicles/{0}/data";
        public const string VEHICLE_WAKEUP_URI = @"https://owner-api.teslamotors.com/api/1/vehicles/{0}/wake_up";
        public const string VEHICLE_UNLOCK_URI = @"https://owner-api.teslamotors.com/api/1/vehicles/{0}/command/door_unlock"; //new
        public const string VEHICLE_LOCK_URI = @"https://owner-api.teslamotors.com/api/1/vehicles/{0}/command/door_lock"; //new
        public const string VEHICLE_STOP_CHARGING_URI = @"https://owner-api.teslamotors.com/api/1/vehicles/{0}/command/charge_stop";
        public const string VEHICLE_START_CHARGING_URI = @"https://owner-api.teslamotors.com/api/1/vehicles/{0}/command/charge_start";
        public const string VEHICLE_FLASH_LIGHTS_URI = @"https://owner-api.teslamotors.com/api/1/vehicles/{0}/command/flash_lights";
        public const string VEHICLE_HONKHORN_URI = @"https://owner-api.teslamotors.com/api/1/vehicles/{0}/command/honk_horn";
        public const string VEHICLE_CLIMATEON_URI = @"https://owner-api.teslamotors.com/api/1/vehicles/{0}/command/auto_conditioning_start"; //new
        public const string VEHICLE_CLIMATEOFF_URI = @"https://owner-api.teslamotors.com/api/1/vehicles/{0}/command/auto_conditioning_stop"; //new
        public const string VEHICLE_CHANGE_TEMPERATURE_URI = @"https://owner-api.teslamotors.com/api/1/vehicles/{0}/command/set_temps"; //new
        public const string VEHICLE_CHANGE_CHARGELIMIT_URI = @"https://owner-api.teslamotors.com/api/1/vehicles/{0}/command/set_charge_limit"; //new
        public const string VEHICLE_ACTUATE_TRUNK_URI = @"https://owner-api.teslamotors.com/api/1/vehicles/{0}/command/actuate_trunk"; //new
        public const string VEHICLE_STREAMING_URI = @"https://streaming.vn.teslamotors.com/stream/";


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

            var client = new RestClient(REVOKE_AUTH_URI);
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
            RestClient client = new RestClient(AUTHENTICATE_URI);
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

            RestClient client = new RestClient(VEHICLE_LIST_URI);
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

            string url = String.Format(VEHICLE_WAKEUP_URI, VehicleID);
            //List<VehicleDetails> v;
            VehicleDetails v;
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
                //v = JsonConvert.DeserializeObject<List<VehicleDetails>>(jsonResponse["response"].ToString());
                v = JsonConvert.DeserializeObject<VehicleDetails>(jsonResponse["response"].ToString());
            }
            catch (Newtonsoft.Json.JsonSerializationException serializationEx) {
                vehicleAwake = false;
                Debug.WriteLine("Wake up Vehicle, tried to serialize response to <VehicleDetails>\n" + serializationEx.Message);
                throw;
            }

            if ((v != null)) {
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

        /*
         * 
         * 
         {
            "response": {
                "id": 8449066975289321,
                "user_id": 424917,
                "vehicle_id": 1310913525,
                "vin": "5YJ3E1EAXJF032947",
                "display_name": "Squirty",
                "option_codes": "AD15,MDL3,PBSB,RENA,BT37,ID3W,RF3G,S3PB,DRLH,DV2W,W39B,APF0,COUS,BC3B,CH07,PC30,FC3P,FG31,GLFR,HL31,HM31,IL31,LTPB,MR31,FM3B,RS3H,SA3P,STCP,SC04,SU3C,T3CA,TW00,TM00,UT3P,WR00,AU3P,APH3,AF00,ZCST,MI00,CDM0",
                "color": null,
                "tokens": [
                    "d10c4c0e56796957",
                    "4d0c811771b27176"
                ],
                "state": "online",
                "in_service": null,
                "id_s": "8449066975289321",
                "calendar_enabled": true,
                "backseat_token": null,
                "backseat_token_updated_at": null,
                "charge_state": {
                    "charging_state": "Disconnected",
                    "fast_charger_type": "<invalid>",
                    "fast_charger_brand": "<invalid>",
                    "charge_limit_soc": 90,
                    "charge_limit_soc_std": 90,
                    "charge_limit_soc_min": 50,
                    "charge_limit_soc_max": 100,
                    "charge_to_max_range": false,
                    "max_range_charge_counter": 0,
                    "fast_charger_present": false,
                    "battery_range": 231.94,
                    "est_battery_range": 0,
                    "ideal_battery_range": 231.94,
                    "battery_level": 75,
                    "usable_battery_level": 75,
                    "charge_energy_added": 17.7,
                    "charge_miles_added_rated": 74.5,
                    "charge_miles_added_ideal": 74.5,
                    "charger_voltage": 2,
                    "charger_pilot_current": 48,
                    "charger_actual_current": 0,
                    "charger_power": 0,
                    "time_to_full_charge": 0,
                    "trip_charging": false,
                    "charge_rate": 0,
                    "charge_port_door_open": false,
                    "conn_charge_cable": "<invalid>",
                    "scheduled_charging_start_time": null,
                    "scheduled_charging_pending": false,
                    "user_charge_enable_request": null,
                    "charge_enable_request": true,
                    "charger_phases": null,
                    "charge_port_latch": "Engaged",
                    "charge_current_request": 48,
                    "charge_current_request_max": 48,
                    "managed_charging_active": false,
                    "managed_charging_user_canceled": false,
                    "managed_charging_start_time": null,
                    "battery_heater_on": false,
                    "not_enough_power_to_heat": null,
                    "timestamp": 1534202601957
                },
                "gui_settings": {
                    "gui_distance_units": "mi/hr",
                    "gui_temperature_units": "F",
                    "gui_charge_rate_units": "kW",
                    "gui_24_hour_time": false,
                    "gui_range_display": "Rated",
                    "timestamp": 1534202601955
                },
                "drive_state": {
                    "shift_state": null,
                    "speed": null,
                    "power": 0,
                    "latitude": 47.677685,
                    "longitude": -122.130949,
                    "heading": 91,
                    "gps_as_of": 1534202601,
                    "native_location_supported": 1,
                    "native_latitude": 47.677685,
                    "native_longitude": -122.130949,
                    "native_type": "wgs",
                    "timestamp": 1534202601968
                },
                "vehicle_config": {
                    "can_actuate_trunks": true,
                    "car_special_type": "base",
                    "car_type": "model3",
                    "charge_port_type": "US",
                    "eu_vehicle": false,
                    "exterior_color": "DeepBlue",
                    "has_ludicrous_mode": false,
                    "motorized_charge_port": true,
                    "perf_config": "Base",
                    "plg": null,
                    "rear_seat_heaters": 1,
                    "rear_seat_type": null,
                    "rhd": false,
                    "roof_color": "Glass",
                    "seat_type": null,
                    "spoiler_type": "None",
                    "sun_roof_installed": null,
                    "third_row_seats": "<invalid>",
                    "timestamp": 1534202601979,
                    "wheel_type": "Stiletto19"
                },
                "vehicle_state": {
                    "api_version": 3,
                    "autopark_state_v3": "ready",
                    "autopark_style": "standard",
                    "calendar_supported": true,
                    "car_version": "2018.26.3 be4b11e",
                    "center_display_state": 0,
                    "df": 0,
                    "dr": 0,
                    "ft": 0,
                    "homelink_nearby": false,
                    "last_autopark_error": "no_error",
                    "locked": true,
                    "notifications_supported": true,
                    "odometer": 1490.453945,
                    "parsed_calendar_supported": true,
                    "pf": 0,
                    "pr": 0,
                    "remote_start": false,
                    "remote_start_supported": true,
                    "rt": 0,
                    "speed_limit_mode": {
                        "active": false,
                        "current_limit_mph": 50,
                        "max_limit_mph": 90,
                        "min_limit_mph": 50,
                        "pin_code_set": false
                    },
                    "sun_roof_percent_open": null,
                    "sun_roof_state": "unknown",
                    "timestamp": 1534202601967,
                    "valet_mode": false,
                    "vehicle_name": "Squirty"
                },
                "climate_state": {
                    "inside_temp": 37.9,
                    "outside_temp": 30.5,
                    "driver_temp_setting": 20.6,
                    "passenger_temp_setting": 20.6,
                    "left_temp_direction": 0,
                    "right_temp_direction": 0,
                    "is_front_defroster_on": false,
                    "is_rear_defroster_on": false,
                    "fan_status": 0,
                    "is_climate_on": false,
                    "min_avail_temp": 15,
                    "max_avail_temp": 28,
                    "seat_heater_left": false,
                    "seat_heater_right": false,
                    "seat_heater_rear_left": false,
                    "seat_heater_rear_right": false,
                    "seat_heater_rear_center": false,
                    "seat_heater_rear_right_back": 0,
                    "seat_heater_rear_left_back": 0,
                    "battery_heater": false,
                    "battery_heater_no_power": null,
                    "steering_wheel_heater": false,
                    "wiper_blade_heater": false,
                    "side_mirror_heaters": false,
                    "is_preconditioning": false,
                    "smart_preconditioning": false,
                    "is_auto_conditioning_on": false,
                    "timestamp": 1534202602012
                }
            }
        }

         */
        public VehicleData GetAllVehicleData(string VehicleID, string AccessToken) {
            if (VehicleID is null)
                throw new ArgumentNullException("VehicleID");

            if (AccessToken is null)
                throw new ArgumentNullException("AccessToken");

            if (!vehicleAwake)
                vehicleAwake = WakeUpVechile(VehicleID, AccessToken);

            string url = String.Format(VEHICLE_DATA_URI, VehicleID);
            VehicleData vehicleData;

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
            vehicleData = JsonConvert.DeserializeObject<VehicleData>(jsonResponse["response"].ToString());

            Debug.WriteLine(response.ToString());
            Debug.WriteLine(vehicleData.ToString());

            return vehicleData;
        }
    }
}
