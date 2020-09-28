using ActiveUp.Net.Mail;
using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MimeKit;
using lab1_gmail_handler.Models;
using System.IO;
using MimeKit.Text;

namespace lab1_gmail_handler
{
    class MailRepository
    {
        public List<MimeMessage> Emails { get; }

        public MailRepository(List<MimeMessage> emails)
        {
            Emails = emails;
        }

        public List<CsvFormatA> GetInfoAboutSenders()
        {
            List<CsvFormatA> result = new List<CsvFormatA>();

            foreach (MimeMessage email in Emails)
            {
                CsvFormatA sender = new CsvFormatA
                {
                    FromName = email.From.Mailboxes.FirstOrDefault().Name,
                    FromAddress = email.From.Mailboxes.FirstOrDefault().Address
                };

                result.Add(sender);
            }

            return result;
        }

        public List<CsvFormatB> GetInfoAboutDateTimesSubjectsAndSenders()
        {
            List<CsvFormatB> result = new List<CsvFormatB>();

            foreach (MimeMessage email in Emails)
            {
                string senderName = email.From.Mailboxes.FirstOrDefault().Name;
                string senderAddress = email.From.Mailboxes.FirstOrDefault().Address;

                CsvFormatB info = new CsvFormatB
                {
                    DateTime = email.Date.ToString(),
                    Subject = email.Subject,
                    Sender = senderName + " <" + senderAddress + ">"
                };

                result.Add(info);
            }

            return result;
        }

        public List<ContentFile> GetEmailsSendersAndContent()
        {
            List<ContentFile> result = new List<ContentFile>();

            foreach (MimeMessage email in Emails)
            {
                string senderName = email.From.Mailboxes.FirstOrDefault().Name;
                string senderAddress = email.From.Mailboxes.FirstOrDefault().Address;
                string sender = senderName + " <" + senderAddress + ">";

                ContentFile contentFile = new ContentFile();
                contentFile.Sender = sender;
                contentFile.Body = email.GetTextBody(TextFormat.Text);

                result.Add(contentFile);
            }

            return result;
        }

        public string GetTheMostActiveSender()
        {
            string result = string.Empty;

            var theMostActiveSender = Emails.GroupBy(x => x.From).OrderByDescending(x => x.Count()).First();

            return theMostActiveSender.Key.ToString();
        }
    }
}
