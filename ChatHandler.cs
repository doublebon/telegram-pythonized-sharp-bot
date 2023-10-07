using telegram_pythonized_bot.core.attributes;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace telegram_pythonized_bot;

public class ChatHandler
{
    [MessageAttributes.Command("/kek", "/lal")]
    public static async Task ProcessCommand(ITelegramBotClient bot, Message message, User user, CancellationToken cancellationToken)
    {
        // Отправляем ответное сообщение пользователю с тем же текстом
        await bot.SendTextMessageAsync(
            chatId: message.Chat,
            text: $"Ты выслал команду: {message.Text} и тебя зовут: {user.Username}",
            cancellationToken: cancellationToken
        );
    }
    
    [MessageAttributes.FilterByType(MessageType.Text)]
    public static async Task ProcessText(ITelegramBotClient bot, Message message, User user, CancellationToken cancellationToken)
    {
        // Отправляем ответное сообщение пользователю с тем же текстом
        await bot.SendTextMessageAsync(
            chatId: message.Chat,
            text: "Это твое текстовое сообщение: " + message.Text+" "+user.Username,
            cancellationToken: cancellationToken
        );
    }
    
    [MessageAttributes.FilterByType(MessageType.Audio, MessageType.Document)]
    public static async Task ProcessAudio(ITelegramBotClient bot, Message message, User user, CancellationToken cancellationToken)
    {
        // Отправляем ответное сообщение пользователю с тем же текстом
        await bot.SendTextMessageAsync(
            chatId: message.Chat,
            text: $"Ты выслал аудио: {message.Text} и тебя зовут: {user.Username}",
            cancellationToken: cancellationToken
        );
    }
    
    [MessageAttributes.FilterByType(MessageType.Photo)]
    public static async Task ProcessPhoto(ITelegramBotClient bot, Message message, User user, CancellationToken cancellationToken)
    {
        // Отправляем ответное сообщение пользователю с тем же текстом
        await bot.SendPhotoAsync(
            chatId: message.Chat,
            replyToMessageId: message.MessageId,
            caption: "Здарова, заебал",
            photo: InputFile.FromFileId(message.Photo.First().FileId),
            cancellationToken: cancellationToken);
    }
}