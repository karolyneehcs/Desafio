using Desafio.Context;
using Desafio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.Repository
{
    public class PageRepository : Repository<Page>
    {
        public PageRepository(DesafioContext context) : base(context)
        {

        }
    }
}
