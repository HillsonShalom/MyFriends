using Microsoft.AspNetCore.Mvc;
using MyFriends.Data;
using MyFriends.Data.ViewModels;
using MyFriends.Models;

namespace MyFriends.Controllers
{
    public class PicturesController : Controller
    {
        AppDbContext _conntext;
        public PicturesController(AppDbContext db)
        {
            _conntext = db;
        }
        public IActionResult Index()
        {
            ViewData["list"] = _conntext.Pictures.ToList();
            return View();
        }

        public IActionResult UserPictures(int id)
        {
            ViewData["list"] = _conntext.Pictures.Where<Pictures>(item => item.FriendId == id).ToList();
            return View("Index", new FriendView());
        }

        [HttpPost]
        public async Task<IActionResult> Add(int id, [Bind("File")] FriendView? file)
        {
            if (file.File != null)
            {
                Friend friend = _conntext.Friends.Find(id);
                byte[] data = FriendView.ConvertToBytes(file.File);
                _conntext.Pictures.Add(new Pictures()
                {
                    Data = data,
                    Friend = friend,
                    FriendId = id,
                    Name = file.File.FileName
                });
                _conntext.SaveChanges();
            }
            ViewData["list"] = _conntext.Pictures.Where<Pictures>(item => item.FriendId == id).ToList();
            return View("Index");
        }
    }
}
