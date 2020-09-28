using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new ImapClient())
            {
                client.Connect("imap.gmail.com", 993, true);

                // If you want to disable an authentication mechanism,
                // you can do so by removing the mechanism like this:
                client.AuthenticationMechanisms.Remove("XOAUTH");

                client.Authenticate("web.minning2017@gmail.com", "aq1212qa");

                // The Inbox folder is always available...
                var inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadOnly);

                Console.WriteLine("Total messages: {0}", inbox.Count);
                Console.WriteLine("Recent messages: {0}", inbox.Recent);

                // download each message based on the message index
                for (int i = 0; i < inbox.Count; i++)
                {
                    var message = inbox.GetMessage(i);
                    Console.WriteLine("Subject: {0}", message.Subject);
                }

                //// let's try searching for some messages...
                //var query = SearchQuery.DeliveredAfter(DateTime.Parse("2013-01-12"))
                //    .And(SearchQuery.SubjectContains("MailKit"))
                //    .And(SearchQuery.Seen);

                //foreach (var uid in inbox.Search(query))
                //{
                //    var message = inbox.GetMessage(uid);
                //    Console.WriteLine("[match] {0}: {1}", uid, message.Subject);
                //}

                client.Disconnect(true);
            }
        }
    }
}
