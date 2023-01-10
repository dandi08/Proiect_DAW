using Microsoft.AspNetCore.Mvc;

namespace ProiectDAW.Models
{
    public class Accounts
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int TypeId { get; set; }
        public AccountTypes Type { get; set; }
    }
}
