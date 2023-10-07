using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

namespace telegram_pythonized_bot.extensions;

public static class Extensions
{
    public static T GetConfiguration<T>(this IServiceProvider serviceProvider)
        where T : class
    {
        var o = serviceProvider.GetService<IOptions<T>>();
        if (o is null)
            throw new ArgumentNullException(nameof(T));

        return o.Value;
    }
}