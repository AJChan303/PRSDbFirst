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
    [Route("api/RequestLine")]
    [ApiController]
    public class RequestLinesApiController : ControllerBase
    {
        private readonly PRSDbContext _context;

        public RequestLinesApiController(PRSDbContext context)
        {
            _context = context;
        }

        // GET: api/RequestLinesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestLines>>> GetRequestLines()
        {
            return await _context.RequestLines.ToListAsync();
        }

        // GET: api/RequestLinesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RequestLines>> GetRequestLines(int id)
        {
            var requestLines = await _context.RequestLines.FindAsync(id);

            if (requestLines == null)
            {
                return NotFound();
            }

            return requestLines;
        }

        // PUT: api/RequestLinesApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequestLines(int id, RequestLines requestLines)
        {
            if (id != requestLines.Id)
            {
                return BadRequest();
            }

            _context.Entry(requestLines).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            var success = RecalcuateRequestTotal(id);
            if (!success) { return this.StatusCode(500); }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestLinesExists(id))
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

        // POST: api/RequestLinesApi
        [HttpPost]
        public async Task<ActionResult<RequestLines>> PostRequestLines(RequestLines requestLines)
        {
            _context.RequestLines.Add(requestLines);
            await _context.SaveChangesAsync();

            var success = RecalcuateRequestTotal(requestLines.Id);
            if (!success) { return this.StatusCode(500); }
            return CreatedAtAction("GetRequestLines", new { id = requestLines.Id }, requestLines);
        }
        
        // DELETE: api/RequestLinesApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RequestLines>> DeleteRequestLines(int id)
        {
            var requestLines = await _context.RequestLines.FindAsync(id);
            if (requestLines == null)
            {
                return NotFound();
            }

            _context.RequestLines.Remove(requestLines);
            await _context.SaveChangesAsync();
            var success = RecalcuateRequestTotal(id);
            if (!success) { return this.StatusCode(500); }
            return requestLines;
        }

        private bool RequestLinesExists(int id)
        {
            return _context.RequestLines.Any(e => e.Id == id);
        }
        private bool RecalcuateRequestTotal(int requestId) {
            var request = _context.Requests.SingleOrDefault(r => r.Id == requestId);
            if (request == null) {
                return false;
            }
            request.Total = _context.RequestLines.Include(l => l.Product)
            .Where(l => l.RequestId == requestId)
            .Sum(l => l.Quantity * l.Product.Price);
            if (request.Status == "Review")
                request.Status = "Revised";

            _context.SaveChanges();
            return true;

        }
    }
}
