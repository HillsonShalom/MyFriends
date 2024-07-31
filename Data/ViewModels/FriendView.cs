using MyFriends.Models;
using System.ComponentModel.DataAnnotations;

namespace MyFriends.Data.ViewModels
{
    public class FriendView
    {
        private AppDbContext _context;

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must be between 3 - 50")]
        public string FirstName { get; set; } = null!;
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must be between 3 - 50")]
        public string LastName { get; set; }
        
        public string? Email { get; set; }
        public string? Phone { get; set; }

        public IFormFile? File { get; set; }

        public byte[]? Data { get; set; }
        public int? friendId { get; set; }
        public Friend? friend {  get; set; }
        public int? pictureId { get; set; }
        public Pictures? picture { get; set; }

        
        public FriendView()
        {
        }

        public FriendView(Friend friend, Pictures picture, AppDbContext dbContext)
        {
            _context = dbContext;
            this.friend = friend;
            this.picture = picture;
            FirstName = friend.FirstName;
            LastName = friend.LastName;
            Email = friend.Email;
            Phone = friend.Phone;
            Data = picture.Data;
            friendId = friend.Id;
            pictureId = picture.Id;
        }
        
        public void FriendWithPic()
        {
            Friend friend = new Friend();
            friend.FirstName = FirstName;
            friend.LastName = LastName;
            friend.Email = Email;
            friend.Phone = Phone;

            Pictures pic = new Pictures();
            pic.Data = ConvertToBytes(File);
            pic.Name = File.FileName;

            this.friend = friend;
            this.picture = pic;
        }
        
        public static byte[] ConvertToBytes(IFormFile file)
        {
            MemoryStream stream = new MemoryStream();
            file.CopyTo(stream);
            return stream.ToArray();
        }

        public static string GetImageType(string name)
        {
            string[] splitedName = name.Split('.');
            return splitedName[splitedName.Length - 1];
        }

        public string GetImageType()
        {
            string name = picture.Name;
            string[] splitedName = name.Split('.');
            return splitedName[splitedName.Length - 1];
        }
    }
}
