﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Dto;
using Hotel.Repository;
using Hotel.Services;
using Hotel.Model.Entities.Concrete;
using Hotel.Exceptions;

namespace Hotel.Services.Impl
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
            if ( RoomRepository.Find(number) != null )
                throw new DuplicateNamedEntityException( typeof( Room ), number.ToString() );


            Room r = new Room(Guid.NewGuid(), number, personsCount, price, description, bedCount);

            RoomRepository.Add( r );


            return r.Id;
        }

        public void ResetCriteria ( Guid roomId, SearchCriteria criteria )
        {
            if ( !RoomRepository.Load( roomId ).HasCriteria( criteria ) )
                throw new RemovingNotExistingRoomCriteriaException( roomId );


            Room r = ServiceUtils.GetEntity(RoomRepository, roomId);

            r.ResetCriteria( criteria );

        }

        public void SetCriteria ( Guid roomId, SearchCriteria criteria )
        {
            if ( RoomRepository.Load( roomId ).HasCriteria( criteria ) )
                throw new MultipleAddingRoomCriteriaException( roomId );


            Room r = ServiceUtils.GetEntity(RoomRepository, roomId);

            r.SetCriteria( criteria );

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


            RoomRepository.Delete( r );

        }

        public override void ChangeDescription ( Guid placeId, string description )
        {

            base.ChangeDescription( placeId, description );

  
        }

        public override void ChangePrice ( Guid placeId, decimal price )
        {

            base.ChangePrice( placeId, price );

        }

        public override void ResetPlaceFromRestavration ( Guid placeId )
        {

            base.ResetPlaceFromRestavration( placeId );

        }

        public override void SetPlaceOnRestavration ( Guid placeId )
        {

            base.SetPlaceOnRestavration( placeId );

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
            BookingPeriod bp = ModelBuilder.BuildPeriod(period);

            if ( bp.Begin < Utils.BookingDate.GetToday() || bp.End < Utils.BookingDate.GetToday()
                || bp.Begin > Utils.BookingDate.GetMax() || bp.End > Utils.BookingDate.GetMax() ||
                bp.Begin > bp.End )
                throw new InvalidDataPeriodException();

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
