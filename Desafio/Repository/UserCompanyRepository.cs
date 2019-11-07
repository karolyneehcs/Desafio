﻿using Desafio.Context;
using Desafio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.Repository
{
    public class UserCompanyRepository : Repository<UserCompany>
    {
        public UserCompanyRepository(DesafioContext context) : base(context)
        {

        }
    }
}
