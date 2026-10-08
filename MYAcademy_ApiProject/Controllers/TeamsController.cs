using Microsoft.AspNetCore.Mvc;
using MYAcademy_ApiProject.Context;
using MYAcademy_ApiProject.Entities;

namespace MYAcademy_ApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ApiContext _context;

        public TeamsController(ApiContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetTeams()
        {
            var values = _context.Teams.ToList();

            return Ok(values);
        }
        [HttpPost]
        public IActionResult CreateTeam(Team team)
        {
            _context.Teams.Add(team);
            _context.SaveChanges();

            return Ok(team);
        }
        [HttpGet("{id}")]
        public IActionResult GetTeam(int id)
        {
            var value = _context.Teams.Find(id);

            return Ok(value);
        }
        [HttpPut]
        public IActionResult UpdateTeam(Team team)
        {
            _context.Teams.Update(team);
            _context.SaveChanges();
            return Ok(team);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteTeam(int id)
        {
            var value = _context.Teams.Find(id);

            _context.Teams.Remove(value);
            _context.SaveChanges();

            return Ok(value);
        }
    }

}