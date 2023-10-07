using Microsoft.Extensions.Logging;
using telegram_pythonized_bot.core.handlers;
using Telegram.Bot;

namespace telegram_pythonized_bot.core.services.@abstract;

public class ReceiverService : ReceiverServiceBase<UpdateHandler>
{
    public ReceiverService(
        ITelegramBotClient botClient,
        UpdateHandler updateHandler,
        ILogger<ReceiverServiceBase<UpdateHandler>> logger)
        : base(botClient, updateHandler, logger)
    {
    }
}