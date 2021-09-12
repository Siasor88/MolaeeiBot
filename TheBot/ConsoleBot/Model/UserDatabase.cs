using System.Collections.Generic;

namespace ConsoleBot.Model
{
    public class UserDatabase : IDatabase<User>
    {
        private Dictionary<string/*userId*/, User> _onlineUsers = new();
        
        public void AddDocument(User doc)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveDocument(User doc)
        {
            throw new System.NotImplementedException();
        }

        public void Create()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<User> GetAllData()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<User> MatchAll(string[] data)
        {
            throw new System.NotImplementedException();
        }
    }
}