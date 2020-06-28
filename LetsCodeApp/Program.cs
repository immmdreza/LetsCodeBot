using LetsCodeApp.Handlers;
using LetsCodeApp.Handlers.UpdateHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace LetsCodeApp
{
    class Program
    {
        //It's bad, find a more Secure way to pass botToken here!
        private static ITelegramBotClient telegramBotClient = new TelegramBotClient("botToken");

        private static readonly List<IHandler> Handlers = new List<IHandler>();

        static async Task Main()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Telegram.Bot.Types.User me = await telegramBotClient.GetMeAsync();

            Console.WriteLine($"bot {me.FirstName} Online!");

            Handlers.Add(new StartMessage());
            Handlers.Add(new NewMembers());
            Handlers.Add(new SetGroupInfo());

            telegramBotClient.OnUpdate += async (sender, e) =>
            {
                try
                {
                    switch (e)
                    {
                        case { Update: { Message: { Text: { } text } message } update }:
                            {
                                IHandler handler = null;

                                IEnumerable<IHandler> handlers = Handlers
                                    .Where(x => x.updateType == UpdateType.Message)
                                    .Where(x => x.triggerType == TriggerType.Command || x.triggerType == TriggerType.Command);

                                string[] Params = text.Split(' ');

                                if (text[0] == '/')
                                {
                                    handler = handlers
                                        .Where(x => x.triggerType == TriggerType.Command)
                                        .FirstOrDefault(x => x.textTriggers.Any(x => x == Params[0].ToLower()));
                                }
                                else
                                {
                                    handler = handlers
                                        .FirstOrDefault(x => x.triggerType == TriggerType.NormalText);
                                }

                                if (handler != null)
                                {
                                    if (handler.ChatTypes.Any(x => x == message.Chat.Type))
                                    {
                                        if (handler.replyOnly)
                                        {
                                            if (message.ReplyToMessage != null)
                                            {
                                                await handler.Process(telegramBotClient, update, new NeededItems
                                                {
                                                    ChatId = message.Chat.Id,
                                                    Params = Params,
                                                    Sender = message.From
                                                });
                                            }
                                        }
                                        else
                                        {
                                            await handler.Process(telegramBotClient, update, new NeededItems
                                            {
                                                ChatId = message.Chat.Id,
                                                Params = Params,
                                                Sender = message.From
                                            });
                                        }

                                    }
                                }


                                return;
                            }

                        case { Update: { Message: { NewChatMembers: { } newChatMember } message } update }:
                            {

                                var handler = Handlers.FirstOrDefault(x => x.triggerType == TriggerType.NewMember);

                                await handler.Process(telegramBotClient, update, new NeededItems
                                {
                                    ChatId = message.Chat.Id,
                                    Sender = message.NewChatMembers[^1]
                                });

                                return;
                            }

                        default: return;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            };

            telegramBotClient.StartReceiving();

            Console.ReadLine();
            telegramBotClient.StopReceiving();
        }
    }
}
