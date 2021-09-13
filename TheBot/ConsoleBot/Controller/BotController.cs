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
                string text = update.InlineQuery.Query;
                Console.WriteLine(text);
                List<InlineQueryResultBase> inlineQueryResultBases = new();
                // https://media.giphy.com/media/duzpaTbCUy9Vu/giphy.gif
                // CgACAgQAAxkBAAIBFGE_Q8HMVXAJDeVxCDPubl5nEFRHAALKAgACUc1NUNzbx5H8oMDVIAQ
                InlineQueryResultGif inlineQueryResultGif = new InlineQueryResultGif("",
                    "CgACAgQAAxkBAAIBFGE_Q8HMVXAJDeVxCDPubl5nEFRHAALKAgACUc1NUNzbx5H8oMDVIAQ", "");
                inlineQueryResultBases.Add(inlineQueryResultGif);
                AnswerInlineQuery(update.InlineQuery.Id, inlineQueryResultBases);
                // foreach (var gif in GifController.Search(text))
                // {
                //     InlineQueryResultGif inlineQueryResultGif = new InlineQueryResultGif("blahblah" , gif.FileId , "dunno");
                //     inlineQueryResultBases.Add(inlineQueryResultGif);
                // }
                // AnswerInlineQueryAsync("blahblah", inlineQueryResultBases);
            }
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