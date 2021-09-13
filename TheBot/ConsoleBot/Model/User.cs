using ConsoleBot.Model.Processors;
using ConsoleBot.Processors;
using Telegram.Bot.Types;

namespace ConsoleBot
{
    public class User
    {
        public User(long userId)
        {
            UserId = userId;
            MessageProcessor = new CommandProcessor();
        }
        public long UserId { get; }
        public long LastCommandTime { get; set; }
        public MessageProcessor MessageProcessor { get; set; }
    }
}