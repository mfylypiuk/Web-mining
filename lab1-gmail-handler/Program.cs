using lab1_gmail_handler.Models;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace lab1_gmail_handler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            GmailHandler gmailHandler = new GmailHandler();
            gmailHandler.Authenticate("web.minning2017@gmail.com", "aq1212qa");

            Console.WriteLine("Please wait for the emails to be loaded...");
            List<MimeMessage> emails = gmailHandler.GetAllEmails();
            MailRepository mailRepository = new MailRepository(emails);
            Console.WriteLine("Emails loaded!");

            // Створити csv-файл з переліком інформації про відправників листів: назва відправника, електронна адреса відправника
            CsvHandler sendersInfo = new CsvHandler(';', new List<string>() { "From_Name", "From_Email" });
            var infoAboutSenders = mailRepository.GetInfoAboutSenders();

            foreach (var row in infoAboutSenders)
            {
                sendersInfo.AddRow(new List<string>() { row.FromName, row.FromAddress });
            }

            sendersInfo.SaveToFile("output\\1.csv");

            // Створити csv-файл з журналом повідомлень: дата відправлення, час відправлення, тема листа, відправник
            CsvHandler dateTimesSubjectsAndSendersInfo = new CsvHandler(';', new List<string>() { "DateTime", "Subject", "Sender" });
            var infoAboutDateTimesSubjectsAndSenders = mailRepository.GetInfoAboutDateTimesSubjectsAndSenders();

            foreach (var row in infoAboutDateTimesSubjectsAndSenders)
            {
                dateTimesSubjectsAndSendersInfo.AddRow(new List<string>() { row.DateTime, row.Subject, row.Sender });
            }

            dateTimesSubjectsAndSendersInfo.SaveToFile("output\\2.csv");

            // Створити текстовий файл у якому розмістити тексти всіх повідомлень з вказанням відправника
            StringBuilder fileContent = new StringBuilder();
            var emailsSendersAndContent = mailRepository.GetEmailsSendersAndContent();

            foreach (var row in emailsSendersAndContent)
            {
                fileContent.Append(row.Sender);
                fileContent.AppendLine();
                fileContent.Append(row.Body);
                fileContent.AppendLine();
                fileContent.AppendLine();
            }

            File.WriteAllText("output\\3.csv", fileContent.ToString());

            // Визначити найбільш активного дописувача для вказаної поштової скриньки
            Console.WriteLine("The most active sender is " + mailRepository.GetTheMostActiveSender());
        }
    }
}
