using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using ChatBotTelegram_re_factored.Bot;

namespace ChatBotTelegram_re_factored
{
    class Program
    {
        static async Task Main()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            string telegramToken = config["BotConfiguration:TelegramBotToken"] ?? throw new Exception("Не нашел");
            string togetherKey = config["BotConfiguration:TogetherApiKey"] ?? throw new Exception("Не нашел");
            var botClient = new TelegramBotClient(telegramToken);
            using var cts = new CancellationTokenSource();
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };
            var botService = new BotService(botClient, togetherKey);
            botClient.StartReceiving(
                botService.HandleUpdateAsync,
                botService.HandleErrorAsync,
                receiverOptions,
                cts.Token
            );
            var me = await botClient.GetMe();
            Console.WriteLine($"@{me.Username} запущен");
            await Task.Delay(-1, cts.Token);
        }
    }
}
