using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Model.Entities.Concrete;
using Hotel.Repository.Abstract;

namespace Hotel.Repository.Concrete
{
    public class BookingRepository : BasicRepository<Booking>, IBookingRepository
    {
        public BookingRepository ( HotelDbContext dbContext, DbSet<Booking> dbSet ) : base( dbContext, dbSet )
        {
        }
    }
}
