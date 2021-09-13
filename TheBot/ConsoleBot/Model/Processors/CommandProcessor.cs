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
                User user = BotController.UserDatabase.GetUserById(userId);
                user.MessageProcessor = new AddGifProcessor();
                BotController.SendTextMessage(user.UserId , AddGifProcessor.EntranceMessage);
            }
        }
    }
}