namespace InTouchApi.Application.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string Message, string LoggerMessage) : base(Message)
        {
            this.LoggerMessage = LoggerMessage;
        }

        public string LoggerMessage { get; }
    }
}
