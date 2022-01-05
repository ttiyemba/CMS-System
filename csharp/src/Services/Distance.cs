using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Services
{
    //Gives the straight-line distance between two points using the Haversine Formula
    public class Distance
    {
        public double GetDistanceIgnoringElevation(double lat1, double lon1, double lat2, double lon2)
        {
            double flatDist;
            double flatDistTwoDecimalPlaces;
            double earthRadius = 6371; //Radius of the Earth in KM
            double deltaLat = ToRadians(lat2 - lat1);
            double deltaLon = ToRadians(lon2 - lon1);
            lat1 = ToRadians(lat1);
            lat2 = ToRadians(lat2);

            double a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) + Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
            double c = 2 * Math.Asin(Math.Sqrt(a));
            flatDist = earthRadius * c;
            flatDistTwoDecimalPlaces = Math.Round(flatDist, 2);
            return flatDistTwoDecimalPlaces;
        }

        public double ToRadians(double angle)
        {
            return Math.PI * angle/ 180.0;
        }

        public double GetDistance(double height1, double height2, double lat1, double lon1, double lat2, double lon2)
        {
            double flatDist = GetDistanceIgnoringElevation(lat1, lon1, lat2, lon2);
            double heightTotal = height2 - height1;
            double hypotSquared = Math.Pow(flatDist, 2) + Math.Pow(heightTotal, 2);
            double distanceWithElevation = Math.Sqrt(hypotSquared);
            double distanceWithElevationTwoDecimalPlaces = Math.Round(distanceWithElevation, 2);
            return distanceWithElevationTwoDecimalPlaces;
        }


    }
}
