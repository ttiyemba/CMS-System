using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Dto
{
    public class PlottableEntityUpdateDto
    {
        public string Identifier { get; set; }
        public string Classification { get; set; }
        public string Subclassification { get; set; }
        public bool Armed { get; set; }
        public string Hostility { get; set; }
    }
}
