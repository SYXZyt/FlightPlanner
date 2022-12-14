using FlightPlanner.Data;
using GeoCoordinatePortable;

namespace FlightPlanner.Paths
{
    /// <summary>
    /// Stores data about a path between two <see cref="FlightPlanner.Paths.Path"/>s
    /// </summary>
    internal class Path
    {
        private readonly Airport start;
        private readonly Airport end;

        private readonly GeoCoordinate startPos;
        private readonly GeoCoordinate endPos;

        /// <summary>
        /// Calculate distance between both <see cref="FlightPlanner.Data.Airport"/>s
        /// </summary>
        /// <returns>Distance in nautical miles</returns>
        public double Distance()
        {
            //We have to divide the result by 1852, to convert it from M to nm
            return startPos.GetDistanceTo(endPos) / 1852.0d;
        }

        /// <summary>
        /// Calculate an estimated minimum time for the flight
        /// </summary>
        /// <param name="aircraft">The aircraft used for the flight</param>
        /// <returns>Time in minutes</returns>
        public double EstimatedTime(Model aircraft)
        {
            double dist = Distance();

            //If dist is too short, just assume we are traveling fast the entire time
            if (dist <= GlobalData.AvgMilesLanding + GlobalData.AvgMilesTakeoff)
            {
                return dist / (aircraft.CruiseSpeed * 0.7d) / 60.0d;
            }

            double totalTime = 0.0d;
            totalTime += (dist - (GlobalData.AvgMilesLanding + GlobalData.AvgMilesTakeoff)) / (aircraft.CruiseSpeed / 60.0d);
            totalTime += GlobalData.AvgMilesLanding / (GlobalData.AvgSpeedLanding / 60.0d);
            totalTime += GlobalData.AvgMilesTakeoff / (GlobalData.AvgSpeedTakeoff / 60.0d);
            return totalTime;
        }

        public override string ToString() => $"{start} -> {end} ({Math.Round(Distance(), 2)}nm)";

        /// <summary>
        /// Creates a new <see cref="FlightPlanner.Paths.Path"/>
        /// </summary>
        /// <param name="start">The departure airport</param>
        /// <param name="end">The arrival airport</param>
        public Path(Airport start, Airport end)
        {
            this.start = start;
            this.end = end;

            startPos = new(start.Latitude, start.Longitude);
            endPos = new(end.Latitude, end.Longitude);
        }
    }
}