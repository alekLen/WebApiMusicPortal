using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiMusicPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApiMusicPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly MusicPortalContext _context;

        public ArtistController(MusicPortalContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artist>>> GetArtists()
        {
            return await _context.Artists.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Artist>> GetStyle(int id)
        {
            return await _context.Artists.SingleOrDefaultAsync(m => m.Id == id);
        }
        [HttpPut]
        public async Task<ActionResult<Artist>> PutArtist(Artist artist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_context.Artists.Any(e => e.Id == artist.Id))
            {
                return NotFound();
            }

            _context.Update(artist);
            await _context.SaveChangesAsync();
            return Ok(artist);
        }
        [HttpPost]
        public async Task<ActionResult<Artist>> PostArtist(Artist artist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();

            return Ok(artist);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Artist>> DeleteArtist(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var artist = await _context.Artists.SingleOrDefaultAsync(m => m.Id == id);
            if (artist == null)
            {
                return NotFound();
            }

            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();

            return Ok(artist);
        }
    }
}
