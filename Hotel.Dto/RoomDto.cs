using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Model.Entities.Concrete;
namespace Hotel.Dto
{
    public class RoomDto : PlaceDto
    {

        private IDictionary<string, bool> SearchCriteriaToDictionary ( int sCriteria )
        {
            Dictionary<string, bool> res = new Dictionary<string, bool>();

            byte sc = 0x01;

            while ( sc < ( byte ) SearchCriteria.Count )
            {
                res.Add( ( ( SearchCriteria ) sc ).ToString(), (sCriteria & sc) != 0 );
                sc = ( byte ) ( sc * 2 );
            }

            return res;
        }

        public int BedCount { get; private set; }

        public IDictionary<string, bool> SearchCriterias { get; private set; }

        public RoomDto ( Guid id, int number, decimal price, string description, int personsCount, bool onRestavration,
            int bedCount, int searchCriterias, IList<PeriodDto> bookings)
            : base( id, number, price, description, personsCount, onRestavration, bookings )
        {
            BedCount = bedCount;
            SearchCriterias = SearchCriteriaToDictionary( searchCriterias );
        }

        public override string ToString ()
        {
            string res = base.ToString();

            foreach ( var val in SearchCriterias )
            {
                res += val.Value == true ? "Has " : "Doesn't have ";
                res += val.Key + "\r\n"; 
            }

            res += "\r\n\r\n";

            return res;
        }
    }
}
