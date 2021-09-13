using System;
using System.Threading;
using System.Threading.Tasks;
using ConsoleBot.Model;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ConsoleBot
{
    public class BotController
    {
        public static IUserDatabase UserDatabase { get; set; } = new UserDatabase();
        public static GifController GifController { get; set; }
        public static TelegramBotClient Client { get; set; }
        public static async Task UpdateHandler(ITelegramBotClient botClient, Update update,
            CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message)
            {
                Message message = update.Message;
                User user = new User(message.From.Id);
                UserDatabase.AddNewUserIfDoesNotExist(user);
                user = UserDatabase.GetUserById(user.UserId);
                Console.WriteLine(user.MessageProcessor.GetType());
                try
                {
                    user.MessageProcessor.Process(message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                
            }
            else if (update.Type == UpdateType.InlineQuery)
            {
                
            }
        }
        
        public static async Task ErrorHandler(ITelegramBotClient botClient, Exception exception,
            CancellationToken cancellationToken)
        {
            Console.WriteLine(exception);
            
        }

        public static async Task SendTextMessage(long chatId , string text)
        {
            await Client.SendTextMessageAsync(chatId, text);
        }

    }
}