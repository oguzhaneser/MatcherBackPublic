using System;
using System.Collections.Generic;

namespace Matcher.DATA.Models
{
    public partial class User
    {
        public User()
        {
            ChatMessages = new HashSet<ChatMessage>();
            UserChatRooms = new HashSet<UserChatRoom>();
            UserLikeLikedUsers = new HashSet<UserLike>();
            UserLikeUsers = new HashSet<UserLike>();
            UserMatchOptions = new HashSet<UserMatchOption>();
            UserOptions = new HashSet<UserOption>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public short Gender { get; set; }

        public virtual ICollection<ChatMessage> ChatMessages { get; set; }
        public virtual ICollection<UserChatRoom> UserChatRooms { get; set; }
        public virtual ICollection<UserLike> UserLikeLikedUsers { get; set; }
        public virtual ICollection<UserLike> UserLikeUsers { get; set; }
        public virtual ICollection<UserMatchOption> UserMatchOptions { get; set; }
        public virtual ICollection<UserOption> UserOptions { get; set; }
    }
}
