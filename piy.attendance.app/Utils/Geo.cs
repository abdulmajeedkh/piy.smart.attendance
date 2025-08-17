using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace piy.attendance.app.Utils
{
    public static class Geo
    {
        // Returns distance in meters using Haversine
        public static double DistanceMeters(decimal lat1, decimal lon1, decimal lat2, decimal lon2)
        {
            const double R = 6371000; // meters
            double dLat = DegreesToRadians((double)(lat2 - lat1));
            double dLon = DegreesToRadians((double)(lon2 - lon1));
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(DegreesToRadians((double)lat1)) * Math.Cos(DegreesToRadians((double)lat2)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }

        private static double DegreesToRadians(double deg) => deg * Math.PI / 180.0;
    }
}
