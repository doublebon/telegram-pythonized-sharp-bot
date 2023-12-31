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
    private static readonly Type[] AttrTypes = {
        typeof(MessageAttributes.CommandAttribute), 
        typeof(MessageAttributes.FilterByTypeAttribute),
        typeof(MessageAttributes.AnyAttribute)
    };
    
    private static readonly MethodInfo[] Methods = typeof(ChatHandler)
        .GetMethods(BindingFlags.Static | BindingFlags.Public)
        .Where(method => AttrTypes.Any(method.IsDefined))
        .ToArray();
    
    public static async Task InvokeByMessageType(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        foreach (var method in Methods)
        {
            var methodCustomAttribute = method.GetCustomAttributes().First(attr => AttrTypes.Contains(attr.GetType()));
            switch (methodCustomAttribute)
            {
                case MessageAttributes.CommandAttribute command when message is { Type: MessageType.Text} && command.Commands.Contains(message.Text): 
                    await (Task) method.Invoke(null, new object[] { botClient, message, message.From!, cancellationToken })!;
                    return;
                case MessageAttributes.FilterByTypeAttribute attr when attr.Type.Contains(message.Type):
                    await (Task) method.Invoke(null, new object[] { botClient, message, message.From!,  cancellationToken })!;
                    return;
                case MessageAttributes.AnyAttribute:
                    await (Task) method.Invoke(null, new object[] { botClient, message, message.From!,  cancellationToken })!;
                    return;
            }
        }
    }
}