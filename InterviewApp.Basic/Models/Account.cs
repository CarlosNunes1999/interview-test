using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewApp.Models
{
    public class Account
    {
        public Guid Id { get; set; } = Guid.Empty;

        public string Username { get; set; } = "";

        public string Password { get; set; } = "";

    }
}
