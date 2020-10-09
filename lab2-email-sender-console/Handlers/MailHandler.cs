using lab2_email_sender_console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace lab2_email_sender_console.Handlers
{
    class MailHandler
    {
        private SmtpHandler smtpHandler;
        public List<string> EmailAddresses { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public MailHandler(SmtpHandler smtpHandler)
        {
            this.smtpHandler = smtpHandler;
            EmailAddresses = new List<string>();
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
                EmailAddresses.Add(row.First());
            }
        }

        public void SendAll()
        {
            if (string.IsNullOrEmpty(Subject) || string.IsNullOrEmpty(Body))
            {
                throw new Exception("Subject and/or can not be empty");
            }

            foreach (string address in EmailAddresses)
            {
                MailAddress from = new MailAddress(smtpHandler.Email, smtpHandler.FromName);
                MailAddress to = new MailAddress(address);
                MailMessage mailMessage = new MailMessage(from, to);
                mailMessage.Subject = Subject;
                mailMessage.Body = Body;

                smtpHandler.SendMail(mailMessage);
            }
        }
    }
}
