using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiMusicPortal.Models;

namespace WebApiMusicPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly MusicPortalContext _context;

        public SongController(MusicPortalContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongs()
        {
            return await _context.Songs.Include((p) => p.artist).Include((p) => p.style).ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Song>> GetSong(int id)
        {
            var song= await _context.Songs.Include((p) => p.artist).Include((p) => p.style).SingleOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }
            return new ObjectResult(song);
        }
        [HttpPut]
        public async Task<ActionResult<Song>> PutSong(DopSong s)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_context.Songs.Any(e => e.Id == s.Id))
            {
                return NotFound();
            }
            Song song = await _context.Songs.SingleOrDefaultAsync(m => m.Id == s.Id);
            Artist artist = new Artist();
            Style style = new Style();
            artist = await _context.Artists.SingleOrDefaultAsync(m => m.Name == s.artist);
            style = await _context.Styles.SingleOrDefaultAsync(m => m.Name == s.style);           
            song.Name = s.Name;
            song.artist = artist;
            song.style = style;
            song.Album = s.Album;
            song.Year = s.Year;
            song.file = s.file;
            song.text = s.text;
            _context.Update(song);
            await _context.SaveChangesAsync();
            return Ok(song);
        }
        [HttpPost]
        public async Task<ActionResult<Song>> PostSong(DopSong s)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Song song = new();
            Artist artist = new Artist();
            Style style = new Style();
            artist = await _context.Artists.SingleOrDefaultAsync(m => m.Name == s.artist);
            style = await _context.Styles.SingleOrDefaultAsync(m => m.Name == s.style);         
            song.Name = s.Name;
            song.artist = artist;
            song.style = style;
            song.Album = s.Album;
            song.Year = s.Year;
            song.file = s.file;
            song.text = s.text;
            _context.Songs.Add(song);
            await _context.SaveChangesAsync();
            return Ok(song);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Song>> DeleteSong(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var song = await _context.Songs.SingleOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }

            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();

            return Ok(song);
        }
    }
}
