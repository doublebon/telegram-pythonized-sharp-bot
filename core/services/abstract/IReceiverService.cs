namespace telegram_pythonized_bot.core.services.@abstract;

public interface IReceiverService
{
    Task ReceiveAsync(CancellationToken stoppingToken);
}