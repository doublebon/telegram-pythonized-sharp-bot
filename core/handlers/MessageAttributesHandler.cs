using System.Reflection;
using System.Runtime.Intrinsics.X86;
using telegram_pythonized_bot.core.attributes;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace telegram_pythonized_bot.core.handlers;

public class MessageAttributesHandler
{
    public static async Task InvokeByMessageType(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var methods = typeof(ChatHandler)
            .GetMethods(BindingFlags.Static | BindingFlags.Public)
            .Where(method =>
                method.IsDefined(typeof(MessageAttributes.CommandAttribute), true) ||
                method.IsDefined(typeof(MessageAttributes.FilterByTypeAttribute), true)
            ).ToArray();

        
        //var methods = typeof(UpdateHandler).GetMethods(BindingFlags.Static | BindingFlags.Public);
        foreach (var method in methods)
        {
            var commandAttribute = method.GetCustomAttribute<MessageAttributes.CommandAttribute>();
            if (commandAttribute != null && message is { Type: MessageType.Text } && commandAttribute.Commands.Contains(message.Text))
            {
                await (Task) method.Invoke(null, new object[] { botClient, message, message.From!, cancellationToken })!;
                break;
            }
            
            var messageTypeAttribute = method.GetCustomAttribute<MessageAttributes.FilterByTypeAttribute>();
            if (messageTypeAttribute != null && message.Type == messageTypeAttribute.Type)
            {
                await (Task) method.Invoke(null, new object[] { botClient, message, message.From!,  cancellationToken })!;
                break;
            }
        }
    }
}