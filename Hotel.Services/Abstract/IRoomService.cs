using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Dto;
using Hotel.Model.Entities.Concrete;

namespace Hotel.Services.Abstract
{
    public interface IRoomService : IPlaceService<RoomDto>
    {
        Guid CreateRoom ( int number, string description, int personsCount, int bedCount,
            decimal price);

        void SetCriteria ( Guid roomId, SearchCriteria criteria );

        void ResetCriteria ( Guid roomId, SearchCriteria criteria );

        IList<RoomDto> GetRooms ( SearchCriteria criteria );

        IList<RoomDto> GetFreeRooms ( PeriodDto period );
    }
}
