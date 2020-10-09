using lab2_email_sender_console.Models;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace lab2_email_sender_console.Repositories
{
    class MailsRepository
    {
        public List<Email> MailMessages { get; set; }

        public MailsRepository()
        {
            MailMessages = new List<Email>();
        }
    }
}
