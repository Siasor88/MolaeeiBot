using ConsoleBot.Processors;
using Telegram.Bot.Types;

namespace ConsoleBot.Model.Processors
{
    public abstract class MessageProcessor :IProcessor<Message>
    {
        public abstract void Process(Message obj);
    }
}