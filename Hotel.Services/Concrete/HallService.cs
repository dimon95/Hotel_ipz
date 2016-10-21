using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Dto;
using Hotel.Repository.Abstract;
using Hotel.Services.Abstract;
using Hotel.Model.Entities.Concrete;

namespace Hotel.Services.Concrete
{
    public class HallService : PlaceService, IHallService
    {
        public HallService ( IRoomRepository rRepo, IHallRepository hRepo ) 
            : base( rRepo, hRepo )
        {
        }

        public Guid CreateHall ( int number, string description, int personsCount, decimal price )
        {
            HallRepository.StartTransaction();

            Hall h = new Hall(Guid.NewGuid(), number, personsCount, price, description);

            HallRepository.Add(h);

            HallRepository.Commit();

            return h.Id;
        }

        public void DeletePlace ( Guid id )
        {
            Hall h = ServiceUtils.GetEntity(HallRepository, id);

            HallRepository.StartTransaction();

            HallRepository.Delete( h );

            HallRepository.Commit();
        }

        public HallDto View ( Guid id )
        {
            return ServiceUtils.GetEntity( HallRepository, id ).toDto();
        }

        public IList<Guid> ViewAll ()
        {
            return HallRepository.SelectAllDomainIds().ToList();
        }

        public override void ChangeDescription ( Guid placeId, string description )
        {
            HallRepository.StartTransaction();

            base.ChangeDescription( placeId, description );

            HallRepository.Commit();
        }

        public override void ChangePrice ( Guid placeId, decimal price )
        {
            HallRepository.StartTransaction();

            base.ChangePrice( placeId, price );

            HallRepository.Commit();
        }

        public override void ResetPlaceFromRestavration ( Guid placeId )
        {
            HallRepository.StartTransaction();

            base.ResetPlaceFromRestavration( placeId );

            HallRepository.Commit();
        }

        public override void SetPlaceOnRestavration ( Guid placeId )
        {
            HallRepository.StartTransaction();

            base.SetPlaceOnRestavration( placeId );

            HallRepository.Commit();
        }

        public IList<HallDto> GetFreeHalls ( PeriodDto period )
        {
            IQueryable<Hall> freeRooms = HallRepository.LoadAll().Where( r => r.isFree( ModelBuilder.BuildPeriod( period ) ) );

            List<HallDto> res = new List<HallDto>();

            foreach ( Hall h in freeRooms )
            {
                res.Add( h.toDto() );
            }

            return res;
        }
    }
}
