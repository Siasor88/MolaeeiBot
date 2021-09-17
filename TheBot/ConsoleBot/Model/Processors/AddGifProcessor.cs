
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using static ConsoleBot.BotController;

namespace ConsoleBot.Model.Processors
{
    public class AddGifProcessor : MessageProcessor
    {
        public static readonly string EntranceMessage = "please send a gif";
        public static readonly string GifAddedMessage = "merc mashti gif add shod";
        public static readonly string SendGifRequestMessage = "hala esm gif befrest";
        public static readonly string YouShouldSendGifErrorMessage = "haji gif befrest in chie?? :||";
        public static readonly string YouShouldSendTextErrorMessage = "haji ein adam ye matn befrest; in chie?? :||";
        private Gif _queuedGif = null;

        public override void Process(Message obj)
        {
            if (IsThereAnyGif())
            {
                AddGifToQueue(obj);
            }
            else
            {
                HandleGifData(obj);
            }
        }

        private bool IsThereAnyGif()
        {
            return _queuedGif == null;
        }

        private void AddGifToQueue(Message message)
        {
            if (message.Animation == null)
            {
                Respond(message,YouShouldSendGifErrorMessage);
                return;
            }
            _queuedGif = new Gif(message.Animation.FileUniqueId, message.Animation.FileId, message.From.Id);
            Respond(message,SendGifRequestMessage);
        }

        private void HandleGifData(Message message)
        {
            if (message.Type == MessageType.Text)
            {
                AddGifToDatabase(message);
                Respond(message,GifAddedMessage);
                ChangeUsersProcessor(message);
            }
            else
            {
                Respond(message,YouShouldSendTextErrorMessage);
            }
        }

        private static void ChangeUsersProcessor(Message message)
        {
            User user = BotController.UserDatabase.GetUserById(message.From.Id);
            user.MessageProcessor = new CommandProcessor();
        }

        private void AddGifToDatabase(Message message)
        {
            string text = message.Text;
            _queuedGif.Data = text;
            BotController.GifController.Add(_queuedGif);
        }

        private static async void Respond(Message message,string text)
        {
            await SendTextMessage(chatId: message.From.Id, text: text,replyMessageId: message.MessageId);
        }
    }
}