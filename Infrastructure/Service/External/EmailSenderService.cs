using Application.Interfaces.External;
using MimeKit;
using MimeKit.Text;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;


namespace Infrastructure.Service.External
{
    public class EmailSenderService : IEmailSenderService
    {
        public async Task<bool> SendMesage(string name ,string email, string subject, string body)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Kapo Social Media", "abdulkabirfagbohun@gmail.com"));
                message.To.Add(new MailboxAddress(name,email));
                message.Subject = subject;

                message.Body = new TextPart(TextFormat.Plain)
                {
                    Text = body
                };

                using var smtpClient = new SmtpClient();
                await smtpClient.ConnectAsync("smtp.gmail.com", 465, MailKit.Security.SecureSocketOptions.SslOnConnect); // or use 587 with StartTls
                await smtpClient.AuthenticateAsync("abdulkabirfagbohun@gmail.com", "mtnodgickbueunaa");
                await smtpClient.SendAsync(message);
                await smtpClient.DisconnectAsync(true);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}