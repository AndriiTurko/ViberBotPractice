using practice.DAL.Models;

namespace practice.BLL.Helpers
{
    public static class CountHelper
    {
        private const double EarthRadius = 6371;

        public static List<List<TrackLocation>> GetWalks(List<TrackLocation> trackLocations)
        {
            var walks = new List<List<TrackLocation>>();

            var flag = 0;

            for (var i = 1; i < trackLocations.Count - 1; i++)
            {
                var timeDifference = trackLocations[i].date_track - trackLocations[i - 1].date_track;

                if (timeDifference.TotalMinutes > 30)
                {
                    walks.Add(trackLocations.GetRange(flag, i - flag));

                    flag = i;
                }
            }

            return walks;
        }

        public static Tuple<DateTime, double> WalkInfo(List<TrackLocation> trackLocations)
        {
            double distance = 0;
            var duration = trackLocations[^1].date_track - trackLocations[0].date_track;
            var dt = DateTime.MinValue.Add(duration);

            for (var i = 1; i < trackLocations.Count - 1; i++)
            {
                distance += GetDistance(trackLocations[i - 1], trackLocations[i]);
            }

            return Tuple.Create(dt, distance);
        }

        private static double GetDistance(TrackLocation first, TrackLocation second) 
        {
            var earthRadiusKm = 6371.0;

            var dLat = ToRadians(first.Latitude - second.Latitude);
            var dLon = ToRadians(first.Longitude - second.Longitude);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(ToRadians(first.Latitude)) * Math.Cos(ToRadians(second.Latitude)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var distance = earthRadiusKm * c;

            return distance;
        }

        private static double ToRadians(decimal degrees)
        {
            return Convert.ToDouble(degrees) * (Math.PI / 180);
        }
    }
}
