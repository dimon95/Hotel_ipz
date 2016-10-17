﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Utils;

namespace Hotel.Model.Entities.Concrete
{
    public class Admin : Abstract.Account
    {
        protected Admin () { }

        public Admin ( Guid id, string name, string surname, string middlename,
            string email, string passwordHash, DateOfBirth dateOfBirth )
            :base(id, name, surname, middlename, email, passwordHash, dateOfBirth)
        {

        }
    }
}
