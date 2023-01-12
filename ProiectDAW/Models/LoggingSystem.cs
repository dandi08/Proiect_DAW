using System.Security.Cryptography;
using System.Text;
using ProiectDAW.ContextModels;

namespace ProiectDAW.Models
{
    public sealed class LoggingSystem
    {
        private static LoggingSystem instance;
        private static readonly object _lock = new object();
        public Accounts Account { get; set; }
        private LoggingSystem()
        {
        }

        public static LoggingSystem GetInstance()
        {
            lock (_lock)
            {
                if (instance == null)
                    instance = new LoggingSystem();

                return instance;
            }
        }

        public Accounts LogIn(Accounts account, List<Accounts> accountsList)
        {
            return accountsList.FirstOrDefault(a => a.Username == account.Username && a.Password == account.Password);
        }

        public Accounts SearchUsername(Accounts account, List<Accounts> accountsList)
        {
            return accountsList.FirstOrDefault(a => a.Username == account.Username);
        }

        public void AddAccount(Accounts account)
        {
            var hashedPassword = HashPassword(account.Password);
            account.Password = hashedPassword;
        }

        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
