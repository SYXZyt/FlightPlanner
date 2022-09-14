namespace FlightPlanner.Data
{
    internal class Group<T> : IEnumerable<T>
    {
        private readonly List<T> values;

        #region Enumerable Stuff

        private IEnumerable<T> GetValues()
        {
            foreach (T t in values)
            {
                yield return t;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return GetValues().GetEnumerator();
        }

        #endregion

        public T PickRandom(Random rng) => rng.Choice(values);

        public T this[int index]
        {
            get => values[index];
            set => values[index] = value;
        }

        public void Add(T value) => values.Add(value);

        public void AddRange(IEnumerable<T> values) => this.values.AddRange(values);

        public Group()
        {
            values = new();
        }

        public Group(IEnumerable<T> values)
        {
            this.values = new(values.Count());
            this.values.AddRange(values);
        }
    }
}