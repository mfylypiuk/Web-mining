using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using lab2_email_sender_console.Models;
using lab2_html_mailto_parser.Handlers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;

namespace lab2_html_mailto_parser
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://belgium.mfa.gov.ua/
            var webPageHandler = new WebPageHandler("https://belgium.mfa.gov.ua/");

            Console.WriteLine($"Hello! I`m starting work on {webPageHandler.Url} page.");

            var emailAddresses = webPageHandler.GetHrefMailToAddresses();

            var addressesCsvFile = new CsvFile(';');
            addressesCsvFile.Headers.Add("Email");

            foreach (string emailAddress in emailAddresses)
            {
                addressesCsvFile.Rows.Add(new List<string>() { emailAddress });
            }

            if (!Directory.Exists("output"))
            {
                Directory.CreateDirectory("output");
            }

            addressesCsvFile.SaveToFile(@"output\emails.csv");

            Console.WriteLine("Well, copy emails.csv file from output directory to input directory of lab2-email-sender-console project and run it.");
        }
    }
}
