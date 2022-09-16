using IniParser;
using IniParser.Model;

namespace FlightPlanner
{
    internal static class GlobalData
    {
        private static int avgMilesLanding;
        private static int avgMilesTakeoff;

        private static float avgSpeedLanding;
        private static float avgSpeedTakeoff;

        private static float timeOffset;

        private static float minimumFlight;

        public static int AvgMilesLanding => avgMilesLanding;
        public static int AvgMilesTakeoff => avgMilesTakeoff;
        public static float AvgSpeedLanding => avgSpeedLanding;
        public static float AvgSpeedTakeoff => avgSpeedTakeoff;
        public static float TimeOffset => timeOffset;
        public static float MinimumFlight => minimumFlight;

        public static void LoadFromConfig()
        {
            const string group = "Averages";

            FileIniDataParser iniParser = new();
            IniData data = iniParser.ReadFile(@"Config\Config.ini");

            //When parsing the data, if we cannot parse, just ignore and keep the data at its default value
            string landingSpeed = data[group]["avgLandingSpeed"];
            string takeoffSpeed = data[group]["avgTakeoffSpeed"];
            string landingMiles = data[group]["avgLanding"];
            string takeoffMiles = data[group]["avgTakeoff"];
            string timeoff = data[group]["timeOffset"];
            string minimum = data[group]["minimumFlight"];

            if (landingSpeed is not null)
            {
                _ = float.TryParse(landingSpeed, out avgSpeedLanding);
            }

            if (takeoffSpeed is not null)
            {
                _ = float.TryParse(takeoffSpeed, out avgSpeedTakeoff);
            }

            if (landingMiles is not null)
            {
                _ = int.TryParse(landingMiles, out avgMilesLanding);
            }

            if (takeoffMiles is not null)
            {
                _ = int.TryParse(takeoffMiles, out avgMilesTakeoff);
            }

            if (timeoff is not null)
            {
                _ = float.TryParse(timeoff, out timeOffset);
            }

            if (minimum is not null)
            {
                _ = float.TryParse(minimum, out minimumFlight);
            }
        }

        static GlobalData()
        {
            avgMilesLanding = 10;
            avgMilesTakeoff = 3;

            avgSpeedLanding = 170;
            avgSpeedTakeoff = 180;

            timeOffset = 10;
            minimumFlight = 150;
        }
    }
}