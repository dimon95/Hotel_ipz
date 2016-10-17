﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Model.Entities.Concrete;
using Hotel.Dto;

namespace Hotel.Services
{
    static class ModelBuilder
    {
        public static BookingPeriod BuildPeriod ( this PeriodDto pd )
        {
            return new BookingPeriod( Guid.NewGuid(),
                                      new Utils.BookingDate( pd.BeginDay, pd.BeginMonth, pd.BeginYear ),
                                      new Utils.BookingDate( pd.EndDay, pd.EndMonth, pd.EndYaer ) );
        } 
    }
}
