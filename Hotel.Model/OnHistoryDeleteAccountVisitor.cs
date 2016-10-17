﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Model.Entities.Abstract;
using Hotel.Model.Entities.Concrete;

namespace Hotel.Model
{
    public class OnHistoryDeleteAccountVisitor : IAccountVisitor
    {
        public void Visit ( Account acc )
        {
            foreach ( Booking b in acc.History.Bookings )
            {
                b.BookedPlace.DeleteBookingPeriod( b.BookingPeriod );
            }
        }
    }
}