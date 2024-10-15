using System;
using System.Collections.Generic;

namespace Matcher.DATA.Models
{
    public partial class ChatRoom
    {
        public ChatRoom()
        {
            ChatMessages = new HashSet<ChatMessage>();
            UserChatRooms = new HashSet<UserChatRoom>();
        }

        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<ChatMessage> ChatMessages { get; set; }
        public virtual ICollection<UserChatRoom> UserChatRooms { get; set; }
    }
}
