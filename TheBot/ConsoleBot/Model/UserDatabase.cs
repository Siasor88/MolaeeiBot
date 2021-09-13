using System.Collections.Generic;
using ConsoleBot.Model.Processors;

namespace ConsoleBot.Model
{
    public class UserDatabase : IUserDatabase
    {
        private Dictionary<long/*userId*/, User> _onlineUsers = new();

        public void AddNewUser(User user)
        {
            _onlineUsers[user.UserId] = user;
        }

        public User GetUserById(long id)
        {
            return _onlineUsers[id];
        }

        public bool DoesUserExist(long id)
        {
            return _onlineUsers.ContainsKey(id);
        }

        public void AddNewUserIfDoesNotExist(User user)
        {
            if(!DoesUserExist(user.UserId))
                AddNewUser(user);
        }
    }
}