using System;
using System.Collections.Generic;
using System.Text;
using TigerBot.Models;

namespace TigerBot.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        User Add(User newUser);
        User Update(User user);
        User Get(User user);
        User GetById(User user);
    }
}
