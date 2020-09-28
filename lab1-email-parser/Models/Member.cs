using System;
using System.Collections.Generic;
using System.Text;

namespace lab1_email_parser.Models
{
    class Member
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public Member(string email)
        {
            Email = email;
        }

        public Member(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}
