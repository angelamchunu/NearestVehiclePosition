// See https://aka.ms/new-console-template for more information
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace NearestVehiclePosition { 

 class Program

  
    {
     
        static void Main(string[] args)
        {

            List<VehiclePosition> rawVehiclePositions = DataFileParser.ReadDataFile();
            SortedDictionary<Coord, VehiclePosition> vehiclePositions = new SortedDictionary<Coord, VehiclePosition>();

            foreach (var rawVehiclePosition in rawVehiclePositions)
            {
                if (!vehiclePositions.ContainsKey(new Coord(rawVehiclePosition.Latitude, rawVehiclePosition.Longitude)))
                {
                    /* it seems that the duplicates were the same vehicle in the same position so one entry will suffice for the problem */
                    vehiclePositions.Add(new Coord(rawVehiclePosition.Latitude, rawVehiclePosition.Longitude), rawVehiclePosition);
                }
            }
            Stopwatch stopwatch = Stopwatch.StartNew();

            foreach (var coord in GetLookupPositions())
            {
                vehiclePositions.Add(new Coord(coord.Latitude, coord.Longitude), new VehiclePosition());
                var closestVehiclePositions = VehicleFinder.FindClosetN(coord, vehiclePositions.Keys.ToArray());
                Console.WriteLine($"The closest vehicle to coord  [{coord.Latitude}] [{coord.Longitude}]  is : {vehiclePositions[closestVehiclePositions.First().Value].Registration}");
            }
            stopwatch.Stop();
            Console.WriteLine($"Total Execution time for all 10 : {stopwatch.ElapsedMilliseconds} ms");
        }

        private static Coord[] GetLookupPositions()
        {
            Coord[] array = new Coord[10];
            array[0].Latitude = 34.54491f;
            array[0].Longitude = -102.100845f;
            array[1].Latitude = 32.345543f;
            array[1].Longitude = -99.12312f;
            array[2].Latitude = 33.234234f;
            array[2].Longitude = -100.21413f;
            array[3].Latitude = 35.19574f;
            array[3].Longitude = -95.3489f;
            array[4].Latitude = 31.89584f;
            array[4].Longitude = -97.78957f;
            array[5].Latitude = 32.89584f;
            array[5].Longitude = -101.78957f;
            array[6].Latitude = 34.115837f;
            array[6].Longitude = -100.22573f;
            array[7].Latitude = 32.33584f;
            array[7].Longitude = -99.99223f;
            array[8].Latitude = 33.53534f;
            array[8].Longitude = -94.79223f;
            array[9].Latitude = 32.234234f;
            array[9].Longitude = -100.22222f;
            return array;
        }

    }
}

