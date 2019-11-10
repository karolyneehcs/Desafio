using Desafio.Context;
using Desafio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.Repository
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(DesafioContext context) : base(context)
        {

        }

        private string HashPassword(string Password)
        {
            //hash function

            return Password;
        }

        public User CheckPassword(string email, string password)
        {
            return _entity.FirstOrDefault(a => a.Email == email && a.Password == HashPassword(password));
        }

        public override User Add(User user)
        {
            user.Password = HashPassword(user.Password);
            return base.Add(user);
        }
    }
}
