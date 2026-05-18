namespace SportsLeague.Domain.Entities
{
    public class Card : AuditBase
    {
        public int Minute { get; set; }
        public string Type { get; set; } = string.Empty;

        // Foreign Keys
        public int MatchId { get; set; }
        public int PlayerId { get; set; }
        public int RefereeId { get; set; }

        // Navigation
        public Match Match { get; set; } = null!;
        public Player Player { get; set; } = null!;
        public Referee Referee { get; set; } = null!;
    }
}