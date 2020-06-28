using LetsCodeApp.Services;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace LetsCodeApp.Handlers.UpdateHandlers
{
    class NewMembers : IHandler
    {
        public UpdateType updateType => UpdateType.Message;

        public string[] textTriggers => new string[0];

        public TriggerType triggerType => TriggerType.NewMember;

        public ChatType[] ChatTypes => new ChatType[] { ChatType.Group, ChatType.Supergroup };

        public bool replyOnly => false;

        public async Task<bool> Process(ITelegramBotClient botClient, Update update, NeededItems n)
        {
            if (n.Sender.Id == botClient.BotId)
            {
                await botClient.SendTextMessageAsync(n.ChatId, $"HI there ...");

                var c = new GroupServices();

                if (!await c.CheckByTlId(n.ChatId))
                {
                    await c.Create(new Models.BaseModels.Group
                    {
                        About = "",
                        OwnerId = 0,
                        TelegramId = n.ChatId,
                        Title = update.Message.Chat.Title,
                        WelcomeMessage = ""
                    });
                }
            }
            else
            {
                if (n.Sender.IsBot)
                {
                    return true;
                }

                var c = new GroupServices();

                if (await c.CheckByTlId(n.ChatId))
                {
                    var wlc = await c.WlcmsgByTlId(n.ChatId).ConfigureAwait(false);

                    if (string.IsNullOrEmpty(wlc))
                    {
                        wlc = "Hi there {namelink}";
                    }

                    wlc = ParseWelcome(wlc, update.Message.Chat, n.Sender);

                    await botClient.SendTextMessageAsync(n.ChatId, wlc, ParseMode.Html);
                }
            }

            return true;
        }

        private string ParseWelcome(string st, Chat chat, User user)
        {
            return st
                .Replace("{id}", user.Id.ToString())
                .Replace("{name}", HtmlEncoder.Default.Encode(user.FirstName + " " + user.LastName))
                .Replace("{title}", HtmlEncoder.Default.Encode(chat.Title))
                .Replace("{namelink}", $"<a href=\"tg://user?id={user.Id}\">{HtmlEncoder.Default.Encode(user.FirstName)}</a>")
                .Replace("{username}", user.Username);
        }
    }
}
