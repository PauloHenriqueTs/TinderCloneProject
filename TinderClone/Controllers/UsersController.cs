using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TinderClone;
using TinderClone.Models;
using TinderClone.Security;

namespace TinderClone.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("login")]
        public async Task<ActionResult<string>> PostUser(UserDto model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user == null)
            {
                return NotFound(new { message = "Invalid User" });
            }
            else
            {
                bool validPassword = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);
                if (validPassword)
                {
                    var token = TokenService.GenerateToken(user);
                    user.Password = "";

                    return token;
                }
                else
                {
                    return NotFound(new { message = "Invalid User" });
                }
            }
        }

        [HttpPost("signup")]
        public async Task<ActionResult<UserDto>> Signup(UserDto model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user != null)
            {
                return NotFound(new { message = "Invalid User" });
            }

            await _context.Users.AddAsync(UserFactory.create(model));
            await _context.SaveChangesAsync();
            model.Password = "";
            return model;
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}