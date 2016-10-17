﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Model.Entities.Concrete;

namespace Hotel.Repository.Abstract
{
    public interface IHallRepository : IPlaceRepository<Hall>
    {
    }
}