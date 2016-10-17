using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Dto;

namespace Hotel.Services.Abstract
{
    public interface IHistoryService : IBookingHolderService
    {
        void ResheduleBooking ( Guid HistoryId, Guid bookingId, PeriodDto newPeriod );
    }
}
