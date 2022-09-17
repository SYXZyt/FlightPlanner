namespace FlightPlanner.Data
{
    /// <summary>
    /// Class to represent an aeroplane model
    /// </summary>
    internal sealed class Model
    {
        private readonly string name;
        private readonly float range;
        private readonly float endurance;
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
        /// This name of this plane model. For example <code>B (in F-35B)</code>
        /// </summary>
        public string Name => name;

        /// <summary>
        /// How many knots this plane can cruise at
        /// </summary>
        public float CruiseSpeed => cruiseSpeed;

        public override string ToString()
        {
            return name;
        }

        public Model(string name, float range, float endurance, float cruiseSpeed)
        {
            this.name = name;
            this.range = range;
            this.endurance = endurance;
            this.cruiseSpeed = cruiseSpeed;
        }
    }
}