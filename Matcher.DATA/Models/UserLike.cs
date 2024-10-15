using System;
using System.Collections.Generic;

namespace Matcher.DATA.Models
{
    public partial class UserLike
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int LikedUserId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual User LikedUser { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
