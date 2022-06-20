﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppers.Utility
{
    public class SmtpConfiguration
    {
        public string Server { get; set; }
        public string Username { get; set; }
        public string Password { get;set; }
        public int Port { get; set; }
        public bool UseSSL { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
    }
}
