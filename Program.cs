using FlightPlanner.Data;
using FlightPlanner.Paths;

using Path = FlightPlanner.Paths.Path;

namespace FlightPlanner
{
    internal static class Program
    {
        private const string PlaneDataFile = "Config\\Plane.txt";
        private const string AirportDataFile = "Config\\Airport.txt";

        private static Group<Aircraft> aircraft;
        private static Group<Airport> airports;

        internal static string[] ReadEncodedData(string encodedFile)
        {
            List<string> lines = new();

            using StreamReader fs = new(new FileStream(encodedFile, FileMode.Open));

            while (!fs.EndOfStream)
            {
                string line = fs.ReadLine();
                if (line is null) break;

                //We want to check if the first proper character is a comment, and skip if so
                if (line.FirstNonWhiteChar() == ';') continue;
                //We can do the same check with blank lines
                if (line.FirstNonWhiteChar() == ' ') continue;

                //Now we know we have actual code, so we can add it to our encoded list
                lines.Add(line);
            }

            return lines.ToArray();
        }

        private static void Main()
        {
            Console.WriteLine("Loading data...");

            GlobalData.LoadFromConfig();

            //Check if our data files exist
            if (!File.Exists(PlaneDataFile) || !File.Exists(AirportDataFile))
            {
                throw new FileNotFoundException("Configuration file not found", $"{PlaneDataFile} or {AirportDataFile}");
            }

            string[] planeData = ReadEncodedData(PlaneDataFile);
            aircraft = Aircraft.CreateMultipleAircraft(planeData);

            string[] airportData = ReadEncodedData(AirportDataFile);
            airports = Airport.CreateMultipleAirports(airportData);

            PathGenerator pathGenerator = new(aircraft, airports);
            
            while (true)
            {
                Path path = pathGenerator.GeneratePath();
                Console.WriteLine($"I have generated {path} using {pathGenerator.SelectedAircraft} (Estimated {Math.Round(path.EstimatedTime(pathGenerator.SelectedAircraft), 2)} minutes)");
                Console.WriteLine("Press enter to generate a new path");
                Console.ReadLine();
            }
        }
    }
}