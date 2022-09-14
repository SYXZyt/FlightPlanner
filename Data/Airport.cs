namespace FlightPlanner.Data
{
    /// <summary>
    /// Class to represent an airport
    /// </summary>
    internal sealed class Airport
    {
        private readonly string icao;
        private readonly string name;
        private readonly double latitude;
        private readonly double longitude;
        private readonly int shortestRunwayLength;
        private readonly string country;

        /// <summary>
        /// ICAO code used at this airport. For example <code>KXTA</code>
        /// </summary>
        public string ICAO => icao;

        /// <summary>
        /// The name of this airport. For example <code>Los Angeles International</code>
        /// </summary>
        public string Name => name;

        /// <summary>
        /// Latitude position
        /// </summary>
        public double Latitude => latitude;
        
        /// <summary>
        /// Longitude position
        /// </summary>
        public double Longitude => longitude;

        /// <summary>
        /// The shortest runway length in feet
        /// </summary>
        public int ShortestRunwayLength => shortestRunwayLength;

        /// <summary>
        /// The state or country that the airport is in. For example <code>UK</code> or <code>Texas US</code>
        /// </summary>
        public string Country => country;

        public override string ToString()
        {
            return $"{icao} - {name}";
        }

        /// <summary>
        /// Generate a group of airports, using an array of encoded airport data
        /// </summary>
        /// <param name="encodedAirports">All of the encoded data</param>
        /// <returns>The generated airports</returns>
        public static Group<Airport> CreateMultipleAirports(string[] encodedAirports)
        {
            List<Airport> planes = new();

            foreach (string s in encodedAirports)
            {
                planes.Add(new Airport(s));
            }

            return new(planes);
        }

        public override bool Equals(object obj)
        {
            if (obj is not Airport) return false;

            return GetHashCode() == ((Airport)obj).GetHashCode();
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public Airport(string icao, string name, double latitude, double longitude, int shortestRunwayLength, string country)
        {
            this.icao = icao;
            this.name = name;
            this.latitude = latitude;
            this.longitude = longitude;
            this.shortestRunwayLength = shortestRunwayLength;
            this.country = country;
        }

        public Airport(string encoded)
        {
            string[] bits = encoded.Split(';');

            //Check if there are too many bits, or too few
            if (bits.Length != 6) throw new MalformedEncoding();

            icao = bits[0];
            name = bits[1];

            if (!double.TryParse(bits[2], out latitude))
            {
                throw new MalformedEncoding();
            }

            if (!double.TryParse(bits[3], out longitude))
            {
                throw new MalformedEncoding();
            }

            if (!int.TryParse(bits[4], out shortestRunwayLength))
            {
                throw new MalformedEncoding();
            }

            country = bits[5];
        }
    }
}