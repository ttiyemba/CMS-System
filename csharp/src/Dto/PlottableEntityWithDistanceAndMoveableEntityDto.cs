using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Dto
{
    public class PlottableEntityWithDistanceAndMoveableEntityDto : PlottableEntityWithDistanceDto
    {
        public MoveableEntityDto MoveableEntity { get; set; }
    }
}
