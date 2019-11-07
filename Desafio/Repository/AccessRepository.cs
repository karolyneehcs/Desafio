using Desafio.Context;
using Desafio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.Repository
{
    public class AccessRepository : Repository<Access>
    {
        private readonly DesafioContext _context;
        public AccessRepository(DesafioContext context)
        {
            _context = context;
        }
    }
}
