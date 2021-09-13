using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;


namespace ConsoleBot
{
    class Program
    {
        private static readonly string BOT_KEY = "1952790138:AAGTO8escIrN2im50ydJCF6dDVZ29PzSQTg";
        
        static async Task Main(string[] args)
        {
            await InitBot();
            Wait();
        }

        private static void Wait()
        {
            while (true)
            {
                Console.ReadLine();
            }
        }

        private static async Task InitBot()
        {
            TelegramBotClient client = new TelegramBotClient(BOT_KEY);
            var me = await client.GetMeAsync();
            Console.WriteLine($"successfully connected to {me.Username}");
            BotController.Client = client;
            var cts = new CancellationTokenSource();

            DefaultUpdateHandler defaultUpdateHandler =
                new DefaultUpdateHandler(BotController.UpdateHandler, BotController.ErrorHandler);
            client.StartReceiving(defaultUpdateHandler, cts.Token);
            ManageCommands(client);
        }

        private static void ManageCommands(TelegramBotClient client)
        {

            List<BotCommand> bots = new List<BotCommand>();
            bots.Add(ManageAddGifCommand(client));
            client.SetMyCommandsAsync(bots);
        }

        private static BotCommand ManageAddGifCommand(TelegramBotClient client)
        {
            BotCommand botCommand = new BotCommand()
            {
                Command = "addgif",
                Description = "use this for adding a gif to the butt"
            };
            return botCommand;
        }
    }
}