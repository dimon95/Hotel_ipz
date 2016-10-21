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
    public class RoomService : PlaceService, IRoomService
    {
        public RoomService ( IRoomRepository rRepo, IHallRepository hRepo ) : 
            base( rRepo, hRepo )
        {
        }

        public Guid CreateRoom ( int number, string description, int personsCount, int bedCount, 
            decimal price )
        {
            RoomRepository.StartTransaction();

            Room r = new Room(Guid.NewGuid(), number, personsCount, price, description, bedCount);

            RoomRepository.Add( r );

            RoomRepository.Commit();

            return r.Id;
        }

        public void ResetCriteria ( Guid roomId, SearchCriteria criteria )
        {
            RoomRepository.StartTransaction();

            Room r = ServiceUtils.GetEntity(RoomRepository, roomId);

            r.ResetCriteria( criteria );

            RoomRepository.Commit();
        }

        public void SetCriteria ( Guid roomId, SearchCriteria criteria )
        {
            RoomRepository.StartTransaction();

            Room r = ServiceUtils.GetEntity(RoomRepository, roomId);

            r.SetCriteria( criteria );

            RoomRepository.Commit();
        }

        public RoomDto View ( Guid id )
        {
            return ServiceUtils.GetEntity( RoomRepository, id ).toDto();
        }

        public IList<Guid> ViewAll ()
        {
            return RoomRepository.SelectAllDomainIds().ToList();
        }

        public void DeletePlace ( Guid id )
        {
            Room r = ServiceUtils.GetEntity(RoomRepository, id);

            RoomRepository.StartTransaction();

            RoomRepository.Delete( r );

            RoomRepository.Commit();
        }

        public override void ChangeDescription ( Guid placeId, string description )
        {
            RoomRepository.StartTransaction();

            base.ChangeDescription( placeId, description );

            RoomRepository.Commit();
  
        }

        public override void ChangePrice ( Guid placeId, decimal price )
        {
            RoomRepository.StartTransaction();

            base.ChangePrice( placeId, price );

            RoomRepository.Commit();
        }

        public override void ResetPlaceFromRestavration ( Guid placeId )
        {
            RoomRepository.StartTransaction();

            base.ResetPlaceFromRestavration( placeId );

            RoomRepository.Commit();
        }

        public override void SetPlaceOnRestavration ( Guid placeId )
        {
            RoomRepository.StartTransaction();

            base.SetPlaceOnRestavration( placeId );

            RoomRepository.Commit();
        }

        public IList<RoomDto> GetRooms ( SearchCriteria criteria )
        {
            List<RoomDto> res = new List<RoomDto>();

            foreach ( Room r in RoomRepository.LoadAll() )
            {
                if ( ( r.SearchCriterias & ( byte ) criteria ) != 0 )
                    res.Add( r.toDto() );
            }

            return res;
        }

        public IList<RoomDto> GetFreeRooms ( PeriodDto period )
        {
            IQueryable<Room> freeRooms = RoomRepository.LoadAll().Where( r => r.isFree( ModelBuilder.BuildPeriod( period ) ) );

            List<RoomDto> res = new List<RoomDto>();

            foreach ( Room r in freeRooms )
            {
                res.Add( r.toDto() );
            }

            return res;
        }
    }
}
