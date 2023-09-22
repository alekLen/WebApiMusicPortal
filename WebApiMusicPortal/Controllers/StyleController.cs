using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiMusicPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApiMusicPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StyleController : ControllerBase
    {
        private readonly MusicPortalContext _context;

        public StyleController(MusicPortalContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Style>>> GetStyles()
        {
            return await _context.Styles.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Style>> GetStyle(int id)
        {
            var style = await _context.Styles.SingleOrDefaultAsync(m => m.Id == id);
            if (style == null)
            {
                return NotFound();
            }
            return new ObjectResult(style);
        }
        [HttpPut]
        public async Task<ActionResult<Style>> PutStyle(Style style)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_context.Styles.Any(e => e.Id == style.Id))
            {
                return NotFound();
            }

            _context.Update(style);
            await _context.SaveChangesAsync();
            return Ok(style);
        }
        [HttpPost]
        public async Task<ActionResult<Style>> PostStudent(Style style)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Styles.Add(style);
            await _context.SaveChangesAsync();

            return Ok(style);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Style>> DeleteStyle(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var style = await _context.Styles.SingleOrDefaultAsync(m => m.Id == id);
            if (style == null)
            {
                return NotFound();
            }

            _context.Styles.Remove(style);
            await _context.SaveChangesAsync();

            return Ok(style);
        }
    }
}
