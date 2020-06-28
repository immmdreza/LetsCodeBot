using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace LetsCodeApp.Handlers.UpdateHandlers
{
    class StartMessage : IHandler
    {
        public ChatType[] ChatTypes => new ChatType[] { ChatType.Private };
        public bool replyOnly => false;
        UpdateType IHandler.updateType => UpdateType.Message;
        string[] IHandler.textTriggers => new[] { "/start" };
        TriggerType IHandler.triggerType => TriggerType.Command;

        public async Task<bool> Process(ITelegramBotClient botClient, Update update, NeededItems n)
        {
            await botClient.SendTextMessageAsync(n.ChatId, $"HI {n.Sender.FirstName}");

            return true;
        }
    }
}
