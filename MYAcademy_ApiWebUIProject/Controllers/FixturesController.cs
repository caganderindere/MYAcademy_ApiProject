using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace MYAcademy_ApiWebUIProject.Controllers
{
    public class FixturesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FixturesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(int week = 1)
        {
            var client = _httpClientFactory.CreateClient("SerieAApi");

            Console.WriteLine("API ADRESİ: " + client.BaseAddress);

            var response = await client.GetAsync($"api/Matches/Week/{week}");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }

            var matches = await response.Content
                .ReadFromJsonAsync<List<MatchDto>>();

            if (matches == null)
            {
                matches = new List<MatchDto>();
            }

            return View(matches);
        }
    }

    public class MatchDto
    {
        public int MatchId { get; set; }

        public int Week { get; set; }

        public string Status { get; set; }

        public TeamDto HomeTeam { get; set; }

        public TeamDto AwayTeam { get; set; }

        public DateTime MatchDate { get; set; }

        public string Time { get; set; }

        public string Stadium { get; set; }

        public int? HomeScore { get; set; }

        public int? AwayScore { get; set; }
    }

    public class TeamDto
    {
        public string Name { get; set; }
    }
}