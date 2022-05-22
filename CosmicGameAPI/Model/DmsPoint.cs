using System;
using System.Collections.Generic;
using System.Text;

namespace CosmicGameAPI.Model
{
    public class DmsPoint
    {
        public Int64 Degrees { get; set; }

        public Int64 Minutes { get; set; }

        public Int64 Seconds { get; set; }

        public PointType Type { get; set; }

        public override string ToString()
        {
            return string.Format("{0}° {1}' {2}\" {3}",
                Math.Abs(Degrees),
                Minutes,
                Seconds,
                Type == PointType.Lat
                    ? Degrees < 0 ? "S" : "N"
                    : Degrees < 0 ? "W" : "E");
        }
    }

    public enum PointType
    {
        Lat,
        Lon
    }
}
