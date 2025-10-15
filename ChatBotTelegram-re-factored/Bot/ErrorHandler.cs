using Telegram.Bot;
using Telegram.Bot.Exceptions;

namespace ChatBotTelegram_re_factored.Bot
{
    public class ErrorHandler
    {
        public async Task HandleAsync(ITelegramBotClient bot, Exception ex, CancellationToken token)
        {
            string errorMsg = ex switch
            {
                ApiRequestException apiEx => $"{apiEx.ErrorCode} — {apiEx.Message}",
                _ => ex.ToString()
            };
            Console.WriteLine($"⚠️ Ошибка: {errorMsg}");
            await Task.Delay(2000, token);
        }
    }
}
