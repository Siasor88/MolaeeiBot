using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ConsoleBot
{
    public static class Handlers
    {
        
     
        public static async Task ReplyMessageHandler(ITelegramBotClient botClient,string content,long chatId,int messageId,CancellationToken cancellationToken)
        {
            await botClient.SendTextMessageAsync(chatId: chatId, 
                text: content,
                replyToMessageId: messageId,
                cancellationToken: cancellationToken);
        }

        public static bool IsAnimation(Message message)
        {
            return message.Animation != null;
        }
        
        /*
         * file uniqId:
         * fileId:
         * message:
         * 
         * 
         */
    }
}