using Telegram.Bot;
using Telegram.Bot.Types;
using ChatBotTelegram_re_factored.Services;

namespace ChatBotTelegram_re_factored.Bot
{
    public class BotService
    {
        private readonly ITelegramBotClient _botClient;
        private readonly MessageHandler _messageHandler;
        private readonly ErrorHandler _errorHandler;

        public BotService(ITelegramBotClient botClient, string togetherApiKey)
        {
            _botClient = botClient;
            _messageHandler = new MessageHandler(_botClient, togetherApiKey);
            _errorHandler = new ErrorHandler();
        }
        public async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken token)
        {
            await _messageHandler.HandleAsync(update, token);
        }
        public async Task HandleErrorAsync(ITelegramBotClient bot, Exception ex, CancellationToken token)
        {
            await _errorHandler.HandleAsync(bot, ex, token);
        }
    }
}
