using System;
using System.Collections.Generic;
using System.IO;
namespace NearestVehiclePosition
{

    internal class DataFileParser
    {
        internal static List<VehiclePosition> ReadDataFile()
        {
            byte[] array = ReadFileData();
            List<VehiclePosition> list = new List<VehiclePosition>();
            int offset = 0;
            while (offset < array.Length)
            {
                list.Add(ReadVehiclePosition(array, ref offset));
            }
            return list;
        }

        internal static byte[] ReadFileData()
        {
            string localFilePath = VehiclePositionUtil.GetLocalFilePath("VehiclePositions.dat");          
            if (!File.Exists(localFilePath))
            {
                Console.WriteLine("Data file not found.");
                return null;
            }
            return File.ReadAllBytes(localFilePath);
        }

        private static VehiclePosition ReadVehiclePosition(byte[] data, ref int offset)
        {
            return VehiclePositionUtil.FromBytes(data, ref offset);
        }
    }
}
