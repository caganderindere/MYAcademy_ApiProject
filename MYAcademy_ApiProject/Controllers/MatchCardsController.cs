using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_ApiProject.Context;
using MYAcademy_ApiProject.Entities;

namespace MYAcademy_ApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchCardsController : ControllerBase
    {
        private readonly ApiContext _context;

        public MatchCardsController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/MatchCards
        [HttpGet]
        public IActionResult GetCards()
        {
            var values = _context.MatchCards
                .Include(x => x.Match)
                .Include(x => x.Team)
                .ToList();

            return Ok(values);
        }

        // GET: api/MatchCards/1
        [HttpGet("{id}")]
        public IActionResult GetCard(int id)
        {
            var value = _context.MatchCards
                .Include(x => x.Match)
                .Include(x => x.Team)
                .FirstOrDefault(x => x.MatchCardId == id);

            return Ok(value);
        }

        // POST: api/MatchCards
        [HttpPost]
        public IActionResult CreateCard(MatchCard card)
        {
            var match = _context.Matches
                .FirstOrDefault(x => x.MatchId == card.MatchId);

            if (match == null)
            {
                return BadRequest("Bu maç bulunamadı.");
            }

            if (match.HomeTeamId != card.TeamId &&
                match.AwayTeamId != card.TeamId)
            {
                return BadRequest("Bu takım bu maçta oynamıyor.");
            }

            _context.MatchCards.Add(card);
            _context.SaveChanges();

            return Ok(card);
        }

        // PUT: api/MatchCards
        [HttpPut]
        public IActionResult UpdateCard(MatchCard card)
        {
            _context.MatchCards.Update(card);
            _context.SaveChanges();

            return Ok(card);
        }

        // DELETE: api/MatchCards/1
        [HttpDelete("{id}")]
        public IActionResult DeleteCard(int id)
        {
            var value = _context.MatchCards.Find(id);

            _context.MatchCards.Remove(value);
            _context.SaveChanges();

            return Ok(value);
        }
    }
}