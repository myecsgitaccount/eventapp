using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEnd.Data;
using BackEnd.Infrastructure;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeakersController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public SpeakersController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: api/Speakers
        [HttpGet]
        public async Task<ActionResult<List<EventDTO.SpeakerResponse>>> GetSpeakers()
        {
            var speakers = await _db.Speakers.AsNoTracking()
                                            .Include(s => s.SessionSpeakers)
                                            .ThenInclude(ss => ss.Session)
                                            .Select(s => s.MapSpeakerResponse())
                                            .ToListAsync();
            return speakers;
        }

        // GET: api/Speakers/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<EventDTO.SpeakerResponse>> GetSpeaker(int id)
        {
            var speaker = await _db.Speakers.AsNoTracking()
                                            .Include(s => s.SessionSpeakers)
                                            .ThenInclude(ss => ss.Session)
                                            .SingleOrDefaultAsync(s => s.ID == id);
            if (speaker == null)
            {
                return NotFound();
            }
            var result = speaker.MapSpeakerResponse();
            return Ok(result); 
        }

        // PUT: api/Speakers/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutSpeaker(int id, EventDTO.Speaker input)
        {
            var speaker = await _db.FindAsync<Speaker>(id);

            if (speaker == null)
            {
                return NotFound();
            }

            speaker.Name = input.Name;
            speaker.WebSite = input.WebSite;
            speaker.Bio = input.Bio;

            // TODO: Handle exceptions, e.g. concurrency
            await _db.SaveChangesAsync();

            return NoContent();
        }


        // POST: api/Speakers
        [HttpPost]
        public async Task<ActionResult<EventDTO.SpeakerResponse>> PostSpeaker(EventDTO.Speaker input)
        {
            var speaker = new Speaker
            {
                Name = input.Name,
                WebSite = input.WebSite,
                Bio = input.Bio
            };

            _db.Speakers.Add(speaker);
            await _db.SaveChangesAsync();

            var result = speaker.MapSpeakerResponse();

            return CreatedAtAction(nameof(GetSpeaker), new { id = speaker.ID }, result);
        }

        // DELETE: api/Speakers/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<EventDTO.SpeakerResponse>> DeleteSpeaker(int id)
        {
            var speaker = await _db.FindAsync<Speaker>(id);

            if (speaker == null)
            {
                return NotFound();
            }

            _db.Remove(speaker);
            await _db.SaveChangesAsync();

            return speaker.MapSpeakerResponse();
        }
    }
}
