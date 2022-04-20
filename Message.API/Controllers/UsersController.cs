using Message.API.Data;
using Message.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Message.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly MessageDbContext userDbContext;

        public UsersController(MessageDbContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }
        // get all users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await userDbContext.Users.ToListAsync();
            return Ok(users);
        }

        // get user
        [HttpGet]
        [Route("{id:int}")]
        [ActionName("GetUser")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            var user = await userDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound("User not found");
        }

        // add user
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] Users user)
        {
            await userDbContext.Users.AddAsync(user);
            await userDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }
    }
}
