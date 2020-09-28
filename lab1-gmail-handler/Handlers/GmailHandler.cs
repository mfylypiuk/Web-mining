using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace lab1_gmail_handler
{
    class GmailHandler
    {
        private string login;
        private string password;
        private ImapClient client;

        public void Authenticate(string login, string password)
        {
            this.login = login;
            this.password = password;

            client = new ImapClient();
            client.Connect("imap.gmail.com", 993, true);
            client.Authenticate(login, password);
        }

        public List<MimeMessage> GetAllEmails()
        {
            List<MimeMessage> result = new List<MimeMessage>();

            client.Inbox.Open(FolderAccess.ReadOnly);
            IList<UniqueId> uids = client.Inbox.Search(SearchQuery.All);

            foreach (UniqueId uid in uids)
            {
                MimeMessage message = client.Inbox.GetMessage(uid);
                result.Add(message);
            }

            return result;
        }

        public void Disconnect()
        {
            client.Disconnect(true);
        }
    }
}
