namespace InTouchApi.Application.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string Message, string LoggerMessage) : base(Message)
        {
            this.LoggerMessage = LoggerMessage;
        }

        public string LoggerMessage { get; }
    }
}
