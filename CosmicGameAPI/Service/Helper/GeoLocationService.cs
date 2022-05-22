using CosmicGameAPI.Model;

namespace CosmicGameAPI.Service.Helper
{
    public class GeoLocationService
    {
        #region Get Decimal locations to DMS and DMS to Decimal location
        public DmsLocation GetDmsLocation(decimal Latitude, decimal Longitude)
        {
            return new DmsLocation
            {
                Latitude = new DmsPoint
                {
                    Degrees = ExtractDegrees(Latitude),
                    Minutes = ExtractMinutes(Latitude),
                    Seconds = ExtractSeconds(Latitude),
                    Type = PointType.Lat
                },
                Longitude = new DmsPoint
                {
                    Degrees = ExtractDegrees(Longitude),
                    Minutes = ExtractMinutes(Longitude),
                    Seconds = ExtractSeconds(Longitude),
                    Type = PointType.Lon
                }
            };
        }

        Int64 ExtractDegrees(decimal value)
        {
            return (Int64)value;
        }

        Int64 ExtractMinutes(decimal value)
        {
            value = Math.Abs(value);
            return (Int64)((value - ExtractDegrees(value)) * 60);
        }

        Int64 ExtractSeconds(decimal value)
        {
            value = Math.Abs(value);
            decimal minutes = (value - ExtractDegrees(value)) * 60;
            return (Int64)Math.Round((minutes - ExtractMinutes(value)) * 60);
        }
        #endregion
    }
}
