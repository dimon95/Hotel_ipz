using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Model.Entities.Abstract;
using Hotel.Utils;

namespace Hotel.Model.Entities.Concrete
{
    public class Cart : Abstract.BookingHolder
    {
        protected Cart () { }

        public Cart ( Guid id )
            : base( id )
        {
        }

        public decimal Calculate ()
        {
            decimal res = 0;

            foreach ( Booking b in Bookings )
            {
                res += b.BookedPlace.Price * b.BookingPeriod.CountDays(); 
            }

            return res;
        }

        public void MakeEmpty ()
        {
            /*foreach ( Booking b in Bookings )
            {

                b.BookedRoom.DeleteBookingPeriod( b.BookingPeriod );
            }*/

            Bookings.Clear();
        }

        public void Clear ()
        {
            foreach ( Booking b in Bookings )
            {

                b.BookedPlace.DeleteBookingPeriod( b.BookingPeriod );
            }

            Bookings.Clear();
        }
    }
}
