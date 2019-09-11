using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRSDbfirst.Models;

namespace PRSDbfirst.Controllers
{
    [Route("api/Request")]
    [ApiController]
    public class RequestsApiController : ControllerBase
    {
        private readonly PRSDbContext _context;

        public RequestsApiController(PRSDbContext context)
        {
            _context = context;
        }

        // GET: api/RequestsApi 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Requests>>> GetRequests()
        {
            return await _context.Requests.ToListAsync();
        }
        // GET: api/GetRequestsForReview
        [Route("/api/GetRequestsForReview")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Requests>>> GetRequestsForReview()
        {

            return await _context.Requests.Where(m=>m.Status == "Review").ToListAsync();
        }

        // GET: api/RequestsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Requests>> GetRequests(int id)
        {
            var requests = await _context.Requests.FindAsync(id);

            if (requests == null)
            {
                return NotFound();
            }

            return requests;
        }
        
        // GET: api/SetStatusReview
        //a request that we are going to do an update with
        [Route("/api/SetStatusReview/{id}")]
        [HttpGet]
        
        public async Task<ActionResult<Requests>> SetStatusReview(int id)
        {
            var requests = await _context.Requests.FindAsync(id);

            if (requests == null)
            {
                return NotFound();
            }
            requests.Status = "Review";
            await _context.SaveChangesAsync();
            return Ok();
        }
        [Route("/api/SetStatusreject/{id}")]
        [HttpGet]

        public async Task<ActionResult<Requests>> SetStatusReject(int id) {
            var requests = await _context.Requests.FindAsync(id);

            if (requests == null) {
                return NotFound();
            }
            requests.Status = "Reject";
            await _context.SaveChangesAsync();
            return Ok();
        }
        [Route("/api/SetStatusApproved/{id}")]
        [HttpGet]

        public async Task<ActionResult<Requests>> SetStatusApproved(int id) {
            var requests = await _context.Requests.FindAsync(id);

            if (requests == null) {
                return NotFound();
            }
            requests.Status = "Approved";
            await _context.SaveChangesAsync();
            return Ok();
        }
        // PUT: api/RequestsApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequests(int id, Requests requests)
        {
            if (id != requests.Id)
            {
                return BadRequest();
            }

            _context.Entry(requests).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestsExists(id))
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

        // POST: api/RequestsApi
        [HttpPost]
        public async Task<ActionResult<Requests>> PostRequests(Requests requests)
        {
            _context.Requests.Add(requests);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequests", new { id = requests.Id }, requests);
        }

        // DELETE: api/RequestsApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Requests>> DeleteRequests(int id)
        {
            var requests = await _context.Requests.FindAsync(id);
            if (requests == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(requests);
            await _context.SaveChangesAsync();

            return requests;
        }

        private bool RequestsExists(int id)
        {
            return _context.Requests.Any(e => e.Id == id);
        }
    }
}
