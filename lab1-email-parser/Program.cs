using lab1_email_parser.Models;
using OpenPop.Mime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace lab1_email_parser
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] fileContent = File.ReadAllBytes("Emails\\email1.txt");
            Message message = new Message(fileContent);
            Member from = new Member(message.Headers.From.DisplayName, message.Headers.From.Address);

            Email email = new Email
            {
                Date = message.Headers.Date,
                Subject = message.Headers.Subject,
                From = from,
                To = message.Headers.To.First().Address,
                Content = System.Text.Encoding.UTF8.GetString(message.MessagePart.Body)
            };

            List<string> headers1 = new List<string>() { "Date", "Subject" };
            List<string> row1 = new List<string>() { email.Date.ToString(), email.Subject };
            CsvHandler file1 = new CsvHandler(';', headers1, new List<List<string>>() { row1 });
            file1.SaveToFile("output\\1.csv");

            List<string> headers2 = new List<string>() { "From_Name", "From_Email", "To_Email" };
            List<string> row2 = new List<string>() {  email.From.Name, email.From.Email, email.To };
            CsvHandler file2 = new CsvHandler(';', headers2, new List<List<string>>() { row2 });
            file2.SaveToFile("output\\2.csv");

            List<string> headers3 = new List<string>() { "Content" };
            List<string> row3 = new List<string>() { email.Content.ToString().Replace(Environment.NewLine, "\\n") };
            CsvHandler file3 = new CsvHandler(';', headers3, new List<List<string>>() { row3 });
            file3.SaveToFile("output\\3.csv");
        }
    }
}
