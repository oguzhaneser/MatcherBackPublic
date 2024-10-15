using System;
using System.Collections.Generic;

namespace Matcher.DATA.Models
{
    public partial class UserOption
    {
        public int UserId { get; set; }
        public int OptionId { get; set; }
        public int Id { get; set; }

        public virtual Option? Option { get; set; } = null!;
        public virtual User? User { get; set; } = null!;
    }
}
