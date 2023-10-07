using System.Reflection;
using System.Runtime.Intrinsics.X86;
using telegram_pythonized_bot.core.attributes;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace telegram_pythonized_bot.core.handlers;

public class MessageAttributesHandler
{
    public static void InvokeByMessageFilter(ITelegramBotClient botClient, Message message)
    {
        var methods = typeof(UpdateHandler).GetMethods(BindingFlags.Static | BindingFlags.Public);
        foreach (var method in methods)
        {
            var commandAttribute = method.GetCustomAttribute<MessageAttributes.CommandAttribute>();
            var messageTypeAttribute = method.GetCustomAttribute<MessageAttributes.MessageTypeAttribute>();
            if (commandAttribute != null && message is { Type: MessageType.Text } && commandAttribute.Commands.Contains(message.Text))
            {
                method.Invoke(null, new object[] { message, botClient });
                break;
            }

            if (messageTypeAttribute != null && message.Type == messageTypeAttribute.Type)
            {
                method.Invoke(null, new object[] { message, botClient });
                break;
            }
        }
    }
}