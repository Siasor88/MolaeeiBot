using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ConsoleBot
{
    public class BotController
    {
        public static IDatabase<User> UserDatabase { get; set; }
        public static GifController GifController { get; set; }

        public static async Task UpdateHandler(ITelegramBotClient botClient, Update update,
            CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message)
            {
                var message = update.Message;
                var id = message.Animation.FileUniqueId;
                Thread.Sleep(3000);
                await botClient.SendAnimationAsync(message.Chat.Id, animation: message.Animation.FileId,
                    replyToMessageId: message.MessageId, cancellationToken: cancellationToken);
                Console.WriteLine(id);
            }
        }
        
        public static Task ErrorHandler(ITelegramBotClient botClient, Exception exception,
            CancellationToken cancellationToken)
        {
            Console.WriteLine(exception.Message);
            return Task.CompletedTask;
        }

    }
}