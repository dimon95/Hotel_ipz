using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Model.Entities.Abstract;

namespace Hotel.Repository.Abstract
{
    public interface IBookingHolderRepository : IRepository<BookingHolder>
    {
        IEnumerable<Guid> GetBookings ( Guid bookingHolderId );

        IQueryable<Guid> GetCarts ();

        IQueryable<Guid> GetHistories ();

    }
}
