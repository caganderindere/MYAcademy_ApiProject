namespace MYAcademy_ApiProject.Entities
{
    public class MatchGoal
    {
        public int MatchGoalId { get; set; }

        public int MatchId { get; set; }

        public int TeamId { get; set; }

        public string PlayerName { get; set; }

        public int Minute { get; set; }

        public Match Match { get; set; }
        
        public Team Team { get; set; }
    }
}