using System;
using System.Collections.Generic;

namespace Matcher.DATA.Models
{
    public partial class UserMatchOption
    {
        public int UserId { get; set; }
        public int MatchOptionId { get; set; }
        public int Id { get; set; }

        public virtual MatchOption MatchOption { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
