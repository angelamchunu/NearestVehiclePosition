using System;
using System.Diagnostics;

namespace NearestVehiclePosition
{
   
    public class VehicleFinder
	{
       
        internal static SortedList<double, Coord> FindClosetN(Coord coord, Coord[] vehiclePositions)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            SortedList<double, Coord> sortedList = new SortedList<double, Coord>();
          
            if (vehiclePositions.First().Equals(coord))
            {
                /*if the given coord is the first item in the list , then the value in the 2nd index will be the closest vehicle*/
                var distance = Distance(vehiclePositions[1].Latitude, vehiclePositions[1].Longitude, coord.Latitude, coord.Longitude);
                sortedList.Add(distance, vehiclePositions[1]);

            }
            else if (vehiclePositions.Last().Equals(coord))
            {
                /*if the given coord is the last item in the list , then the value in the 2nd last index will be the closest vehicle*/
                var distance = Distance(vehiclePositions.Last().Latitude, vehiclePositions.Last().Longitude, coord.Latitude, coord.Longitude);
                sortedList.Add(distance, vehiclePositions[vehiclePositions.Length-1]);

            }
            else
            {
                /* since the list is sorted by x coordinates, the numbers surrounding the given coord are the ones closest to the vehicle.
                 * I am getting the min distance between the vehicles in the list that are before and after the coord in the , thus getting the closest vehicle
                 this approach should do at most 2 distance calulations per given coordinate*/
                var minIndex = Array.IndexOf(vehiclePositions, coord) - 1;
                var maxIndex = Array.IndexOf(vehiclePositions, coord) + 1;

                var firstDistance = Distance(vehiclePositions[minIndex].Latitude, vehiclePositions[minIndex].Longitude, coord.Latitude, coord.Longitude);
                sortedList.Add(firstDistance, vehiclePositions[minIndex]);
                var secondDistance = Distance(vehiclePositions[maxIndex].Latitude, vehiclePositions[maxIndex].Longitude, coord.Latitude, coord.Longitude);
                sortedList.Add(secondDistance, vehiclePositions[maxIndex]);

            }
            stopwatch.Stop();
            Console.WriteLine($"Execution time : {stopwatch.ElapsedMilliseconds} ms");
            return sortedList;
        }

        private static double Distance(float firstLat, float firstLong, float secondLat, float secondLong)
        {
            return Math.Sqrt(Math.Pow(firstLat - secondLat, 2) + Math.Pow(firstLong - secondLong, 2));
        }
    }
}

