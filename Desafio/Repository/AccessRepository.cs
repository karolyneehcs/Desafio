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
        public AccessRepository(DesafioContext context) : base(context)
        {
        }

        public Access GetByToken(string token)
        {
            return _entity.FirstOrDefault(a => a.Token == token);
        }
    }
}
