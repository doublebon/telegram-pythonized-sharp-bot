using Telegram.Bot.Types.Enums;

namespace telegram_pythonized_bot.core.attributes;

public abstract class MessageAttributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class MessageTypeAttribute : Attribute
    {
        // Свойство для хранения типа сообщения
        public MessageType Type { get; }

        // Конструктор для инициализации типа сообщения
        public MessageTypeAttribute(MessageType type)
        {
            Type = type;
        }
    }
    
    [AttributeUsage(AttributeTargets.Method)]
    public class CommandAttribute : Attribute
    {
        // Свойство для хранения списка команд
        public string[] Commands { get; }

        // Конструктор для инициализации списка команд
        public CommandAttribute(params string[] commands)
        {
            Commands = commands;
        }
    }
    
}