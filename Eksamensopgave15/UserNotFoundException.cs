﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave15
{
    class UserNotFoundException : Exception
    {
        public string userName;
        public UserNotFoundException(string userName)
        {

            this.userName = userName;
        }
    }
}
