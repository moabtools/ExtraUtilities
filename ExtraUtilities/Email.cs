using System.Net;
using System.Net.Mail;

namespace ExtraUtilities
{
    public static class Email
    {
        /// <summary>
        /// Проверяет, является ли строка валидным Email-адресом. Используется System.Net.Mail.
        /// </summary>
        /// <param name="email">Входящая строка</param>
        /// <returns>true - входящая строка является валидным Email-адресом, false - не является</returns>
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Отправляет электронное письмо
        /// </summary>
        /// <param name="from">От кого</param>
        /// <param name="to">Кому</param>
        /// <param name="subject">Тема</param>
        /// <param name="body">Текст письма</param>
        /// <param name="isBodyHtml">Является ли текст HTML-кодом</param>
        /// <param name="smtpServer">SMTP сервер</param>
        /// <param name="smtpPort">SMTP port</param>
        /// <param name="smtpLogin">SMTP логин</param>
        /// <param name="smtpPassword">SMTP пароль</param>
        /// <param name="enableSsl">Использует ли SMTP сервер SSL</param>
        public static void Send(string from, string to, string subject, string body, bool isBodyHtml, string smtpServer, int smtpPort, string smtpLogin, string smtpPassword, bool enableSsl)
        {
            try
            {
                SmtpClient client = new()
                {
                    Host = smtpServer,
                    Port = smtpPort,
                    Credentials = new NetworkCredential(smtpLogin, smtpPassword),
                    EnableSsl = enableSsl
                };

                MailMessage msg = new()
                {
                    From = new MailAddress(from),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = isBodyHtml,
                    BodyEncoding = System.Text.Encoding.UTF8
                };

                msg.To.Add(new MailAddress(to));

                client.Send(msg);
            }
            catch (System.Exception)
            {
                throw;
            }

        }
    }
}
