namespace InTouchApi.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string Message, string LoggerMessage) : base(Message)
        {
            this.LoggerMessage = LoggerMessage;
        }

        public string LoggerMessage { get; }
    }
}
