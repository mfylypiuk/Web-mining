using lab2_email_sender_console.Handlers;
using lab2_email_sender_console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace lab2_email_sender_console
{
    class Program
    {
        static void Main(string[] args)
        {
            SmtpHandler smtpHandler = new SmtpHandler("smtp.gmail.com", 587, "web.minning2017@gmail.com", "aq1212qa", "MF", true);
            MailHandler mailHandler = new MailHandler(smtpHandler);

            Console.Write("Select mode of mail subject and body selecting (type/file): ");

            if (Console.ReadLine() == "type")
            {
                Console.Write("Enter a mail subject: ");
                mailHandler.Subject = Console.ReadLine();
                Console.Write("Enter a mail body: ");
                mailHandler.Body = Console.ReadLine();
                Console.Write("Enter path to attachment file (or skip this step): ");
                string pathToAttachmentFile = Console.ReadLine();

                if (!string.IsNullOrEmpty(pathToAttachmentFile))
                {
                    mailHandler.PathToAttachmentFile = pathToAttachmentFile;
                }
            }
            else
            {
                CsvFile subjectAndBodyFile = new CsvFile(';');
                
                if (!subjectAndBodyFile.ReadFile(@"input\mail-subject-and-body.csv", out string r2))
                {
                    Console.WriteLine(r2);
                    return;
                }

                mailHandler.Subject = subjectAndBodyFile.Rows.First()[0];
                mailHandler.Body = subjectAndBodyFile.Rows.First()[1];
                mailHandler.PathToAttachmentFile = subjectAndBodyFile.Rows.First()[2];
            }

            Console.WriteLine("Emails sending...");
            mailHandler.LoadEmailsFromCsvFile(@"input\emails.csv");
            mailHandler.GenerateMailRepository();
            mailHandler.StartSendingProcess(false);
            Console.WriteLine("Okay, I`m done!");
        }
    }
}
