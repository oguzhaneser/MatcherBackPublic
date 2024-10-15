using System;
using System.Collections.Generic;

namespace Matcher.DATA.Models
{
    public partial class MatchOption
    {
        public MatchOption()
        {
            Options = new HashSet<Option>();
            UserMatchOptions = new HashSet<UserMatchOption>();
        }

        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Option> Options { get; set; }
        public virtual ICollection<UserMatchOption> UserMatchOptions { get; set; }
    }
}
