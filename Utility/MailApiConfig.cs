using System.Security.Permissions;

namespace paroquiaRussas.Utility
{
    public class MailApiConfig
    {
        public string MailFrom { get; set; }

        public string MailTo { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string Host { get; set; }

        public string Password { get; set; }

        public int Port { get; set; }
    }
}
