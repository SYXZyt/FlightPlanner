using System.Text;

namespace FlightPlanner.Data
{
    internal class Aircraft : IEnumerable<Model>
    {
        #region Enumerable Stuff
        private IEnumerable<Model> GetValues()
        {
            foreach (Model m in models)
            {
                yield return m;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Model> GetEnumerator()
        {
            return GetValues().GetEnumerator();
        }
        #endregion

        private readonly List<Model> models;

        private string name;

        public string Name => name;

        public Model PickModel(Random rng) => rng.Choice(models);

        public override string ToString()
        {
            StringBuilder builder = new();
            builder.Append(name);

            foreach (Model m in this)
            {
                builder.Append(m.ToString());
                builder.Append('/');
            }

            return builder.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (obj is not Aircraft) return false;
            Aircraft other = obj as Aircraft;

            return GetHashCode() == other.GetHashCode();
        }

        public override int GetHashCode() => name.GetHashCode();

        public static bool operator ==(Aircraft a, Aircraft b) => a.Equals(b);
        public static bool operator !=(Aircraft a, Aircraft b) => !a.Equals(b);

        public static implicit operator Aircraft(string s)
        {
            Aircraft a = new()
            {
                name = s
            };

            return a;
        }

        public static Group<Aircraft> CreateMultipleAircraft(string[] planeData)
        {
            Group<Aircraft> aircraft = new();

            foreach (string plane in planeData)
            {
                //Check if this plane is already existing, and if it is, we can just add a new model
                //If not, create a new plane
                if (aircraft.Contains(CheckPlaneName(plane)))
                {
                    aircraft[CheckPlaneName(plane)].AddModel(GenerateModel(plane));
                }
                else
                {
                    aircraft.Add(new(plane));
                }
            }

            return aircraft;
        }

        public static string CheckPlaneName(string encoded)
        {
            string[] bits = encoded.Split(';');

            if (bits.Length != 5)
            {
                //Check if the length is 7, and the last is empty, because there is likely a terminating semicolon that was placed by mistake
                if (!(bits.Length == 6 && bits[^1] == "")) throw new MalformedEncoding(encoded);
            }

            return bits[0];
        }

        public void AddModel(Model model) => models.Add(model);

        public static Model GenerateModel(string encoded)
        {
            string[] bits = encoded.Split(';');

            //Check for the correct amount of bits
            if (bits.Length != 5)
            {
                //Check if the length is 7, and the last is empty, because there is likely a terminating semicolon that was placed by mistake
                if (!(bits.Length == 6 && bits[^1] == "")) throw new MalformedEncoding(encoded);
            }

            string name = bits[1];

            if (!float.TryParse(bits[2], out float range))
            {
                throw new MalformedEncoding(encoded);
            }

            if (!float.TryParse(bits[3], out float endurance))
            {
                throw new MalformedEncoding(encoded);
            }

            if (!float.TryParse(bits[4], out float cruiseSpeed))
            {
                throw new MalformedEncoding(encoded);
            }

            return new(name, range, endurance, cruiseSpeed);
        }

        private Aircraft()
        {
            models = new();
            name = string.Empty;
        }

        public Aircraft(string encoded)
        {
            models = new();

            string[] bits = encoded.Split(';');

            //Check for the correct amount of bits
            if (bits.Length != 5)
            {
                //Check if the length is 7, and the last is empty, because there is likely a terminating semicolon that was placed by mistake
                if (!(bits.Length == 6 && bits[^1] == "")) throw new MalformedEncoding(encoded);
            }

            name = bits[0];

            Model model = GenerateModel(encoded);
            models.Add(model);
        }
    }
}