using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFriends.Models
{
    public class Pictures
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public byte[] Data { get; set; }
        public int? FriendId { get; set; }
        [ForeignKey("FriendId")]
        public Friend? Friend { get; set; }
    }
}