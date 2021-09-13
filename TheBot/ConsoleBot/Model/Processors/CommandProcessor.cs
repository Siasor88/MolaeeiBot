using Telegram.Bot.Types;

namespace ConsoleBot.Model.Processors
{
    public class CommandProcessor : MessageProcessor
    {
        public override void Process(Message obj)
        {
            string text = obj.Text;
            long userId = obj.From.Id;
            if (text == "/addgif")
            {
                AddGifCommand(userId);
            }
        }

        private static void AddGifCommand(long userId)
        {
            User user = BotController.UserDatabase.GetUserById(userId);
            user.MessageProcessor = new AddGifProcessor();
            BotController.SendTextMessage(user.UserId, AddGifProcessor.EntranceMessage);
        }
    }
}