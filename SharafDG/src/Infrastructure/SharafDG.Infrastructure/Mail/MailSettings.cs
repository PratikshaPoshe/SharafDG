﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SharafDG.Infrastructure.Mail
{
   public  class MailSettings
    {
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
