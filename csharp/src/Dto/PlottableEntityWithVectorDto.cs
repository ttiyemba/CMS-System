using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Dto
{
    public class PlottableEntityWithVectorDto

    {
        public int Id { get; set; }

        public string Identifier { get; set; }
        public string Classification { get; set; }
        public string Subclassification { get; set; }
        private float _longitude;
        public float Longitude
        {
            get
            {
                return _longitude;
            }
            set
            {
                if (ValidateLongitude(value))
                {
                    _longitude = value;
                }
            }
        }
        private float _latitude;
        public float Latitude
        {
            get
            {
                return _latitude;
            }
            set
            {
                if (ValidateLatitude(value))
                {
                    _latitude = value;
                }
            }
        }
        public int Elevation { get; set; }
        private ulong _lastUpdate;
        public ulong LastUpdate
        {
            get
            {
                return _lastUpdate;
            }
            set
            {
                if (ValidateLastUpdate(value))
                {
                    _lastUpdate = value;
                }
            }
        }
        public bool Armed { get; set; }
        public string Hostility { get; set; }
        private bool ValidateLongitude(float longitude)
        {
            bool valid = false;
            if (-180 <= longitude && longitude <=180)
            {
                valid = true;
            }
            return valid;
        }
        private bool ValidateLatitude(float latitude)
        {
            bool valid = false;
            if (-90 <= latitude && latitude <=90)
            {
                valid = true;
            }
            return valid;
        }
        private bool ValidateLastUpdate(ulong LastUpdate)
        {
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            ulong now = (ulong)t.TotalMilliseconds;
            bool valid = false;
            if (LastUpdate <= now)
            {
                valid = true;
            }
            return valid;
        }

        public MoveableEntityDto MoveableEntity { get; set; }
    }
}
