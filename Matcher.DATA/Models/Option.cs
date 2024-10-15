using System;
using System.Collections.Generic;

namespace Matcher.DATA.Models
{
    public partial class Option
    {
        public Option()
        {
            UserOptions = new HashSet<UserOption>();
        }

        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public int MatchOptionId { get; set; }
        public string Name { get; set; } = null!;

        public virtual MatchOption MatchOption { get; set; } = null!;
        public virtual ICollection<UserOption> UserOptions { get; set; }
    }
}
