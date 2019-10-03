using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRSDbfirst.Models;

namespace PRSDbfirst.Controllers {
    [Route("api/User")]
    [ApiController]
    public class UsersApiController : ControllerBase {
        private readonly PRSDbContext _context;

        public UsersApiController(PRSDbContext context) {
            _context = context;
        }
        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers() {
            return await _context.Users.ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUsers(int id) {
            var users = await _context.Users.FindAsync(id);

            if (users == null) {
                return NotFound();
            }

            return users;
        }
        // GET: api/User/username/password
        //if route starts with a /  it means sername.com/(start of route) start at root
        //need to be a post to be case sensitive or use odata
        
       

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(int id, Users users)
        {
            if (id != users.Id)
            {
                return BadRequest();
            }

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
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
        [HttpPost("login")]
        public async Task<ActionResult<Users>> GetUser(Users users) {
            var user = await _context
                .Users.Where(m => m.Username == users.Username && m.Password == users.Password)
                .FirstOrDefaultAsync();

            if (user == null) {
                return NotFound();
            }

            return user;
        }

        // POST: api/Users1
        [HttpPost]
        public async Task<ActionResult<Users>> PostUsers(Users users)
        {
            _context.Users.Add(users);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsers", new { id = users.Id }, users);
        }

        // DELETE: api/Users1/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Users>> DeleteUsers(int id)
        {
            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.Users.Remove(users);
            await _context.SaveChangesAsync();

            return users;
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
