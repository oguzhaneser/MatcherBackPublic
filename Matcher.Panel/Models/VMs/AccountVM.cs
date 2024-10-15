using System.ComponentModel.DataAnnotations;

namespace Matcher.Panel.Models
{
    public class AccountVM
    {
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
