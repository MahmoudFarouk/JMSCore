using System;
using System.Collections.Generic;
using System.Text;

namespace JMS.BLL.Common
{
    public static class LocationHandler
    {
        /// <summary>
        /// Calculate Distance Between Two locations in Kilometers
        /// </summary>
        /// <param name="lat1">Latitude of the first location</param>
        /// <param name="lon1">Longitude of the first location</param>
        /// <param name="lat2">Latitude of the second location</param>
        /// <param name="lon2">Longitude of the second location</param>
        /// <returns>Distance in Kilometers</returns>
        public static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            if ((lat1 == lat2) && (lon1 == lon2))
            {
                return 0;
            }
            else
            {
                double theta = lon1 - lon2;
                double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
                dist = Math.Acos(dist);
                dist = rad2deg(dist);
                dist = dist * 60 * 1.1515;

                dist = dist * 1.609344;

                return dist;
            }
        }

        private static double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private static double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
    }
}
