using System.Text;
using ChatBotTelegram_re_factored.Models;

namespace ChatBotTelegram_re_factored.Services
{
    public class HistoryService
    {
        private readonly int _maxHistory = 10;
        private readonly Dictionary<long, List<MessageHistory>> _history = new();

        public void UpdateHistory(long userId, string role, string text)
        {
            if (!_history.ContainsKey(userId))
                _history[userId] = new();
            _history[userId].Add(new MessageHistory(role, text));
            if (_history[userId].Count > _maxHistory)
                _history[userId].RemoveAt(0);
        }
        public string BuildPrompt(long userId, string newMessage)
        {
            var messages = _history.ContainsKey(userId) ? _history[userId] : new List<MessageHistory>();
            var sb = new StringBuilder();
            sb.AppendLine("Введи промт настроения");
            foreach (var msg in messages)
                sb.AppendLine(msg.Role == "user" ? $"пользователь: {msg.Text}" : $"бот: {msg.Text}");
            sb.AppendLine($"пользователь: {newMessage}");
            sb.AppendLine("бот:");
            return sb.ToString();
        }
    }
}
