using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Repository.Abstract;
using Hotel.Model.Entities.Abstract;
using System.Data.Entity;
using Hotel.Model.Entities.Concrete;

namespace Hotel.Repository.Concrete
{
    public class BookingHolderRepository : BasicRepository<BookingHolder>, IBookingHolderRepository
    {
        public BookingHolderRepository ( HotelDbContext dbContext, DbSet<BookingHolder> dbSet ) : base( dbContext, dbSet )
        {
        }

        public IEnumerable<Guid> GetBookings ( Guid bookingHolderId )
        {
            return DbSet.FirstOrDefault( bh => bh.Id == bookingHolderId ).Bookings.Select( b => b.Id );
        }

        public IQueryable<Guid> GetCarts ()
        {
            return DbSet.Where(c => c is Cart).Select(c => c.Id);
        }

        public IQueryable<Guid> GetHistories ()
        {
            return DbSet.Where( c => c is BookingHistory ).Select( c => c.Id );

        }

        /*public override void Delete (BookingHolder bHolder)
        {
            BookingHistory history = bHolder as BookingHistory;

            if ( history != null )
            {
                foreach ( Booking b in history.Bookings )
                {
                    b.BookedRoom.DeleteBookingPeriod( b.BookingPeriod );
                }
            }

            base.Delete( bHolder );            
        }*/
    }
}
