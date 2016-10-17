using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Model.Entities.Concrete;
using Hotel.Repository.Abstract;
using System.Data.Entity;

namespace Hotel.Repository.Concrete
{
    public class BookingPeriodRepository : BasicRepository<BookingPeriod>, IBookingPeriodRepository
    {
        public BookingPeriodRepository ( HotelDbContext dbContext, DbSet<BookingPeriod> dbSet ) : base( dbContext, dbSet )
        {
        }
    }
}
