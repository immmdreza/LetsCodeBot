using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace LetsCodeApp.Handlers
{
    interface IHandler
    {
        UpdateType updateType { get; }

        string[] textTriggers { get; }

        TriggerType triggerType { get; }

        ChatType[] ChatTypes { get; }

        bool replyOnly { get; }

        Task<bool> Process(ITelegramBotClient botClient, Update update, NeededItems n);
    }

    public enum TriggerType
    {
        Command,
        NormalText,
        NewMember
    }

    public class NeededItems
    {
        public long ChatId { get; set; }

        public User Sender { get; set; }

        public string[] Params { get; set; }
    }
}
