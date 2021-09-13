using System;
using Telegram.Bot.Types;

namespace ConsoleBot.Model.Processors
{
    public class AddGifProcessor : MessageProcessor
    {
        public static readonly string EntranceMessage = "please send a gif";
        public static readonly string GifAddedMessage = "merci mashti gif add shod";
        public static readonly string SendGifRequestMessage = "hala esm gif befrest";
        public static readonly string YouShouldSendGifErrorMessage = "haji gif befrest in chie?? :||";
        private Gif _gif;

        public override void Process(Message obj)
        {
            if (_gif == null)
            {
                UpdateGif(obj);
            }
            else
            {
                HandleGifData(obj);
            }
        }

        private void UpdateGif(Message obj)
        {
            if (obj.Animation == null)
            {
                BotController.SendTextMessage(obj.From.Id, YouShouldSendGifErrorMessage);
                return;
            }

            _gif = new Gif(obj.Animation.FileUniqueId, obj.Animation.FileId, obj.From.Id);
            BotController.SendTextMessage(obj.From.Id, SendGifRequestMessage);
        }

        private void HandleGifData(Message obj)
        {
            string text = obj.Text;
            _gif.Data = text;
            BotController.SendTextMessage(obj.From.Id, GifAddedMessage);
            User user = BotController.UserDatabase.GetUserById(obj.From.Id);
            user.MessageProcessor = new CommandProcessor();
            BotController.GifController.Add(_gif);
        }
    }
}