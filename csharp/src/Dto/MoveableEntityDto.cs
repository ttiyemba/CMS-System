using src.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Dto
{
    public class MoveableEntityDto
    {
        private float _bearing;
        public float Bearing
        {
            get
            {
                return _bearing;
            }
            set
            {
                if (ValidateDegrees(value))
                {
                    _bearing = value;
                }
            }
        }
        private float _heading;
        public float Heading
        {
            get
            {
                return _heading;
            }
            set
            {
                if (ValidateDegrees(value))
                {
                    _heading = value;
                }
            }
        }
        public float Speed { get; set; }

        private bool ValidateDegrees(float degrees)
        {
            bool valid = false;
            if ((0 <= degrees) && (degrees < 360))
            {
                valid = true;
            }
            return valid;
        }
    }
}
