namespace InTouchApi.Application.Interfaces
{
    public interface IEmailService
    {
        public Task SendEmailAsync(string sendToEmail, string subject, string body);
    }
}
