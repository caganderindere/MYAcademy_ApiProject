using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_ApiProject.Context;

namespace MYAcademy_ApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StandingsController : ControllerBase
    {
        private readonly ApiContext _context;

        public StandingsController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetStandings()
        {
            var teams = _context.Teams.ToList();

            var matches = _context.Matches
                .Where(x => x.Status == "Tamamlandı")
                .ToList();

            var standings = teams.Select(team =>
            {
                var teamMatches = matches
                    .Where(x => x.HomeTeamId == team.TeamId ||
                                x.AwayTeamId == team.TeamId)
                    .ToList();

                int played = teamMatches.Count;
                int wins = 0;
                int draws = 0;
                int losses = 0;
                int goalsFor = 0;
                int goalsAgainst = 0;

                foreach (var match in teamMatches)
                {
                    if (match.HomeTeamId == team.TeamId)
                    {
                        goalsFor += match.HomeScore ?? 0;
                        goalsAgainst += match.AwayScore ?? 0;

                        if (match.HomeScore > match.AwayScore)
                            wins++;
                        else if (match.HomeScore == match.AwayScore)
                            draws++;
                        else
                            losses++;
                    }
                    else
                    {
                        goalsFor += match.AwayScore ?? 0;
                        goalsAgainst += match.HomeScore ?? 0;

                        if (match.AwayScore > match.HomeScore)
                            wins++;
                        else if (match.AwayScore == match.HomeScore)
                            draws++;
                        else
                            losses++;
                    }
                }

                int goalDifference = goalsFor - goalsAgainst;
                int points = (wins * 3) + (draws * 1);

                return new
                {
                    TeamId = team.TeamId,
                    Team = team.Name,
                    O = played,
                    G = wins,
                    B = draws,
                    M = losses,
                    AG = goalsFor,
                    YG = goalsAgainst,
                    AV = goalDifference,
                    Puan = points
                };
            })
            .OrderByDescending(x => x.Puan)
            .ThenByDescending(x => x.AV)
            .ThenByDescending(x => x.AG)
            .ToList();

            return Ok(standings);
        }
    }
}