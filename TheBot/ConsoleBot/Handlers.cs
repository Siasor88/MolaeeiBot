using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ConsoleBot
{
    public static class Handlers
    {
        public static Task UpdateHandler(ITelegramBotClient botClient,Update update,CancellationToken cancellationToken)
        {
            Console.WriteLine("Meow");
            if (update.Type == UpdateType.Message)
            {
                var message = update.Message;
                Console.WriteLine(message.MessageId);
            }
            return Task.CompletedTask;
        }

        public static Task ErrorHandler(ITelegramBotClient botClient, Exception exception,
            CancellationToken cancellationToken)
        {
            Console.WriteLine(exception.Message);
            return Task.CompletedTask;
        }

        public static async Task ReplyMessageHandler(ITelegramBotClient botClient,string content,int chatId,int messageId,CancellationToken cancellationToken)
        {
            await botClient.SendTextMessageAsync(chatId: chatId, 
                text: content,
                replyToMessageId: messageId,
                cancellationToken: cancellationToken);
        }
    }
}