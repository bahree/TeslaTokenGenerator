using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeslaTokenGenerator {
    public static class Utils {
        public static DateTime ConvertUTCToLocalTime(string date) {
            DateTime convertedDate = DateTime.SpecifyKind(DateTime.Parse(date),
                DateTimeKind.Utc);

            DateTime local = convertedDate.ToLocalTime();
            return local;
        }

        public static double ConvertToCelcius(double f) {
            return (5.0 / 9.0) * (f - 32);
        }

        public static double ConvertToFahrenheit(double c) {
            return ((9.0 / 5.0) * c) + 32;
        }

        public static DateTime ConvertUnixTimeToLocalTime(double time) {
            DateTime tempDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);

            tempDateTime = tempDateTime.AddSeconds(time).ToLocalTime();
            return tempDateTime;
        }

        public static DateTime ConvertUnixTimeToLocalTime(string time) {
            return (ConvertUnixTimeToLocalTime(Convert.ToDouble(time)));
        }
    }
}
