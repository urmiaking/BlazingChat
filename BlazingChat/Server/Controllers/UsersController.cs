#nullable disable
using BlazingChat.Server.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazingChat.Shared.Models;

namespace BlazingChat.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly BlazingChatContext _context;

        public UsersController(BlazingChatContext context) { _context = context; }

        
        [HttpGet] // GET: api/Users
        public async Task<ActionResult<IEnumerable<User>>> GetUsers() => await _context.Users.ToListAsync();
        
        [HttpGet($"{nameof(GetProfile)}/{{userId}}")] // GET: api/Users/GetProfile/5
        public async Task<User> GetProfile(long userId) => await _context.Users.FindAsync(userId);

        [HttpPut($"{nameof(UpdateProfile)}/{{userId}}")] // PUT: api/Users/UpdateProfile/5
        public async Task<User> UpdateProfile(long userId, [FromBody] User user)
        {
            if (!userId.Equals(user.UserId)) return null;

            _context.Users.Update(user);

            await _context.SaveChangesAsync();

            return await Task.FromResult(user);
        }

        [HttpGet(nameof(UpdateTheme))] // GET: api/Users/UpdateTheme?userid=5&value=True
        public async Task<User> UpdateTheme(string userId, string value)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == Convert.ToInt32(userId));
            user.DarkTheme = value == "True" ? 1 : 0;

            await _context.SaveChangesAsync();

            return await Task.FromResult(user);
        }

        [HttpGet(nameof(UpdateNotifications))] // GET: api/Users/UpdateNotifications?userid=5&value=True
        public async Task<User> UpdateNotifications(string userId, string value)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == Convert.ToInt32(userId));
            user.Notifications = value == "True" ? 1 : 0;

            await _context.SaveChangesAsync();

            return await Task.FromResult(user);
        }

        [HttpGet(nameof(GetContacts))]
        public List<User> GetContacts()
        {
            return _context.Users.ToList();
        }
    }
}
