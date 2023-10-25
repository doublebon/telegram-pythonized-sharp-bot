using System.Reflection;
using telegram_pythonized_bot.chat;
using telegram_pythonized_bot.core.attributes;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace telegram_pythonized_bot.core.handlers;

public class InlineAttributesHandler
{
    private static readonly Type[] AttrTypes = {
        typeof(InlineAttributes.AnyAttribute),
    };
    
    private static readonly MethodInfo[] Methods = typeof(ChatInline)
        .GetMethods(BindingFlags.Static | BindingFlags.Public)
        .Where(method => AttrTypes.Any(method.IsDefined))
        .ToArray();
    
    public static async Task InvokeByInlineType(ITelegramBotClient botClient, InlineQuery inlineQuery, CancellationToken cancellationToken)
    {
        foreach (var method in Methods)
        {
            var methodCustomAttribute = method.GetCustomAttributes().First(attr => AttrTypes.Contains(attr.GetType()));
            switch (methodCustomAttribute)
            {
                case InlineAttributes.AnyAttribute:
                    await (Task) method.Invoke(null, new object[] { botClient, inlineQuery, inlineQuery.From, cancellationToken })!;
                    return;
            }
        }
    }
}