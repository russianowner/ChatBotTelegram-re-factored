using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using ChatBotTelegram_re_factored.Services;

namespace ChatBotTelegram_re_factored.Bot
{
    public class MessageHandler
    {
        private readonly ITelegramBotClient _botClient;
        private readonly TogetherService _togetherService;
        private readonly HistoryService _historyService;

        public MessageHandler(ITelegramBotClient botClient, string togetherApiKey)
        {
            _botClient = botClient;
            _togetherService = new TogetherService(togetherApiKey);
            _historyService = new HistoryService();
        }
        public async Task HandleAsync(Update update, CancellationToken token)
        {
            if (update.Type != UpdateType.Message || update.Message!.Type != MessageType.Text)
                return;
            var message = update.Message;
            long userId = message.From!.Id;
            string text = message.Text!.Trim();
            if (text == "/start")
            {
                await _botClient.SendMessage(userId, "Напиши мне что нибудь🙂", cancellationToken: token);
                return;
            }
            _historyService.UpdateHistory(userId, "user", text);
            string prompt = _historyService.BuildPrompt(userId, text);
            string? reply = await _togetherService.GetReplyAsync(prompt);
            if (!string.IsNullOrWhiteSpace(reply))
            {
                await _botClient.SendMessage(userId, reply, cancellationToken: token);
                _historyService.UpdateHistory(userId, "assistant", reply);
            }
            else
            {
                await _botClient.SendMessage(userId, "Нет ответа😔", cancellationToken: token);
            }
        }
    }
}
