using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Dto
{
    public class RoomDto : PlaceDto
    {
        public int BedCount { get; private set; }

        public IDictionary<string, bool> SearchCriterias { get; private set; }

        public RoomDto ( Guid id, int number, decimal price, string description, int personsCount, bool onRestavration,
            int bedCount, IDictionary<string, bool> searchCriterias, IList<PeriodDto> bookings)
            : base( id, number, price, description, personsCount, onRestavration, bookings )
        {
            BedCount = bedCount;
            SearchCriterias = searchCriterias;
        }
    }
}
