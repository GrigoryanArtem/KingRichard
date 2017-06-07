namespace KingRichard.Bot.Modules
{
    internal class PermissionException : BotException
    {
        public PermissionException(string message) : base(message) { }
    }
}