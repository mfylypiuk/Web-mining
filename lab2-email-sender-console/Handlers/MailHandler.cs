using lab2_email_sender_console.Models;
using lab2_email_sender_console.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace lab2_email_sender_console.Handlers
{
    class MailHandler
    {
        private SmtpHandler smtpHandler;
        public List<(string, DateTime)> EmailData { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string PathToAttachmentFile { get; set; }
        public MailsRepository MailsRepository { get; set; }

        public MailHandler(SmtpHandler smtpHandler)
        {
            this.smtpHandler = smtpHandler;
            EmailData = new List<(string, DateTime)>();
            MailsRepository = new MailsRepository();
        }

        public void LoadEmailsFromCsvFile(string pathToFile)
        {
            CsvFile csvEmailsFile = new CsvFile(';');

            if (!csvEmailsFile.ReadFile(pathToFile, out string r1))
            {
                Console.WriteLine(r1);
                return;
            }

            foreach (List<string> row in csvEmailsFile.Rows)
            {
                DateTime sendingTime = DateTime.MinValue;

                if (!string.IsNullOrEmpty(row[1]))
                {
                    sendingTime = DateTime.Parse(row[1]);
                }
                else if (int.TryParse(row[1], out int startInSeconds) && startInSeconds > 0)
                {
                    sendingTime = DateTime.Now.AddSeconds(startInSeconds);
                }
                else
                {
                    sendingTime = DateTime.MinValue;
                }

                EmailData.Add((row[0], sendingTime));
            }
        }

        public void GenerateMailRepository()
        {
            if (string.IsNullOrEmpty(Subject) || string.IsNullOrEmpty(Body))
            {
                throw new Exception("Subject and/or can not be empty");
            }

            foreach (var emailData in EmailData)
            {
                MailAddress from = new MailAddress(smtpHandler.Email, smtpHandler.FromName);
                MailAddress to = new MailAddress(emailData.Item1);
                MailMessage mailMessage = new MailMessage(from, to);
                mailMessage.Subject = Subject;
                mailMessage.Body = Body;

                if (!string.IsNullOrEmpty(PathToAttachmentFile))
                {
                    if (!File.Exists(PathToAttachmentFile))
                    {
                        throw new Exception("Attachment file does not exist");
                    }

                    var attachment = new Attachment(PathToAttachmentFile);
                    mailMessage.Attachments.Add(attachment);
                }

                var email = new Email(mailMessage, emailData.Item2);
                MailsRepository.MailMessages.Add(email);
            }

            Console.WriteLine(new string('-', 100));

            foreach (var email in MailsRepository.MailMessages)
            {
                string sendingTimeContent = string.Empty;

                if (email.SendingTime == DateTime.MinValue)
                {
                    sendingTimeContent = "right now";
                }
                else
                {
                    sendingTimeContent = $"at {email.SendingTime}";
                }

                Console.WriteLine($"Email for {email.MailMessage.To.First().Address} will send {sendingTimeContent}");
            }

            Console.WriteLine(new string('-', 100));
        }

        public void StartSendingProcess(bool send = false)
        {
            if (MailsRepository.MailMessages.Count == 0)
            {
                throw new Exception("Mail repository can not be empty");
            }

            foreach (var email in MailsRepository.MailMessages)
            {
                if (email.SendingTime == DateTime.MinValue)
                {
                    Send(email, send);
                }
            }

            while (true)
            {
                if (!MailsRepository.MailMessages.Exists(email => email.Sended == false))
                {
                    Console.WriteLine("All emails were sent.");
                    break;
                }

                foreach (var email in MailsRepository.MailMessages)
                {
                    if (!email.Sended)
                    {
                        if (DateTime.Compare(email.SendingTime, DateTime.Now) == 0)
                        {
                            Send(email, send);
                        }
                    }
                }
            }
        }

        private void Send(Email email, bool send)
        {
            if (send)
            {
                smtpHandler.SendMail(email.MailMessage);
                email.Sended = true;

                Console.WriteLine($"Email for {email.MailMessage.To.First().Address} was sent at {DateTime.Now}");
            }
        }
    }
}
