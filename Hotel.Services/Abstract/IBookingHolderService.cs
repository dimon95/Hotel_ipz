using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Dto;

namespace Hotel.Services.Abstract
{
    public interface IBookingHolderService : IDomainEntityService<BookingHolderDto>
    {
        //cart
        //void AddItem ( Guid bookingHolderId, Guid bookingId );

        //cart
        //void DeleteItem ( Guid bookingHolderId, Guid bookingId );

        //cart
        //void Clear ( Guid bookingHolderId );

        //both
        IList<Guid> GetBookingsList ( Guid bookingHolderId );

        //hist
       // void ResheduleBooking ( Guid bookingHolderId, Guid bookingId, PeriodDto newPeriod );

        //cart
        //decimal CalculatePrice ( Guid cartId );
    }
}
