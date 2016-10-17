﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Model.Entities.Concrete;

namespace Hotel.Repository.Configurations
{
    public class RoomConfiguration : BasicConfiguration<Room>
    {
        public RoomConfiguration ()
        {
            Property( p => p.Description ).IsRequired();
            HasMany( p => p.Bookings );
        }
    }
}
