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
        private void Reshedule ( BookingHistory history, Booking oldBooking, BookingPeriod bp )
        {
            

            if ( oldBooking.BookedPlace.isFree( bp ) )
            {
                history.AddBooking( new Booking( Guid.NewGuid(), bp, oldBooking.BookedPlace,
                    oldBooking.Name, oldBooking.Surname, oldBooking.Middlename ) );

                history.DeleteBooking( oldBooking );
            }
            else
            {
                throw new ArgumentException( "No free places found" );
            }
        }

        public HistoryService ( IBookingHolderRepository bhRepo, IBookingRepository bRepo, 
            IRoomRepository rRepo, IHallRepository hRepo ) 
            : base( bhRepo, bRepo, rRepo, hRepo )
        {
        }

        public void ResheduleBooking ( Guid historyId, Guid bookingId, PeriodDto newPeriod )
        {
            BookingHistory bh = ServiceUtils.GetEntity<BookingHolder, BookingHistory>(HolderRepository, historyId);
            Booking oldBooking = HolderRepository.GetBooking(bh.Id, bookingId);

            if ( oldBooking.CheckStatus() != BookingStatus.Booked )
                throw new ArgumentException("Can't reshedule this booking");

            BookingRepository.StartTransaction();
            try
            {
                Reshedule( bh, oldBooking, ModelBuilder.BuildPeriod( newPeriod ) );
            }
            catch ( ArgumentException e )
            {
                BookingRepository.Rollback();
                throw e;
            }

            BookingRepository.Commit();
 
        }

        public void ResheduleBooking ( Guid historyId, Guid bookingId, IList<PeriodDto> newPeriods )
        {
            BookingHistory bh = ServiceUtils.GetEntity<BookingHolder, BookingHistory>(HolderRepository, historyId);
            Booking oldBooking = HolderRepository.GetBooking(bh.Id, bookingId);

            if ( oldBooking.CheckStatus() != BookingStatus.Booked )
                throw new ArgumentException( "Can't reshedule this booking" );

            BookingRepository.StartTransaction();
            try
            {
                foreach ( PeriodDto p in newPeriods )
                {
                    Reshedule( bh, oldBooking, ModelBuilder.BuildPeriod( p ) );
                }
            }
            catch ( ArgumentException e )
            {
                BookingRepository.Rollback();
                throw e;
            }

            BookingRepository.Commit();
        }

        public override IList<Guid> ViewAll ()
        {
            return HolderRepository.GetHistories().ToList();
        }
    }
}
