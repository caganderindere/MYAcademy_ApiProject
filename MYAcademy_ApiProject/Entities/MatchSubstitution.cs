namespace MYAcademy_ApiProject.Entities
{
    public class MatchSubstitution
    {
        public int MatchSubstitutionId { get; set; }

        public int MatchId { get; set; }

        public int TeamId { get; set; }

        public string PlayerOut { get; set; }

        public string PlayerIn { get; set; }

        public int Minute { get; set; }

        public Match Match { get; set; }

        public Team Team { get; set; }
    }
}