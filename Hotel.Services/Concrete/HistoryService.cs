using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Dto;
using Hotel.Repository.Abstract;
using Hotel.Services.Abstract;
using Hotel.Model.Entities.Concrete;
using Hotel.Model.Entities.Abstract;

namespace Hotel.Services.Concrete
{
    public class HistoryService : BookingHolderService, IHistoryService
    {
        public HistoryService ( IBookingHolderRepository bhRepo, IBookingRepository bRepo, 
            IRoomRepository rRepo, IHallRepository hRepo ) 
            : base( bhRepo, bRepo, rRepo, hRepo )
        {
        }

        public void ResheduleBooking ( Guid historyId, Guid bookingId, PeriodDto newPeriod )
        {
            Booking b = ServiceUtils.GetEntity(BookingRepository, bookingId);

            BookingHistory bh = ServiceUtils.GetEntity<BookingHolder, BookingHistory>(HolderRepository, historyId);

            if ( b.CheckStatus() != BookingStatus.Booked )
                throw new ArgumentException("Can't reshedule this booking");

            BookingRepository.StartTransaction();

            BookingPeriod bp = ModelBuilder.BuildPeriod(newPeriod);
            Booking oldBooking = BookingRepository.Load( bookingId );

            if ( b.BookedPlace.isFree( bp ) )
            {
                bh.AddBooking( new Booking( Guid.NewGuid(), bp, oldBooking.BookedPlace,
                    oldBooking.Name, oldBooking.Surname, oldBooking.Middlename ) );

                bh.DeleteBooking( oldBooking );
            }
            else
            if ( b.BookedPlace is Room )
            {
                Room r = RoomRepository.Find( bp );

                if ( r != null )
                {
                    bh.AddBooking( new Booking( Guid.NewGuid(), bp, r,
                    oldBooking.Name, oldBooking.Surname, oldBooking.Middlename ) );
                }
                else
                {
                    BookingRepository.Rollback();                    
                    throw new ArgumentException( "No free rooms found" );
                }
            }
            else
            {
                BookingRepository.Rollback();
                throw new ArgumentException( "No free places found" );
            }

            BookingRepository.Commit();
        }

        public override IList<Guid> ViewAll ()
        {
            return HolderRepository.GetHistories().ToList();
        }
    }
}
