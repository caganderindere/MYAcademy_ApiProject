using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_ApiProject.Context;
using MYAcademy_ApiProject.Entities;

namespace MYAcademy_ApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchSubstitutionsController : ControllerBase
    {
        private readonly ApiContext _context;

        public MatchSubstitutionsController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/MatchSubstitutions
        [HttpGet]
        public IActionResult GetSubstitutions()
        {
            var values = _context.MatchSubstitutions
                .Include(x => x.Match)
                .Include(x => x.Team)
                .ToList();

            return Ok(values);
        }

        // GET: api/MatchSubstitutions/1
        [HttpGet("{id}")]
        public IActionResult GetSubstitution(int id)
        {
            var value = _context.MatchSubstitutions
                .Include(x => x.Match)
                .Include(x => x.Team)
                .FirstOrDefault(x => x.MatchSubstitutionId == id);

            return Ok(value);
        }

        // POST: api/MatchSubstitutions
        [HttpPost]
        public IActionResult CreateSubstitution(MatchSubstitution substitution)
        {
            var match = _context.Matches
                .FirstOrDefault(x => x.MatchId == substitution.MatchId);

            if (match == null)
            {
                return BadRequest("Bu maç bulunamadı.");
            }

            if (match.HomeTeamId != substitution.TeamId &&
                match.AwayTeamId != substitution.TeamId)
            {
                return BadRequest("Bu takım bu maçta oynamıyor.");
            }

            _context.MatchSubstitutions.Add(substitution);
            _context.SaveChanges();

            return Ok(substitution);
        }

        // PUT: api/MatchSubstitutions
        [HttpPut]
        public IActionResult UpdateSubstitution(MatchSubstitution substitution)
        {
            _context.MatchSubstitutions.Update(substitution);
            _context.SaveChanges();

            return Ok(substitution);
        }

        // DELETE: api/MatchSubstitutions/1
        [HttpDelete("{id}")]
        public IActionResult DeleteSubstitution(int id)
        {
            var value = _context.MatchSubstitutions.Find(id);

            _context.MatchSubstitutions.Remove(value);
            _context.SaveChanges();

            return Ok(value);
        }
    }
}