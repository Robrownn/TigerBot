using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TigerBot.Data;
using TigerBot.Models;

namespace TigerBot.Services
{
    public class UserService : IUserService
    {
        private TigerBotDbContext _context;

        public UserService(TigerBotDbContext context)
        {
            _context = context;
        }

        public User Add(User newUser)
        {
            _context.Users.Add(newUser);
            _context.SaveChanges();

            return newUser;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.OrderBy(u => u.UserName);
        }

        public User Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
