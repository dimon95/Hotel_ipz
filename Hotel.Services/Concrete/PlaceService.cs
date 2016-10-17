using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Dto;
using Hotel.Services.Abstract;
using Hotel.Repository.Abstract;
using Hotel.Model.Entities.Concrete;
using Hotel.Model.Entities.Abstract;

namespace Hotel.Services.Concrete
{
    public abstract class PlaceService 
    {
        private IList<IRepository<Place>> _placesRepos;

        protected IRoomRepository RoomRepository { get; private set; }
        protected IHallRepository HallRepository { get; private set; }

        /*protected IList<IRepository<Place>> GetPlacesRepos
        {
            get
            {
                if ( _placesRepos == null )
                {
                    _placesRepos = new List<IRepository<Place>>();

                    _placesRepos.Add( RoomRepository as IRepository<Place> );
                    _placesRepos.Add( HallRepository as IRepository<Place> );
                }

                return _placesRepos;
            }
        }*/

        protected PlaceService ( IRoomRepository rRepo, IHallRepository hRepo )
        {
            RoomRepository = rRepo;
            HallRepository = hRepo;
        }

        public virtual void ChangeDescription ( Guid placeId, string description )
        {            
            Place p = ServiceUtils.GetPlace(RoomRepository, HallRepository, placeId);

            p.Description = description;
        }

        public virtual void ChangePrice ( Guid placeId, decimal price )
        {
            Place p = ServiceUtils.GetPlace(RoomRepository, HallRepository, placeId);

            p.Price = price;
        }

        public IList<PeriodDto> GetBookedPeriodsFor ( Guid placeId )
        {
            IList<PeriodDto> res = new List<PeriodDto>();

            Place p = ServiceUtils.GetPlace(RoomRepository, HallRepository, placeId);

            foreach ( BookingPeriod bp in p.Bookings )
            {
                res.Add( bp.toDto() );
            }

            return res;
        }

        public virtual void ResetPlaceFromRestavration ( Guid placeId )
        {
            Place p = ServiceUtils.GetPlace(RoomRepository, HallRepository, placeId);

            p.ResetFromRestavretion();
        }

        public virtual void SetPlaceOnRestavration ( Guid placeId )
        {
            Place p = ServiceUtils.GetPlace(RoomRepository, HallRepository, placeId);

            p.SetOnRestavretion();
        }

        /*public PlaceDto View ( Guid id )
        {
            return 
        }

        public IList<Guid> ViewAll ()
        {
            throw new NotImplementedException();
        }*/
    }
}
