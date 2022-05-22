using System;
using System.Collections.Generic;
using System.Text;

namespace CosmicGameAPI.Model
{
    public class DmsLocation
    {
        public DmsPoint Latitude { get; set; }
        public DmsPoint Longitude { get; set; }

        public override string ToString()
        {
            return string.Format("{0}, {1}",
                Latitude, Longitude);
        }
    }
}
