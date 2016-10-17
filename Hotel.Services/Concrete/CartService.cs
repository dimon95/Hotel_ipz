using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Repository.Abstract;
using Hotel.Services.Abstract;
using Hotel.Model.Entities.Concrete;
using Hotel.Dto;
using Hotel.Model.Entities.Abstract;
using Hotel.Utils;

namespace Hotel.Services.Concrete
{
    public class CartService : BookingHolderService, ICartService
    {   
        /*private IBookingHolderRepository HolderRepository {get; set;}
        private IBookingRepository BookingRepository { get; set;}
        private IRoomRepository RoomRepository { get; set; }
        private IHallRepository HallRepository { get; set; }*/

        public CartService ( IBookingHolderRepository bhRepo, IBookingRepository bRepo,
                IRoomRepository rRepo, IHallRepository hRepo ) 
            : base( bhRepo, bRepo, rRepo, hRepo )
        {
        }

        public void AddItem ( Guid cartId, BookingDto booking )
        {
            Cart c = ServiceUtils.GetEntity<BookingHolder, Cart>(HolderRepository, cartId);

            BookingRepository.StartTransaction();

            BookingPeriod bp = ModelBuilder.BuildPeriod(booking.BookingPeriod);

            Booking b = new Booking(Guid.NewGuid(), bp, 
                ServiceUtils.GetPlace(RoomRepository, HallRepository, booking.BookedPlaceId),
                booking.Name, booking.Surname, booking.Middlename);

            c.AddBooking( b );

            BookingRepository.Commit();
        }

        public void Clear ( Guid cartId )
        {
            BookingRepository.StartTransaction();

            Cart c = ServiceUtils.GetEntity<BookingHolder, Cart>(HolderRepository, cartId);

            c.Clear();

            BookingRepository.Commit();
        }

        public void DeleteItem ( Guid cartId, Guid bookingId )
        {
            Cart c = ServiceUtils.GetEntity<BookingHolder, Cart>(HolderRepository, cartId);

            BookingRepository.StartTransaction();

            Booking b = ServiceUtils.GetEntity(BookingRepository, bookingId);

            c.DeleteBooking( b );

            BookingRepository.Commit();
        }

        /*public IList<Guid> GetBookingsList ( Guid bookingHolderId )
        {
            return HolderRepository.GetBookings( bookingHolderId ).ToList();
        }*/

        public decimal GetTotalCoast ( Guid cartId )
        {
            Cart c = ServiceUtils.GetEntity<BookingHolder, Cart>(HolderRepository, cartId);

            return c.Calculate();
        }

        public override IList<Guid> ViewAll ()
        {
            return HolderRepository.GetCarts().ToList();
        }
    }
}
