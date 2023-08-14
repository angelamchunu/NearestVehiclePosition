using System;
using System.Reflection;
using System.Text;

namespace NearestVehiclePosition
{
    internal static class VehiclePositionUtil
    {
        internal static DateTime Epoch => new DateTime(1970, 1, 1, 0, 0, 0, 0);

        internal static string GetLocalFilePath(string fileName)
        {
            return GetLocalFilePath(string.Empty, fileName);
        }

        internal static string GetLocalFilePath(string subDirectory, string fileName)
        {
            return Path.Combine(GetLocalPath(subDirectory), fileName);
        }

        internal static byte[] ToNullTerminatedString(string registration)
        {
            byte[] bytes = Encoding.Default.GetBytes(registration);
            byte[] array = new byte[bytes.Length + 1];
            bytes.CopyTo(array, 0);
            return array;
        }

        internal static string GetLocalPath(string subDirectory)
        {
            string location = Assembly.GetExecutingAssembly().Location;
            location = Path.GetDirectoryName(location);
            if (subDirectory != string.Empty)
            {
                location = Path.Combine(location, subDirectory);
            }
            return location;
        }

        internal static ulong ToCTime(DateTime time)
        {
            return Convert.ToUInt64((time - Epoch).TotalSeconds);
        }

        internal static DateTime FromCTime(ulong cTime)
        {
            return Epoch.AddSeconds(cTime);
        }

        internal static VehiclePosition FromBytes(byte[] buffer, ref int offset)
        {
            VehiclePosition vehiclePosition = new VehiclePosition();
            vehiclePosition.ID = BitConverter.ToInt32(buffer, offset);
            offset += 4;
            StringBuilder stringBuilder = new StringBuilder();
            while (buffer[offset] != 0)
            {
                stringBuilder.Append((char)buffer[offset]);
                offset++;
            }
            vehiclePosition.Registration = stringBuilder.ToString();
            offset++;
            vehiclePosition.Latitude = BitConverter.ToSingle(buffer, offset);
            offset += 4;
            vehiclePosition.Longitude = BitConverter.ToSingle(buffer, offset);
            offset += 4;
            ulong cTime = BitConverter.ToUInt64(buffer, offset);
            vehiclePosition.RecordedTimeUTC = FromCTime(cTime);
            offset += 8;
            return vehiclePosition;
        }
    }
}

