using FlightPlanner.Data;

namespace FlightPlanner.Paths
{
    /// <summary>
    /// Has methods which can generate a <see cref="FlightPlanner.Paths.Path"/> between two <see cref="FlightPlanner.Data.Airport"/>s
    /// </summary>
    internal class PathGenerator
    {
        private readonly Group<Aircraft> aircraft;
        private readonly Group<Airport> airports;
        private readonly Random rng;
        private Aircraft selectedAircraft;

        /// <summary>
        /// Get the <see cref="FlightPlanner.Data.Aircraft"/> to be used in the flight
        /// </summary>
        public Aircraft SelectedAircraft => selectedAircraft;

        /// <summary>
        /// Pick a random <see cref="FlightPlanner.Data.Aircraft"/> to use
        /// </summary>
        private void GetAircraft()
        {
            selectedAircraft = rng.Choice(aircraft);
        }

        /// <summary>
        /// Run through every saved <see cref="FlightPlanner.Data.Airport"/>, to make sure that at least one <see cref="FlightPlanner.Data.Airport"/> is in range
        /// </summary>
        /// <param name="departure">The <see cref="FlightPlanner.Data.Airport"/> to check is in range of all other <see cref="FlightPlanner.Data.Airport"/>s</param>
        /// <returns>True if in range</returns>
        private bool IsAirportInDistance(Airport departure)
        {
            //Check every airport to make sure that a valid path can be formed
            foreach (Airport a in airports)
            {
                if (a == departure) continue;

                if (new Path(departure, a).Distance() <= selectedAircraft.Range) return true; //As long as one airport is in range, we can mark it as valid
            }

            return false;
        }

        /// <summary>
        /// Generate a random flight-plan
        /// </summary>
        /// <returns>The generated plan</returns>
        public Path GeneratePath()
        {
            Path selectedPath;

            //Loop up until we find a valid path
            while (true)
            {
                Console.Clear();

                //This uses a bogo-sort type algorithm, so it could run til the end of time, or it could get a valid path first time
                //Should probably use a different algorithm, but that's for another day
                GetAircraft();

                Airport startAirport = rng.Choice(airports);
                Airport endAirport;

                //If no valid path can be made, don't bother trying to generate one. Just start from scratch
                if (!IsAirportInDistance(startAirport)) continue;

                do
                {
                    endAirport = rng.Choice(airports);
                } while (endAirport == startAirport);

                //Now we have two airports, we need to check if the selected plane is actually capable of the flight
                selectedPath = new(startAirport, endAirport);

                if (selectedPath.Distance() > selectedAircraft.Range)
                {
                    //If the plane cannot make this path, reselect
                    continue;
                }


                if (selectedPath.GetShortestRunway() < selectedAircraft.RequiredRunwayLength)
                {
                    //If the shortest runway is too short, we can use one of these airports
                    continue;
                }

                break;
            }

            return selectedPath;
        }

        /// <summary>
        /// Create a new generator
        /// </summary>
        /// <param name="aircraft">All of the saved aircraft</param>
        /// <param name="airports">All of the saved airports</param>
        public PathGenerator(Group<Aircraft> aircraft, Group<Airport> airports)
        {
            rng = new Random();
            this.aircraft = aircraft;
            this.airports = airports;
        }
    }
}