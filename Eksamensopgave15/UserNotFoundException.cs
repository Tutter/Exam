﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave15
{
    class UserNotFoundException : Exception
    {
        public string username;
        public UserNotFoundException(string username)
        {

            this.username = username;
        }
    }
}
