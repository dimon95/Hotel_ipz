using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Dto;

namespace Hotel.Services.Abstract
{
    public interface IPlaceService<T> : IDomainEntityService<T>
        where T : PlaceDto
        
    {
        void DeletePlace ( Guid id );

        IList<PeriodDto> GetBookedPeriodsFor ( Guid placeId );

        void SetPlaceOnRestavration ( Guid placeId );

        void ResetPlaceFromRestavration ( Guid placeId );

        void ChangeDescription ( Guid placeId, string description );

        void ChangePrice ( Guid placeId, decimal price );
    }
}
