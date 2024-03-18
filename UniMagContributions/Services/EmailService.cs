using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using UniMagContributions.Constraints;
using UniMagContributions.Models;
using UniMagContributions.Repositories.Interface;
using UniMagContributions.Services.Interface;

namespace UniMagContributions.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment environment;
        private readonly IUserRepository _userRepository;

        public EmailService(IConfiguration configuration, IWebHostEnvironment environment, IUserRepository userRepository)
        {
            _configuration = configuration;
            this.environment = environment;
            _userRepository = userRepository;
        }

        public string SendEmail(Message message)
        {
            List<User> users = _userRepository.GetAllUserByRoleAndFaculty(ERole.Coordinator.ToString(), message.Content.FacultyId);

            foreach (User user in users)
            {
                message.To = user.Email;
                var emailMessage = CreateEmailMessage(message, user.FirstName);
                Send(emailMessage);
            }

            return "Email sent successfully";
        }

        public async Task<string> SendEmailAsync(Message message)
        {
            List<User> users = _userRepository.GetAllUserByRoleAndFaculty(ERole.Coordinator.ToString(), message.Content.FacultyId);

            foreach (User user in users)
            {
                message.To = user.Email;
                var emailMessage = CreateEmailMessage(message, user.FirstName);
                await SendAsync(emailMessage);
            }

            return "Email sent successfully";
        }

        private string PopulateBody(EmailContent emailContent, string coordinatorName)
        {
            string body = string.Empty;
            string wwwPath = this.environment.WebRootPath;
            string path = Path.Combine(wwwPath, "Template", "email.html");
            using (StreamReader reader = new(path))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{coordinatorName}", coordinatorName);
            body = body.Replace("{studentName}", emailContent.StudentName);
            body = body.Replace("{contributionTitle}", emailContent.ContributionTitle);
            body = body.Replace("{submissionDate}", emailContent.SubmissionDate);

            return body;
        }

        private MimeMessage CreateEmailMessage(Message message, string coordinatorName)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(MailboxAddress.Parse(_configuration["EmailConfiguration:From"]));
            emailMessage.To.Add(MailboxAddress.Parse(message.To));
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = PopulateBody(message.Content, coordinatorName) };

            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_configuration["EmailConfiguration:SmtpServer"], int.Parse(_configuration["EmailConfiguration:Port"]), SecureSocketOptions.StartTls);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_configuration["EmailConfiguration:Username"], _configuration["EmailConfiguration:Password"]);

                    client.Send(mailMessage);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_configuration["EmailConfiguration:SmtpServer"], int.Parse(_configuration["EmailConfiguration:Port"]), SecureSocketOptions.StartTls);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_configuration["EmailConfiguration:Username"], _configuration["EmailConfiguration:Password"]);

                    await client.SendAsync(mailMessage);
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
    }
}
