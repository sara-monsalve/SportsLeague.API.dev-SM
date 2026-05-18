namespace SportsLeague.Domain.Entities
{
    public class Goal : AuditBase
    {
        public int Minute { get; set; }

        // Foreign Keys
        public int MatchId { get; set; }
        public int PlayerId { get; set; }
        public int TeamId { get; set; }

        // Navigation
        public Match Match { get; set; } = null!;
        public Player Player { get; set; } = null!;
        public Team Team { get; set; } = null!;
    }
}