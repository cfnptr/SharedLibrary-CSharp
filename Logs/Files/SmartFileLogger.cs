using InjectorGames.SharedLibrary.Times;
using System.Net;
using System.Net.Mail;

namespace InjectorGames.SharedLibrary.Logs.Files
{
    /// <summary>
    /// Smart file logger class
    /// </summary>
    public class SmartFileLogger : FileLogger
    {
        /// <summary>
        /// Fatal error SMTP server hostname
        /// </summary>
        protected readonly string smtpHost;
        /// <summary>
        /// Fatal error SMTP server port
        /// </summary>
        protected readonly int smtpPort;
        /// <summary>
        /// Fatal error SMTP from address
        /// </summary>
        protected readonly string fromAddress;
        /// <summary>
        /// Fatal error SMTP to address
        /// </summary>
        protected readonly string toAddress;
        /// <summary>
        /// Fatal error SMTP from password
        /// </summary>
        protected readonly ICredentialsByHost smtpCredentials;

        /// <summary>
        /// Creates a new smart file stream logger class instance
        /// </summary>
        public SmartFileLogger(string smtpHost, int smtpPort, string fromAddress, string toAddress, ICredentialsByHost smtpCredentials, IClock timer, LogType level, bool writeToConsole, string logFolderPath) : base(timer, level, writeToConsole, logFolderPath)
        {
            this.smtpHost = smtpHost;
            this.smtpPort = smtpPort;
            this.fromAddress = fromAddress;
            this.toAddress = toAddress;
            this.smtpCredentials = smtpCredentials;
        }

        /// <summary>
        /// Logs a new message at fatal log level
        /// </summary>
        public override void Fatal(object message)
        {
            base.Fatal(message);

            // TODO: Use MailKit library
            var smtpClient = new SmtpClient(smtpHost, smtpPort)
            {
                EnableSsl = true,
                Credentials = smtpCredentials,
            };

            var subject = "Logged Fatal Exception";
            smtpClient.Send(fromAddress, toAddress, subject, message.ToString());
        }
    }
}
