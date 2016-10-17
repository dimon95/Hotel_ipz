using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Model.Entities.Abstract;
using Hotel.Model.Entities.Concrete;
using Hotel.Dto;
using Hotel.Services.Abstract;
using Hotel.Repository.Abstract;


namespace Hotel.Services.Concrete
{
    public abstract class BookingHolderService : IBookingHolderService
    {

        protected IBookingHolderRepository HolderRepository { get; private set; }
        protected IBookingRepository BookingRepository { get; private set; }
        protected IRoomRepository RoomRepository { get; private set; }
        protected IHallRepository HallRepository { get; private set; }

        protected BookingHolderService ( IBookingHolderRepository bhRepo, IBookingRepository bRepo, 
            IRoomRepository roomRepo, IHallRepository hallRepo )
        {
            HolderRepository = bhRepo;
            BookingRepository = bRepo;
            RoomRepository = roomRepo;
            HallRepository = hallRepo;
        }


        /*protected Place GetPlace ( Guid id )
        {
            Room r = RoomRepository.Load(id);
            Hall h = HallRepository.Load(id);

            if ( r == null && h == null )
                throw new ArgumentException( "invalid id" );

            return ( Place ) r == null ? ( Place ) h : ( Place ) r;
        }*/

        public IList<Guid> GetBookingsList ( Guid bookingHolderId )
        {            
            return HolderRepository.GetBookings( bookingHolderId ).ToList();
        }

        public BookingHolderDto View ( Guid id )
        {
            return ServiceUtils.GetEntity( HolderRepository, id ).toDto();
        }

        public abstract IList<Guid> ViewAll ();

        /*public IList<Guid> ViewAll ()
        {
            return HolderRepository.SelectAllDomainIds().ToList();
        }*/
    }
}
