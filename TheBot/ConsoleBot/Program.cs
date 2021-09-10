using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;

namespace ConsoleBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var botClient = new TelegramBotClient("1952790138:AAGTO8escIrN2im50ydJCF6dDVZ29PzSQTg");
            var cts = new CancellationTokenSource();
            botClient.StartReceiving(new DefaultUpdateHandler(Handlers.UpdateHandler,Handlers.ErrorHandler),cts.Token);
            while (true)
            {
                // Console.ReadLine();
            }

        }
    }
}