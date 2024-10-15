using System;
using System.Collections.Generic;

namespace Matcher.DATA.Models
{
    public partial class UserChatRoom
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ChatRoomId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ChatRoom ChatRoom { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
