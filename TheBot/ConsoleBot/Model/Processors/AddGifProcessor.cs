using System;
using Telegram.Bot.Types;

namespace ConsoleBot.Model.Processors
{
    public class AddGifProcessor : MessageProcessor
    {
        public static readonly string EntranceMessage = "please send a gif";
        private Gif _gif;
        private string _text;
        public override void Process(Message obj)
        {
            if (_gif == null)
            {
                if (obj.Animation == null)
                {
                    BotController.SendTextMessage(obj.From.Id, "haji gif befrest in chie?? :||");
                    return;
                }
                _gif = new Gif(obj.Animation.FileUniqueId , obj.Animation.FileId , obj.From.Id);
                BotController.SendTextMessage(obj.From.Id, "hala esm gif befrest");
            }
            else
            {
                string text = obj.Text;
                _text = text;
                BotController.SendTextMessage(obj.From.Id, "merci mashti gif add shod");
                throw new NotImplementedException("bayad bere to elastic");
            }
        }
    }
}