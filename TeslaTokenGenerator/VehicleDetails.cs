using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TeslaTokenGenerator {

    //Vehicle detail example:
    /*
    {
        "response": [
            {
                "id": xxxxxxxx,
                "vehicle_id": xxxxxxxxx,
                "vin": "XXXXXXXXXXXXXXX",
                "display_name": "XXXXXXX",
                "option_codes": "AD15,MDL3,PBSB,RENA,BT37,ID3W,RF3G,S3PB,DRLH,DV2W,W39B,APF0,COUS,BC3B,CH07,PC30,FC3P,FG31,GLFR,HL31,HM31,IL31,LTPB,MR31,FM3B,RS3H,SA3P,STCP,SC04,SU3C,T3CA,TW00,TM00,UT3P,WR00,AU3P,APH3,AF00,ZCST,MI00,CDM0",
                "color": null,
                "tokens": [
                    "XXXXXXXXXXXXXX",
                    "XXXXXXXXXX"
                ],
                "state": "online",
                "in_service": null,
                "id_s": "XXXXXXXXXXXXXXXXX",
                "calendar_enabled": true,
                "backseat_token": null,
                "backseat_token_updated_at": null
            }
        ],
        "count": 1
    }, 
    */
    
    class VehicleDetails {
        [JsonProperty(PropertyName = "id")]
        public string ID { get; set; }

        [JsonProperty(PropertyName = "vehicle_id")]
        public string VehicleID { get; set; }

        [JsonProperty(PropertyName = "vin")]
        public string VIN { get; set; }

        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName { get; set; }

        [JsonProperty(PropertyName = "option_codes")]
        public string OptionCodes { get; set; }

        [JsonProperty(PropertyName = "color", NullValueHandling = NullValueHandling.Ignore)]
        public string Colour { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "in_service", NullValueHandling = NullValueHandling.Ignore)]
        public string IsServiced { get; set; }

        [JsonProperty(PropertyName = "calendar_enabled")]
        public bool CalendarEnabled { get; set; }

        [JsonProperty(PropertyName = "remote_start_enabled")]
        public bool RemoteStartEnabled { get; set; }

        [JsonProperty(PropertyName = "notifications_enabled")]
        public bool NotificationsEnabled { get; set; }

        [JsonProperty(PropertyName = "tokens")]
        public string [] Tokens { get; set; }

        public override string ToString() {
            StringBuilder s = new StringBuilder();

            s.AppendLine("ID: " + ID);
            s.AppendLine("VIN: " + VIN);
            s.AppendLine("Name: " + DisplayName);
            s.AppendLine("Options Installed: " + OptionCodes);
            s.AppendLine("Vehicle State: " + State);
            s.AppendLine("Caelndar Enabled: " + CalendarEnabled);
            s.AppendLine("Remote Start Enabled: " + RemoteStartEnabled);
            s.AppendLine("Notifications Enabled: " + NotificationsEnabled);

            return s.ToString();
        }
    }
    
    /*
    Vehicle State: 
    https://owner-api.teslamotors.com/api/1/vehicles/{8449066975289321}/data_request/vehicle_state
        {
          "response": {
            "df": false,                  // driver's side front door open
            "dr": false,                  // driver's side rear door open
            "pf": false,                  // passenger's side front door open
            "pr": false,                  // passenger's side rear door open
            "ft": false,                  // front trunk is open
            "rt": false,                  // rear trunk is open
            "car_verson": "1.19.42",      // car firmware version
            "locked": true,               // car is locked
            "sun_roof_installed": false,  // panoramic roof is installed
            "sun_roof_state": "unknown",
            "sun_roof_percent_open": 0,   // null if not installed
            "dark_rims": false,           // gray rims installed
            "wheel_type": "Base19",       // wheel type installed
            "has_spoiler": false,         // spoiler is installed
            "roof_color": "Colored",      // "None" for panoramic roof
            "perf_config": "Base"
          }
        }


        OLD Version:
        {
    "response": {
        "api_version": 3,
        "autopark_state": "unavailable",
        "autopark_state_v2": "unavailable",
        "calendar_supported": true,
        "car_version": "2018.21.9 75bdbc11",
        "center_display_state": 0,
        "df": 0,
        "dr": 0,
        "ft": 0,
        "locked": false,
        "notifications_supported": true,
        "odometer": 184.403716,
        "parsed_calendar_supported": true,
        "pf": 0,
        "pr": 0,
        "remote_start": false,
        "remote_start_supported": true,
        "rt": 0,
        "sun_roof_percent_open": null,
        "sun_roof_state": "unknown",
        "timestamp": 1531891296249,
        "valet_mode": false,
        "valet_pin_needed": true,
        "vehicle_name": "Squirty"
    }


        New Version:
        {
    "response": {
        "api_version": 3,
        "autopark_state_v3": "standby",
        "autopark_style": "dead_man",
        "calendar_supported": true,
        "car_version": "2018.24.8 25f83c2",
        "center_display_state": 0,
        "df": 0,
        "dr": 0,
        "ft": 0,
        "homelink_nearby": true,
        "last_autopark_error": "no_error",
        "locked": true,
        "notifications_supported": true,
        "odometer": 230.888497,
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
        "timestamp": 1532199276692,
        "valet_mode": false,
        "valet_pin_needed": true,
        "vehicle_name": "XXXXXXXX"
    }
}
}
        */
    class VehicleState {
        [JsonProperty(PropertyName = "api_version")]
        public int APIVersion { get; set; }

        [JsonProperty(PropertyName = "autopark_state")]
        public string AutoParkState { get; set; }

        [JsonProperty(PropertyName = "autopark_state_v2")]
        public string AutoParkStateV2 { get; set; }

        [JsonProperty(PropertyName = "autopark_state_v3")]
        public string AutoParkStateV3 { get; set; }

        [JsonProperty(PropertyName = "calendar_supported")]
        public bool CalendarSupported { get; set; }

        [JsonProperty(PropertyName = "car_type")]
        public string CarType { get; set; }

        [JsonProperty(PropertyName = "car_version")]
        public string CarVersion { get; set; } //Car's firmware version

        [JsonProperty(PropertyName = "center_display_state")]
        public int CenterDisplayState { get; set; }

        [JsonProperty(PropertyName = "dark_rims")]
        public bool DarkRims { get; set; }

        [JsonProperty(PropertyName = "df")]
        public int Df { get; set; } // driver's side front door open

        [JsonProperty(PropertyName = "dr")]
        public int Dr { get; set; } // driver's side rear door open

        [JsonProperty(PropertyName = "pf")]
        public int Pf { get; set; } // passengers's side front door open

        [JsonProperty(PropertyName = "pr")]
        public int Pr { get; set; } // passengers's side rear door open

        [JsonProperty(PropertyName = "ft")]
        public int Ft { get; set; } // front trunk is open

        [JsonProperty(PropertyName = "rt")]
        public int Rt { get; set; } // rear trunk is open

        [JsonProperty(PropertyName = "exterior_color")]
        public bool ExteriorColor { get; set; }

        [JsonProperty(PropertyName = "has_spoiler")]
        public bool HasSpoiler { get; set; }

        [JsonProperty(PropertyName = "locked")]
        public bool Locked { get; set; }

        [JsonProperty(PropertyName = "notifications_supported")]
        public bool NotificationsSupported { get; set; }

        [JsonProperty(PropertyName = "odometer")]
        public float Odometer { get; set; }

        [JsonProperty(PropertyName = "parsed_calendar_supported")]
        public bool ParsedCalendarSupported { get; set; }

        [JsonProperty(PropertyName = "perf_config")]
        public bool PerfConfig { get; set; }

        [JsonProperty(PropertyName = "rear_seat_heaters")]
        public int RearSeatHeaters { get; set; }

        [JsonProperty(PropertyName = "remote_start")]
        public bool RemoteStart { get; set; }

        [JsonProperty(PropertyName = "remote_start_supported")]
        public bool RemoteStartSupported { get; set; }

        [JsonProperty(PropertyName = "rhd")]
        public bool Rhd { get; set; }

        [JsonProperty(PropertyName = "roof_color")]
        public string RoofColor { get; set; }

        [JsonProperty(PropertyName = "seat_type")]
        public int SeatType { get; set; }

        [JsonProperty(PropertyName = "spoiler_type", NullValueHandling = NullValueHandling.Ignore)]
        public string SpoilerType { get; set; }

        [JsonProperty(PropertyName = "sun_roof_installed", NullValueHandling = NullValueHandling.Ignore)]
        public int SunRoofInstalled { get; set; }

        [JsonProperty(PropertyName = "sun_roof_percent_open", NullValueHandling = NullValueHandling.Ignore)]
        public int SunRoofPercentOpen { get; set; }
        
        [JsonProperty(PropertyName = "sun_roof_state")]
        public string SunRoofState { get; set; }

        [JsonProperty(PropertyName = "third_row_seats")]
        public string ThirdRowSeats { get; set; }

        [JsonProperty(PropertyName = "valet_mode")]
        public bool ValetMode { get; set; }

        [JsonProperty(PropertyName = "valet_pin_needed")]
        public bool ValetPinNeeded { get; set; }

        [JsonProperty(PropertyName = "speed_limit_mode")]
        public Speed_LimitMode SpeedLimitMode { get; set; }

        [JsonProperty(PropertyName = "last_autopark_error")]
        public string LastAutoparkError { get; set; }

        [JsonProperty(PropertyName = "vehicle_name")]
        public string VehicleName { get; set; }

        [JsonProperty(PropertyName = "wheel_type")]
        public string WheelType { get; set; }

        [JsonProperty(PropertyName = "timestamp")]
        public double TimeStamp { get; set; }

        public override string ToString() {
            StringBuilder s = new StringBuilder();

            s.AppendLine("Name: " + VehicleName);
            s.AppendLine("Wheel type: " + WheelType);
            s.AppendLine("Locked: " + Locked);
            s.AppendLine("Odometer: " + Odometer);
            s.AppendLine("Firmware Version: " + CarVersion);

            s.AppendLine("API Version: " + APIVersion);
            s.AppendLine("AutoPark State: " + AutoParkState);
            s.AppendLine("AutoPark (v2) State: " + AutoParkStateV2);
            s.AppendLine("AutoPark (v3) State: " + AutoParkStateV3);
            s.AppendLine("Center Display State: " + CenterDisplayState);
            s.AppendLine("Dark Rims: " + DarkRims);

            s.AppendLine("Driver's side front door open: " + Df);
            s.AppendLine("Driver's side rear door open: " + Dr);
            s.AppendLine("Passengers's side front door open: " + Pf);
            s.AppendLine("Passengers's side front door open: " + Pr);

            s.AppendLine("Frunk is open: " + Ft);
            s.AppendLine("Trunk is open: " + Rt);

            s.AppendLine("Exterior Color: " + ExteriorColor);
            s.AppendLine("HasSpoiler: " + HasSpoiler);

            s.AppendLine("Notifications Supported: " + NotificationsSupported);
            s.AppendLine("Performance Configuration: " + PerfConfig);
            s.AppendLine("Rear Seat Heaters: " + RearSeatHeaters);
            s.AppendLine("Remote Start: " + RemoteStart);
            s.AppendLine("Remote Start Supported: " + RemoteStartSupported);
            s.AppendLine("Right Hand Drive: " + Rhd);
            
            s.AppendLine("Roof Color: " + RoofColor);
            s.AppendLine("Seat Type: " + SeatType);
            s.AppendLine("Spoiler Type: " + SpoilerType);
            s.AppendLine("SunRoof Installed: " + SunRoofInstalled);
            s.AppendLine("SunRoof Open %: " + SunRoofPercentOpen);
            s.AppendLine("SunRoof State: " + SunRoofState);

            s.AppendLine("ThirdRow Seats: " + ThirdRowSeats);
            s.AppendLine("Valet Mode: " + ValetMode);
            s.AppendLine("ValetPin Needed: " + ValetPinNeeded);
            s.AppendLine("Speed Limit Mode: " + SpeedLimitMode.ToString());
            s.AppendLine("Last Autopark Error: " + LastAutoparkError);

            return s.ToString();
        }
    }

    /*
    "active": false,
            "current_limit_mph": 50,
            "max_limit_mph": 90,
            "min_limit_mph": 50,
            "pin_code_set": false
            */
    class Speed_LimitMode {
        [JsonProperty(PropertyName = "current_limit_mph")]
        public float CurrentLimitMPH { get; set; }

        [JsonProperty(PropertyName = "max_limit_mph")]
        public float MaxLimitMPH { get; set; }

        [JsonProperty(PropertyName = "min_limit_mph")]
        public float MinLimitMPH { get; set; }

        [JsonProperty(PropertyName = "pin_code_set")]
        public bool PinCodeSet { get; set; }

        public override string ToString() {
            StringBuilder s = new StringBuilder();

            s.AppendLine("Current Limit (MPH): " + CurrentLimitMPH);
            s.AppendLine("Max Limit (MPH): " + MaxLimitMPH);
            s.AppendLine("Min Limit (MPH): " + MinLimitMPH);
            s.AppendLine("Pin Code Set: " + PinCodeSet);

            return s.ToString();
        }
    }

    //Charge state 
    //https://owner-api.teslamotors.com/api/1/vehicles/8449066975289321/data_request/charge_state
    /*
     * {
    "response": {
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
        "battery_range": 227.85,
        "est_battery_range": 0,
        "ideal_battery_range": 227.85,
        "battery_level": 73,
        "usable_battery_level": 73,
        "charge_energy_added": 1.4,
        "charge_miles_added_rated": 6,
        "charge_miles_added_ideal": 6,
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
        "timestamp": 1531889347118
    }
}
*/
    class VehicleChargeState {
        [JsonProperty(PropertyName = "charging_state")]
        public string ChargingState { get; set; }

        [JsonProperty(PropertyName = "fast_charger_type", NullValueHandling = NullValueHandling.Ignore)]
        public string FastChargerType { get; set; }

        [JsonProperty(PropertyName = "fast_charger_brand", NullValueHandling = NullValueHandling.Ignore)]
        public string FastChargerBrand { get; set; }

        [JsonProperty(PropertyName = "charge_limit_soc")]
        public int ChargeLimitSOC { get; set; } //SOC == State of Charge

        [JsonProperty(PropertyName = "charge_limit_soc_std")]
        public int ChargeLimitSOCStd { get; set; } 

        [JsonProperty(PropertyName = "charge_limit_soc_min")]
        public int ChargeLimitSOCMin { get; set; }

        [JsonProperty(PropertyName = "charge_limit_soc_max")]
        public int ChargeLimitSOCMax { get; set; }

        [JsonProperty(PropertyName = "charge_to_max_range")]
        public bool ChargeToMaxRange { get; set; } 

        [JsonProperty(PropertyName = "max_range_charge_counter")]
        public int MaxRangeChargeCounter { get; set; } 

        [JsonProperty(PropertyName = "fast_charger_present")]
        public int FastChargerPresent { get; set; } 

        [JsonProperty(PropertyName = "battery_range")]
        public float BatteryRange { get; set; } 

        [JsonProperty(PropertyName = "est_battery_range")]
        public float EstBatteryRange { get; set; } 

        [JsonProperty(PropertyName = "ideal_battery_range")]
        public float IdealBatteryRange { get; set; } 

        [JsonProperty(PropertyName = "battery_level")]
        public int BatteryLevel { get; set; }

        [JsonProperty(PropertyName = "usable_battery_level")]
        public int UsableBatteryLevel { get; set; }

        [JsonProperty(PropertyName = "charge_energy_added")]
        public float ChargeEnergyAdded { get; set; }

        [JsonProperty(PropertyName = "charge_miles_added_rated")]
        public int ChargeMilesAddedRated { get; set; }

        [JsonProperty(PropertyName = "charge_miles_added_ideal")]
        public int ChargeMilesAddedIdeal { get; set; }

        [JsonProperty(PropertyName = "charger_voltage")]
        public int ChargerVoltage { get; set; }

        [JsonProperty(PropertyName = "charger_pilot_current")]
        public int ChargerPilotCurrent { get; set; }

        [JsonProperty(PropertyName = "charger_actual_current")]
        public int ChargerActualCurrent { get; set; }

        [JsonProperty(PropertyName = "charger_power")]
        public int ChargerPower { get; set; }

        //TODO: check the type; is this DateTime or Double (for UnixTime) - not sure.
        //Set it to string for now, so the parsing wont fail.
        [JsonProperty(PropertyName = "time_to_full_charge")]
        public string TimeToFullCharge { get; set; }

        [JsonProperty(PropertyName = "trip_charging")]
        public bool TripCharging { get; set; }

        [JsonProperty(PropertyName = "charge_rate")]
        public int ChargeRate { get; set; }

        [JsonProperty(PropertyName = "charge_port_door_open")]
        public bool ChargePortDoorOpen { get; set; }

        [JsonProperty(PropertyName = "conn_charge_cable", NullValueHandling = NullValueHandling.Ignore)]
        public string ConnChargeCable { get; set; }

        //TODO: check the type; is this DateTime or Int - not sure.
        //Set it to string for now, so the parsing wont fail.
        [JsonProperty(PropertyName = "scheduled_charging_start_time", NullValueHandling = NullValueHandling.Ignore)]
        public string ScheduledChargingStartTime { get; set; }

        [JsonProperty(PropertyName = "scheduled_charging_pending")]
        public bool ScheduledChargingPending { get; set; }

        [JsonProperty(PropertyName = "user_charge_enable_request", NullValueHandling = NullValueHandling.Ignore)]
        public bool UserChargeEnableRequest { get; set; }

        [JsonProperty(PropertyName = "charge_enable_request")]
        public bool ChargeEnableRequest { get; set; }

        //TODO: Need to validate if this indeed int.
        [JsonProperty(PropertyName = "charger_phases")]
        public int ChargerPhases { get; set; } 

        [JsonProperty(PropertyName = "charge_port_latch")]
        public bool ChargePortLatch { get; set; }

        [JsonProperty(PropertyName = "charge_current_request")]
        public int ChargeCurrentRequest { get; set; }

        [JsonProperty(PropertyName = "charge_current_request_max")]
        public int ChargeCurrentRequestMax { get; set; }

        [JsonProperty(PropertyName = "managed_charging_active")]
        public bool ManagedChargingActive { get; set; }

        [JsonProperty(PropertyName = "managed_charging_user_canceled")]
        public bool ManagedChargingUserCanceled { get; set; }

        //TODO: check the type; is this DateTime or Int - not sure.
        //Set it to string for now, so the parsing wont fail.
        [JsonProperty(PropertyName = "managed_charging_start_time", NullValueHandling = NullValueHandling.Ignore)]
        public string ManagedChargingStartTime { get; set; }

        [JsonProperty(PropertyName = "battery_heater_on")]
        public bool BatteryHeaterOn { get; set; }

        [JsonProperty(PropertyName = "not_enough_power_to_heat", NullValueHandling = NullValueHandling.Ignore)]
        public bool NotEnoughPowerToHeat { get; set; }

        [JsonProperty(PropertyName = "timestamp")]
        public double TimeStamp { get; set; }

        public override string ToString() {
            StringBuilder s = new StringBuilder();

            s.AppendLine("Charging State: " + ChargingState);
            s.AppendLine("Battery Range: " + BatteryRange);
            s.AppendLine("(Estimated) Battery Range: " + EstBatteryRange);
            s.AppendLine("(Ideal) Battery Range: " + IdealBatteryRange);
            s.AppendLine("Battery Level: " + BatteryLevel);
            s.AppendLine("Usable Battery Level: " + UsableBatteryLevel);
            s.AppendLine("Time To Full Charge: " + TimeToFullCharge);
            s.AppendLine("Trip Charging: " + TripCharging);
            s.AppendLine("Charge Rate: " + ChargeRate);

            s.AppendLine("Charge Port Open: " + ChargePortDoorOpen);
            s.AppendLine("Charge Cable Connection: " + ConnChargeCable);
            s.AppendLine("Scheduled charging Start Time: " + ScheduledChargingStartTime);
            s.AppendLine("Scheduled Charging Pending: " + ScheduledChargingPending);

            s.AppendLine("Charge Energy Added: " + ChargeEnergyAdded);
            s.AppendLine("Charge Miles Added (Rated): " + ChargeMilesAddedRated);
            s.AppendLine("Charge Miles Added (Ideal): " + ChargeMilesAddedIdeal);
            s.AppendLine("Charger Voltage: " + ChargerVoltage);
            s.AppendLine("Charger Pilot Current: " + ChargerPilotCurrent);
            s.AppendLine("Charger Actual (Current): " + ChargerActualCurrent);
            s.AppendLine("Charger Power: " + ChargerPower);

            s.AppendLine("Charge Limit: " + ChargeLimitSOC);
            s.AppendLine("Charge Limit (Regular): " + ChargeLimitSOCStd);
            s.AppendLine("Charge Limit (Min): " + ChargeLimitSOCMin);
            s.AppendLine("Charge Limit (Max): " + ChargeLimitSOCMax);
            s.AppendLine("Charge To Max Range: " + ChargeToMaxRange);
            s.AppendLine("Max Range Charge Count: " + MaxRangeChargeCounter);

            s.AppendLine("Fast Charger Present: " + FastChargerPresent);
            s.AppendLine("Fast Charger Type: " + FastChargerType);
            s.AppendLine("Fast Charger Brand: " + FastChargerBrand);

            s.AppendLine("Charger Phases: " + ChargerPhases);
            s.AppendLine("Charge Port Latch: " + ChargePortLatch);
            s.AppendLine("Charge Current Request: " + ChargeCurrentRequest);
            s.AppendLine("Battery Heater On: " + BatteryHeaterOn);
            s.AppendLine("Not Enough Power To Heat: " + NotEnoughPowerToHeat);

            return s.ToString();
        }
    }


    //Climate Setting
    //https://owner-api.teslamotors.com/api/1/vehicles/8449066975289321/data_request/climate_state
    /*
     * 
        {
        "response": {
            "inside_temp": 31.2,
            "outside_temp": 26.5,
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
            "timestamp": 1531890058544
        }
    }

    */

    class VehicleClimateState {
        [JsonProperty(PropertyName = "inside_temp")]
        public float InsideTemp { get; set; }

        [JsonProperty(PropertyName = "outside_temp")]
        public float OutsideTemp { get; set; }

        [JsonProperty(PropertyName = "driver_temp_setting")]
        public float DriverTempSetting { get; set; }

        [JsonProperty(PropertyName = "passenger_temp_setting")]
        public float PassengerTempSetting { get; set; }

        [JsonProperty(PropertyName = "left_temp_direction")]
        public float LeftTempSetting { get; set; }

        [JsonProperty(PropertyName = "right_temp_direction")]
        public float RightTempSetting { get; set; }
        
        [JsonProperty(PropertyName = "is_front_defroster_on")]
        public bool IsFrontDefrosterOn { get; set; }

        [JsonProperty(PropertyName = "is_rear_defroster_on")]
        public bool IsRearDefrosterOn { get; set; }

        [JsonProperty(PropertyName = "fan_status")]
        public int FanStatus { get; set; }

        [JsonProperty(PropertyName = "is_climate_on")]
        public bool IsClimateOn { get; set; }

        [JsonProperty(PropertyName = "min_avail_temp")]
        public float MinAvailTemp { get; set; }

        [JsonProperty(PropertyName = "max_avail_temp")]
        public float MaxAvailTemp { get; set; }

        [JsonProperty(PropertyName = "seat_heater_left")]
        public bool SeatHeaterLeft { get; set; }

        [JsonProperty(PropertyName = "seat_heater_right")]
        public bool SeatHeaterRight { get; set; }

        [JsonProperty(PropertyName = "seat_heater_rear_left")]
        public bool SeatHeaterRearLeft { get; set; }

        [JsonProperty(PropertyName = "seat_heater_rear_right")]
        public bool SeatHeaterRearRight { get; set; }

        [JsonProperty(PropertyName = "seat_heater_rear_center")]
        public bool SeatHeaterRearCenter { get; set; }

        [JsonProperty(PropertyName = "seat_heater_rear_right_back")]
        public bool SeatHeaterRearRightBack { get; set; }

        [JsonProperty(PropertyName = "seat_heater_rear_left_back")]
        public bool SeatHeaterRearLeftBack { get; set; }

        [JsonProperty(PropertyName = "battery_heater")]
        public bool BatteryHeater { get; set; }

        //Not sure of the data type; keeping it to string to avoid parsing issues.
        [JsonProperty(PropertyName = "battery_heater_no_power", NullValueHandling = NullValueHandling.Ignore)] 
        public string BatteryHeaterNoPower { get; set; }

        [JsonProperty(PropertyName = "steering_wheel_heater")]
        public bool SteeringWheelHeater { get; set; }

        [JsonProperty(PropertyName = "wiper_blade_heater")]
        public bool WiperBladeHeater { get; set; }

        [JsonProperty(PropertyName = "is_preconditioning")]
        public bool IsPreconditioning { get; set; }

        [JsonProperty(PropertyName = "side_mirror_heaters")]
        public bool SideMirrorHeaters { get; set; }

        [JsonProperty(PropertyName = "smart_preconditioning")]
        public bool SmartPreconditioning { get; set; }

        [JsonProperty(PropertyName = "is_auto_conditioning_on")]
        public bool IsAutoConditioningOn { get; set; }

        [JsonProperty(PropertyName = "timestamp")]
        public double TimeStamp { get; set; }

        public override string ToString() {
            StringBuilder s = new StringBuilder();

            s.AppendLine("Fan Status: " + FanStatus);
            s.AppendLine("Is Climate On: " + IsClimateOn);
            s.AppendLine("Inside Temperature: " + InsideTemp);
            s.AppendLine("Outside Temperature: " + OutsideTemp);

            s.AppendLine("Driver Temperature Setting: " + DriverTempSetting);
            s.AppendLine("Passenger Temperature Setting: " + PassengerTempSetting);

            s.AppendLine("Left Temperature Setting: " + LeftTempSetting);
            s.AppendLine("Right Temperature Setting: " + RightTempSetting);

            s.AppendLine("Is Front Defroster On: " + IsFrontDefrosterOn);
            s.AppendLine("Is Rear Defroster On: " + IsRearDefrosterOn);
            s.AppendLine("Min Available Temperature: " + MinAvailTemp);
            s.AppendLine("Max Available Temperature: " + MaxAvailTemp);

            s.AppendLine("Seat Heater (Left): " + SeatHeaterLeft);
            s.AppendLine("Seat Heater (Right): " + SeatHeaterRight);
            s.AppendLine("Seat Heater (Rear Left): " + SeatHeaterRearLeft);
            s.AppendLine("Seat Heater (Rear Right): " + SeatHeaterRearRight);
            s.AppendLine("Seat Heater (Rear Center): " + SeatHeaterRearCenter);
            s.AppendLine("Seat Heater (Rear Right Back): " + SeatHeaterRearRightBack);
            s.AppendLine("Seat Heater (Rear Left Back): " + SeatHeaterRearLeftBack);

            s.AppendLine("Battery Heater: " + BatteryHeater);
            s.AppendLine("Battery Heater (No Power): " + BatteryHeaterNoPower);
            s.AppendLine("Steering Wheel Heater: " + SteeringWheelHeater);
            s.AppendLine("Wiper Blade Heater: " + WiperBladeHeater);
            s.AppendLine("Is Preconditioning: " + IsPreconditioning);
            s.AppendLine("Side Mirror Heaters: " + SideMirrorHeaters);
            s.AppendLine("Smart Preconditioning: " + SmartPreconditioning);
            s.AppendLine("Is Auto Conditioning On: " + IsAutoConditioningOn);

            return s.ToString();
        }
    }

    //Driving and position
    //https://owner-api.teslamotors.com/api/1/vehicles/8449066975289321/data_request/drive_state
    /*

    {
        "response": {
            "shift_state": null,
            "speed": null,
            "power": 0,
            "latitude": XXXXXXXXX,
            "longitude": XXXXXXX,
            "heading": 171,
            "gps_as_of": 1531890225,
            "native_location_supported": 1,
            "native_latitude": XXXXXXXXXXX,
            "native_longitude": XXXXXXXXXX,
            "native_type": "wgs",
            "timestamp": 1531890226433
        }
    }
    */

    class VehicleDriveState {
        //TODO: Not sure of the data type; will need to check
        [JsonProperty(PropertyName = "shift_state", NullValueHandling = NullValueHandling.Ignore)]
        public string ShiftState { get; set; }

        [JsonProperty(PropertyName = "speed", NullValueHandling = NullValueHandling.Ignore)]
        public float Speed { get; set; }

        [JsonProperty(PropertyName = "power")]
        public int Power { get; set; }

        //Can use a string, but a Decimal seems more appropriate; it is better than float as it has more precisions, which is what is needed.
        [JsonProperty(PropertyName = "latitude")]
        public decimal Latitude { get; set; }

        [JsonProperty(PropertyName = "longitude")]
        public decimal Longitude { get; set; }

        [JsonProperty(PropertyName = "heading")]
        public int Heading { get; set; }

        [JsonProperty(PropertyName = "gps_as_of")]
        public double GPSAsOf { get; set; }

        [JsonProperty(PropertyName = "native_location_supported")]
        public int NativeLocationSupported { get; set; }

        [JsonProperty(PropertyName = "native_latitude")]
        public decimal NativeLatitude { get; set; }

        [JsonProperty(PropertyName = "native_longitude")]
        public decimal NativeLongitude { get; set; }

        //This returns as "wgs", which I presume is Word Geodetic System.
        //more here: https://en.wikipedia.org/wiki/World_Geodetic_System
        [JsonProperty(PropertyName = "native_type")]
        public string NativeType { get; set; }

        [JsonProperty(PropertyName = "timestamp")]
        public double TimeStamp { get; set; }

        public override string ToString() {
            StringBuilder s = new StringBuilder();

            s.AppendLine("Shift State: " + ShiftState);
            s.AppendLine("Speed: " + Speed);
            s.AppendLine("Power: " + Power);
            s.AppendLine("Latitude: " + Latitude);
            s.AppendLine("Longitude: " + Longitude);
            s.AppendLine("Heading: " + Heading);
            s.AppendLine("GPS As Of: " + Utils.ConvertUnixTimeToLocalTime(GPSAsOf));
            s.AppendLine("Native Location Supported: " + NativeLocationSupported);
            s.AppendLine("Native Latitude: " + NativeLatitude);
            s.AppendLine("Native Longitude: " + NativeLongitude);
            s.AppendLine("Native Type: " + NativeType);

            return s.ToString();
        }
    }

    /*
     * 
     {
        "response": {
         "result": true,
         "reason": ""
        }
    */
    class VehicleWakeUp {
        [JsonProperty(PropertyName = "result")]
        public bool Result { get; set; }

        [JsonProperty(PropertyName = "reason", NullValueHandling = NullValueHandling.Ignore)]
        public string Reason { get; set; }
    }

    //GUI Settings
    /*
    {
    "response": {
        "gui_distance_units": "mi/hr",
        "gui_temperature_units": "F",
        "gui_charge_rate_units": "kW",
        "gui_24_hour_time": false,
        "gui_range_display": "Rated",
        "timestamp": 1533665508965
    }
    */
    class VehicleGUISettings {
        [JsonProperty(PropertyName = "gui_distance_units")]
        public string GUIDistanceUnits { get; set; }

        [JsonProperty(PropertyName = "gui_temperature_units")]
        public string GUITemperatureUnits { get; set; }

        [JsonProperty(PropertyName = "gui_charge_rate_units")]
        public string GUIChargeRateUnits { get; set; }

        [JsonProperty(PropertyName = "gui_24_hour_time")]
        public bool GUI24HourTime { get; set; }

        [JsonProperty(PropertyName = "gui_range_display")]
        public string GUIRangeDisplay { get; set; }

        [JsonProperty(PropertyName = "timestamp")]
        public double TimeStamp { get; set; }

        public override string ToString() {
            StringBuilder s = new StringBuilder();

            s.AppendLine("Distance Units: " + GUIDistanceUnits);
            s.AppendLine("Temperature Units: " + GUITemperatureUnits);
            s.AppendLine("Charge Rate Units: " + GUIChargeRateUnits);
            s.AppendLine("24 Hour Display: " + GUI24HourTime);
            s.AppendLine("Range Display: " + GUIRangeDisplay);

            return s.ToString();
        }
    }
}
