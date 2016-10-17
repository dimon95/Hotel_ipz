using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Dto;

namespace Hotel.Services.Abstract
{
    public interface IHallService : IPlaceService<HallDto>
    {
        Guid CreateHall ( int number, string description, int personsCount, decimal price );
    }
}
