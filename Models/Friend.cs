using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFriends.Models
{
    public class Friend
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        [NotMapped]
        public List<Pictures> pictures = new List<Pictures>();

        public int? ProfileId { get; set; }
        [ForeignKey("ProfileId")]
        public Pictures? Profile { get; set; }

    }
}
