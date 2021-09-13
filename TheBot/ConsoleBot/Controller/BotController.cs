using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ConsoleBot.Model;
using Nest;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;

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
                HandleMessages(update);
            }
            else if (update.Type == UpdateType.InlineQuery)
            {
                HandleInlineQueries(update);
            }
        }

        private static void HandleMessages(Update update)
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
                Console.WriteLine(e.Message);
            }
        }

        private static void HandleInlineQueries(Update update)
        {
            string text = update.InlineQuery.Query;
            List<InlineQueryResultBase> inlineQueryResultBases = new();
            foreach (var gif in GifController.Search(text))
            {
                InlineQueryResultCachedGif inlineQueryResultGif =
                    new InlineQueryResultCachedGif(Guid.NewGuid().ToString(), gif.FileId);
                inlineQueryResultBases.Add(inlineQueryResultGif);
            }

            AnswerInlineQuery(update.InlineQuery.Id, inlineQueryResultBases);
        }

        public static async Task ErrorHandler(ITelegramBotClient botClient, Exception exception,
            CancellationToken cancellationToken)
        {
            Console.WriteLine(exception);
        }

        public static async Task SendTextMessage(long chatId, string text)
        {
            await Client.SendTextMessageAsync(chatId, text);
        }

        public static async Task AnswerInlineQuery(string inlineQueryId,
            List<InlineQueryResultBase> inlineQueryResultBases)
        {
            await Client.AnswerInlineQueryAsync(inlineQueryId, inlineQueryResultBases);
        }
    }
}