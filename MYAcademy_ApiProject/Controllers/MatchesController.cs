using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_ApiProject.Context;
using MYAcademy_ApiProject.Entities;

namespace MYAcademy_ApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly ApiContext _context;

        public MatchesController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Matches
        [HttpGet]
        public IActionResult GetMatches()
        {
            var values = _context.Matches
                .Include(x => x.HomeTeam)
                .Include(x => x.AwayTeam)
                .ToList();

            return Ok(values);
        }

        // GET: api/Matches/5
        [HttpGet("{id}")]
        public IActionResult GetMatch(int id)
        {
            var value = _context.Matches
                .Include(x => x.HomeTeam)
                .Include(x => x.AwayTeam)
                .Include(x => x.MatchGoals)
                .Include(x => x.MatchCards)
                .Include(x => x.MatchSubstitutions)
                .FirstOrDefault(x => x.MatchId == id);

            return Ok(value);

        }
        [HttpGet("Week/{week}")]
        public IActionResult GetMatchesByWeek(int week)
        {
            var values = _context.Matches
                   .Where(x => x.Week == week)
                   .Include(x => x.HomeTeam)
                   .Include(x => x.AwayTeam)
                   .ToList();

            return Ok(values);
        }


        // POST: api/Matches
        [HttpPost]
        public IActionResult CreateMatch(Match match)
        {
            if (match.HomeTeamId == match.AwayTeamId)
            {
                return BadRequest("Ev sahibi ve deplasman takımı aynı olamaz.");
            }

            if (_context.Matches.Any(x =>
            x.Week == match.Week &&
              (x.HomeTeamId == match.HomeTeamId ||
               x.AwayTeamId == match.HomeTeamId ||
               x.HomeTeamId == match.AwayTeamId ||
               x.AwayTeamId == match.AwayTeamId)))
            {
                return BadRequest("Bu takımlardan biri aynı hafta içinde zaten maç yapıyor.");
            }

            if (match.Status == "Tamamlandı" &&
    (match.HomeScore == null || match.AwayScore == null))
            {
                return BadRequest("Tamamlanan maçlarda skor bilgisi zorunludur.");
            }

            _context.Matches.Add(match);
            _context.SaveChanges();

            return Ok(match);
        }

        // PUT: api/Matches
        [HttpPut]
        public IActionResult UpdateMatch(Match match)
        {
            _context.Matches.Update(match);
            _context.SaveChanges();

            return Ok(match);
        }

        // DELETE: api/Matches/5
        [HttpDelete("{id}")]
        public IActionResult DeleteMatch(int id)
        {
            var value = _context.Matches.Find(id);

            _context.Matches.Remove(value);
            _context.SaveChanges();

            return Ok(value);
        }

    }
}