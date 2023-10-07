using System.ComponentModel;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using telegram_pythonized_bot.core.attributes;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace telegram_pythonized_bot.core.handlers;

public static class MessageAttributesHandler
{
    public static async Task InvokeByMessageType(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var methods = typeof(ChatHandler)
            .GetMethods(BindingFlags.Static | BindingFlags.Public)
            .Where(method =>
                method.IsDefined(typeof(MessageAttributes.CommandAttribute), true) ||
                method.IsDefined(typeof(MessageAttributes.FilterByTypeAttribute), true)
            ).ToArray();
        
        foreach (var method in methods)
        {
            var methodCustomAttribute = method.GetCustomAttributes().First(attr =>
                attr is MessageAttributes.CommandAttribute or MessageAttributes.FilterByTypeAttribute);
            
            switch (methodCustomAttribute)
            {
                case MessageAttributes.CommandAttribute command when message is { Type: MessageType.Text} && command.Commands.Contains(message.Text): 
                    await (Task) method.Invoke(null, new object[] { botClient, message, message.From!, cancellationToken })!;
                    return;
                case MessageAttributes.FilterByTypeAttribute attr when attr.Type.Contains(message.Type):
                    await (Task) method.Invoke(null, new object[] { botClient, message, message.From!,  cancellationToken })!;
                    return;
            }
        }
    }
}