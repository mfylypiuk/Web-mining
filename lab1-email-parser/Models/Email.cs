using System;
using System.Collections.Generic;
using System.Text;

namespace lab1_email_parser.Models
{
    class Email
    {
        public string Date { get; set; }
        public string Subject { get; set; }
        public Member From { get; set; }
        public string To { get; set; }
        public string Content { get; set; }
    }
}
