using telegram_pythonized_bot.core.attributes;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineQueryResults;

namespace telegram_pythonized_bot.chat;

public class ChatInline
{
    [InlineAttributes.Any]
    public static async Task ProcessInline(ITelegramBotClient bot, InlineQuery inline, User user, CancellationToken cancellationToken)
    {
        InlineQueryResult[] results = {
            // displayed result
            new InlineQueryResultArticle(
                id: "1",
                title: "Первый",
                inputMessageContent: new InputTextMessageContent("даров")),
            new InlineQueryResultArticle(
                id: "2",
                title: "Ебать",
                inputMessageContent: new InputTextMessageContent("заебал")),
        };
        
        await bot.AnswerInlineQueryAsync(
            inline.Id,
            results: results,
            isPersonal: false,
            cacheTime: 0, 
            cancellationToken: cancellationToken);
    }
}