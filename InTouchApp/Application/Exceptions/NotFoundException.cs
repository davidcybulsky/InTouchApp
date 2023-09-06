namespace InTouchApi.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string Message, string LoggerMessage) : base(Message)
        {
            this.LoggerMessage = LoggerMessage;
        }

        public string LoggerMessage { get; }
    }
}
