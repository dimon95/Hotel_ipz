using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Model.Entities.Concrete;
using Hotel.Repository.Abstract;

namespace Hotel.Repository.Concrete
{
    public class HallRepository : PlaceRepository<Hall>, IHallRepository
    {
        public HallRepository ( HotelDbContext dbContext, DbSet<Hall> dbSet ) : base( dbContext, dbSet )
        {
        }
    }
}
