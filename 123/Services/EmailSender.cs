using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;
namespace _123.Services;
public class EmailSender
{
    private readonly string _smtpServer = "smtp.gmail.com";
    private readonly int _smtpPort = 587;
    private readonly string _username = "anhhung03102003@gmail.com"; // Địa chỉ email của bạn
    private readonly string _password = "H@hung03"; // Mật khẩu ứng dụng

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var mailMessage = new MimeMessage();
        mailMessage.From.Add(new MailboxAddress("Your Name", _username));
        mailMessage.To.Add(new MailboxAddress("", email));
        mailMessage.Subject = subject;
        mailMessage.Body = new TextPart("html") { Text = message };

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(_smtpServer, _smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_username, _password);
            await client.SendAsync(mailMessage);
            await client.DisconnectAsync(true);
        }
    }
}