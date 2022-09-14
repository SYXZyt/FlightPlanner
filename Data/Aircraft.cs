namespace FlightPlanner.Data
{
    /// <summary>
    /// Class to represent an aeroplane
    /// </summary>
    internal sealed class Aircraft
    {
        private readonly string name;
        private readonly string model;
        private readonly float range;
        private readonly float endurance;
        private readonly int reqRunway;
        private readonly float cruiseSpeed;

        /// <summary>
        /// How many nautical miles this plane can fly
        /// </summary>
        public float Range => range;

        /// <summary>
        /// How many hours this plane can fly (currently unused)
        /// </summary>
        public float Endurance => endurance;

        /// <summary>
        /// This name of this plane. For example <code>F-35</code>
        /// </summary>
        public string Name => name;

        /// <summary>
        /// This is the model of the specific plane. For example <code>B</code>
        /// </summary>
        public string Model => model;

        /// <summary>
        /// How many feet of runway required to operate this plane
        /// </summary>
        public int RequiredRunwayLength => reqRunway;

        /// <summary>
        /// How many knots this plane can cruise at
        /// </summary>
        public float CruiseSpeed => cruiseSpeed;

        public override string ToString()
        {
            return $"{name}{model}";
        }

        /// <summary>
        /// Generate a group of planes, using an array of encoded plane data
        /// </summary>
        /// <param name="encodedPlanes">All of the encoded data</param>
        /// <returns>The generated planes</returns>
        public static Group<Aircraft> CreateMultipleAircraft(string[] encodedPlanes)
        {
            List<Aircraft> planes = new();

            foreach (string s in encodedPlanes)
            {
                planes.Add(new Aircraft(s));
            }

            return new(planes);
        }

        public Aircraft(string name, string model, float range, float endurance, int reqRunway, float cruiseSpeed)
        {
            this.name = name;
            this.model = model;
            this.range = range;
            this.endurance = endurance;
            this.reqRunway = reqRunway;
            this.cruiseSpeed = cruiseSpeed;
        }

        public Aircraft(string encoded)
        {
            string[] bits = encoded.Split(';');

            //Check if there are too many bits, or too few
            if (bits.Length != 6) throw new MalformedEncoding();

            name = bits[0];
            model = bits[1];
            
            if (!float.TryParse(bits[2], out range))
            {
                throw new MalformedEncoding();
            }

            if (!float.TryParse(bits[3], out endurance))
            {
                throw new MalformedEncoding();
            }

            if (!int.TryParse(bits[4], out reqRunway))
            {
                throw new MalformedEncoding();
            }

            if (!float.TryParse(bits[5], out cruiseSpeed))
            {
                throw new MalformedEncoding();
            }
        }
    }
}