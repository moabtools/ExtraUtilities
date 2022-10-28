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
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
