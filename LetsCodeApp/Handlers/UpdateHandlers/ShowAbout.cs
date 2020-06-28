using LetsCodeApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace LetsCodeApp.Handlers.UpdateHandlers
{
    class ShowAbout : IHandler
    {
        public UpdateType updateType => UpdateType.Message;

        public string[] textTriggers => new[] {"/about" , "/welcome"};

        public TriggerType triggerType => TriggerType.Command;

        public ChatType[] ChatTypes => new ChatType[] { ChatType.Group, ChatType.Supergroup };

        public bool replyOnly => false;

        public async Task<bool> Process(ITelegramBotClient botClient, Update update, NeededItems n)
        {
            switch (n.Params[0].ToLower())
            {
                case "/about":
                    {
                        var c = new GroupServices();

                        var abt = await c.AboutmsgByTlId(n.ChatId);

                        if (string.IsNullOrEmpty(abt))
                        {
                            return false;
                        }

                        await botClient.SendTextMessageAsync(n.ChatId, abt, ParseMode.Html);

                        return true;
                    }
                case "/welcome":
                    {
                        ChatMember[] admins = await botClient.GetChatAdministratorsAsync(n.ChatId);

                        if (admins.Any(x => x.User.Id == n.Sender.Id))
                        {
                            GroupServices c = new GroupServices();

                            string wlc = await c.WlcmsgByTlId(n.ChatId);

                            if (string.IsNullOrEmpty(wlc))
                            {
                                return false;
                            }

                            await botClient.SendTextMessageAsync(n.ChatId, wlc);
                        }
                        else
                        {
                            await botClient.SendTextMessageAsync(n.ChatId, "Admin only!");
                        }

                        return true;
                    }

                default: return true;
            }
        }
    }
}
