using ConsoleBot.Model.Processors;

namespace ConsoleBot.Model
{
    public interface IUserDatabase
    {
        public void AddNewUser(User user);
        public User GetUserById(long id);

        public bool DoesUserExist(long id);

        public void AddNewUserIfDoesNotExist(User user);

    }
}