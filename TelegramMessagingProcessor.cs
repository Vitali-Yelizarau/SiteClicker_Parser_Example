using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using static SiteClicker_Parser.Logger;
using static SiteClicker_Parser.SettingsStorage;

namespace SiteClicker_Parser
{

    /*
     * Method, which been used to get chat id
     * botClient.StartReceiving
        (
            updateHandler: HandleUpdateAsync,
            pollingErrorHandler: HandleErrorAsync
        );


        *
        *Method to send message to chat
        *SendMessageToGroup(botClient, chatId, message).Wait();
    */

    public static class TelegramMessagingProcessor
    {
        public static async Task<Task> HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is ApiRequestException apiRequestException)
            {
                await Task.Run(() => LogInfo($"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}"));
            }
            else
            {
                await Task.Run(() => LogInfo($"Unknown Error: {exception.Message}"));
            }

            return Task.CompletedTask;
        }

        public static async Task SendMessageToGroup(TelegramBotClient botClient, long chatId, string message)
        {
            await botClient.SendMessage
            (
                chatId: chatId,
                text: message
            );
        }

        public static async Task SendMessageToTelegramAsync(string message)
        {
            TelegramSettings tgSettings = new TelegramSettings(TG_SETTINGS_PATH);
            var botClient = new TelegramBotClient(tgSettings.API_Token);

            var cts = new CancellationTokenSource();
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            botClient.StartReceiving<TelegramUpdateHandler>(
                receiverOptions,
                cancellationToken: cts.Token
            );

            var me = await botClient.GetMe();
            _ = SendMessageToGroup(botClient, tgSettings.ChatId, message);
        }
    }
}
