using System;

namespace Models
{
    public class AppSettings
    {
        public Stage Stage { get; set; }
        public string Authority { get; set; }
        public SendGridSettings SendGrid { get; set; }
    }

    public class SendGridSettings
    {
        public string APIKey { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
    }
}
