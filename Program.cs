using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using telegram_pythonized_bot.config;
using telegram_pythonized_bot.core.handlers;
using telegram_pythonized_bot.core.services.@abstract;
using telegram_pythonized_bot.extensions;
using Telegram.Bot;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.Configure<BotConfig>(
            context.Configuration.GetSection(BotConfig.Configuration));
        
        services.AddHttpClient("telegram_bot_client")
            .AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
            {
                var botConfig = sp.GetConfiguration<BotConfig>();
                TelegramBotClientOptions options = new(botConfig.BotToken);
                return new TelegramBotClient(options, httpClient);
            });
        services.AddScoped<UpdateHandler>();
        services.AddScoped<ReceiverService>();
        services.AddHostedService<PollingService>();
    })
    .Build();

await host.RunAsync();