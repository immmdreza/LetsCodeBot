using System.Text.Encodings.Web;
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
            await botClient.SendTextMessageAsync(n.ChatId,
                $"{"!".ToHyperLink("https://github.com/immmdreza/LetsCodeBot")} سلام {HtmlEncoder.Default.Encode(n.Sender.FirstName)}، من یک ربات تلگرام هستم که برای اهداف آموزشی ساخته شده."
                + $"\nبنابراین اگر با پلتفرم {"GitHub".ToHyperLink("https://github.com/")} آشنا باشی، می تونی سورس و کدهای تشکیل دهنده منو اونجا ببینی."
                + $"\n\n{"سورس کد من در گیت هاب".ToHyperLink("https://github.com/immmdreza/LetsCodeBot")}"
                + $"\n\nاگر مایل به بحث درباره ربات های تلگرام، برنامه نویسی آنها و بطور کلی برنامه نویسی هستی؛ می توانی در گروه {"Let's C#de".ToHyperLink("https://t.me/joinchat/NlFgP0nOX1mdM6FeA8grcQ")} به ما بپیوندی."
                , ParseMode.Html);

            return true;
        }
    }
}

