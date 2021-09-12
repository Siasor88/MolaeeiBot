using ConsoleBot.Model.Processors;
using ConsoleBot.Processors;
using Telegram.Bot.Types;

namespace ConsoleBot
{
    public class User
    {
        public User(string userId)
        {
            UserId = userId;
        }
        public string UserId { get; }
        public long LastCommandTime { get; set; }
        public MessageProcessor MessageProcessor { get; set; }
    }
}