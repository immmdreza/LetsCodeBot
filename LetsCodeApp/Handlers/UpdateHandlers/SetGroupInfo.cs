using LetsCodeApp.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace LetsCodeApp.Handlers.UpdateHandlers
{
    class SetGroupInfo : IHandler
    {
        public UpdateType updateType => UpdateType.Message;

        public string[] textTriggers => new[] { "/setwelcome", "/setabout" };

        public TriggerType triggerType => TriggerType.Command;

        public ChatType[] ChatTypes => new[] { ChatType.Group, ChatType.Supergroup };

        public bool replyOnly => true;

        public async Task<bool> Process(ITelegramBotClient botClient, Update update, NeededItems n)
        {
            ChatMember[] admins = await botClient.GetChatAdministratorsAsync(n.ChatId);

            if (admins.Any(x => x.User.Id == n.Sender.Id))
            {
                GroupServices c = new GroupServices();

                switch (n.Params[0])
                {
                    case "/setwelcome":
                        {
                            if (string.IsNullOrEmpty(update.Message.ReplyToMessage.Text))
                            {
                                return true;
                            }

                            Models.BaseModels.Group chat = await c.GetByTlId(n.ChatId);

                            await c.UpdateWelcome(n.ChatId, update.Message.ReplyToMessage.Text);


                            try
                            {
                                await botClient.SendTextMessageAsync(n.ChatId, update.Message.ReplyToMessage.Text, ParseMode.Html);
                            }
                            catch(Exception ex)
                            {
                                await botClient.SendTextMessageAsync(n.ChatId, ex.Message, ParseMode.Html);
                            }
                            await botClient.SendTextMessageAsync(n.ChatId, "Done!");

                            return true;
                        }

                    case "/setabout":
                        {
                            if (string.IsNullOrEmpty(update.Message.ReplyToMessage.Text))
                            {
                                return true;
                            }

                            Models.BaseModels.Group chat = await c.GetByTlId(n.ChatId);

                            await c.UpdateAbout(n.ChatId, update.Message.ReplyToMessage.Text);

                            try
                            {
                                await botClient.SendTextMessageAsync(n.ChatId, update.Message.ReplyToMessage.Text, ParseMode.Html);
                            }
                            catch (Exception ex)
                            {
                                await botClient.SendTextMessageAsync(n.ChatId, ex.Message, ParseMode.Html);
                            }
                            await botClient.SendTextMessageAsync(n.ChatId, "Done!");

                            return true;
                        }
                    default:
                        break;
                }
            }
            else
            {
                await botClient.SendTextMessageAsync(n.ChatId, "Admin only");
            }

            return true;
        }
    }
}
