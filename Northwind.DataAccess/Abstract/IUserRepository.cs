﻿using Northwind.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.DataAccess.Abstract
{
    public interface IUserRepository
    {
        User Login(User login);
        User Register(User register);
    }
}
