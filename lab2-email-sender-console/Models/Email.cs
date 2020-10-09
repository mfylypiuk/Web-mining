using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace lab2_email_sender_console.Models
{
    class Email
    {
        public MailMessage MailMessage { get; }
        public DateTime SendingTime { get; }
        public bool Sended { get; set; }

        public Email(MailMessage mailMessage, DateTime sendingTime)
        {
            MailMessage = mailMessage;
            SendingTime = sendingTime;
        }
    }
}
