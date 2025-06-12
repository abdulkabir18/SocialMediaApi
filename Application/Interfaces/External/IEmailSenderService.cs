namespace Application.Interfaces.External
{
    public interface IEmailSenderService
    {
        Task<bool> SendMesage(string name, string email, string subject, string body);
    }
}
