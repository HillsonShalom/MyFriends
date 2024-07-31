using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyFriends.Data;
using MyFriends.Data.ViewModels;
using MyFriends.Models;

namespace MyFriends.Controllers
{
    public class FriendController : Controller
    {
        private AppDbContext _context;
        public FriendController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        
        public IActionResult Create()
        {
            return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Email,Phone,File ")] FriendView friendV)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning"] = "Movie didn't added, Try Again!  ";
                return View();
            }
            else
            {
                friendV.FriendWithPic();
                if (friendV.friend != null && friendV.picture != null)
                {
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            _context.Friends.Add(friendV.friend!);
                            _context.SaveChanges();
                            _context.Pictures.Add(friendV.picture!);
                            friendV.picture.FriendId = friendV.friend.Id;
                            friendV.picture.Friend = friendV.friend;
                            _context.SaveChanges();
                            friendV.friend.ProfileId = friendV.picture.Id;
                            friendV.friend.Profile = friendV.picture;
                            friendV.friend.pictures.Add(friendV.picture);
                            _context.SaveChanges();


                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["warning"] = "Movie didn't added, Try Again!  ";
                    return View();
                }
            }
        }


        // GET: Friends/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friend = await _context.Friends.FindAsync(id);
            if (friend == null)
            {
                return NotFound();
            }
            var picture = await _context.Pictures.FindAsync(friend.ProfileId);

            FriendView fv = new FriendView(friend, picture, _context);
            return View(fv);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("FirstName,LastName,Email,Phone,File,friendId,pictureId,Data")] FriendView friend)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            try
            {
                Friend newf = await _context.Friends.FindAsync(friend.friendId);
                newf.FirstName = friend.FirstName;
                newf.LastName = friend.LastName;
                newf.Email = friend.Email;
                newf.Phone = friend.Phone;
                Pictures newp = await _context.Pictures.FindAsync(friend.pictureId);
                if (friend.File != null)
                {
                    newp.Data = FriendView.ConvertToBytes(friend.File);
                    newp.Name = friend.File.FileName;
                }
                _context.Update(newf);
                _context.Update(newp);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friend = await _context.Friends.FindAsync(id);
            if (friend == null)
            {
                return NotFound();
            }

            ViewData["imageType"] = FriendView.GetImageType(
                _context.Pictures.Find(friend.ProfileId).Name);
            return View(friend);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var friend = await _context.Friends.FindAsync(id);
            var image = await _context.Pictures.FindAsync(friend.ProfileId);
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (friend != null && image != null)
                    {
                        friend.Profile = null; friend.ProfileId = null;
                        image.Friend = null; image.FriendId = null;
                        _context.SaveChanges();
                        _context.Friends.Remove(friend);
                        _context.Pictures.Remove(image);
                    }
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var friend = _context.Friends.Find(id);
            return View(friend);
        }

        [HttpPost]
        public IActionResult AddImage(int id, [Bind("")] Pictures picture)
        {
            
            return View("Details", id);
        }

        public IActionResult Index()
        {
            var friends = _context.Friends
                          .Include(f => f.Profile) // טוען את ה-Profile יחד עם ה-Friend
                          .ToList();
            return View(friends);
        }
    }
}
