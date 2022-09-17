namespace FlightPlanner.Data
{
    internal class MalformedEncoding : Exception
    {
        protected string encoded;

        public string Encoded => encoded;

        public MalformedEncoding(string encoded) : base()
        {
            this.encoded = encoded;
        }
    }
}