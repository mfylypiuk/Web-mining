using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace lab2_email_sender_console.Handlers
{
    class SmtpHandler
    {
        private readonly SmtpClient smtpClient;
        private readonly NetworkCredential credentials;

        public string Host { get; }
        public int Port { get; }
        public string Email { get; }
        public string Password { get; }
        public string FromName { get; set; }
        public bool EnableSsl { get; }

        public SmtpHandler(string host, int port, string userName, string password, string fromName, bool enableSsl)
        {
            Host = host;
            Port = port;
            Email = userName;
            Password = password;
            FromName = fromName;
            EnableSsl = enableSsl;

            credentials = new NetworkCredential(userName, password);
            smtpClient = new SmtpClient(host, port);
            smtpClient.Credentials = credentials;
            smtpClient.EnableSsl = enableSsl;
        }

        public void SendMail(MailMessage mailMessage)
        {
            smtpClient.Send(mailMessage);
        }
    }
}
