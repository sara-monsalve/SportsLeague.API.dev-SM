namespace SportsLeague.Domain.Entities
{
    public class MatchResult : AuditBase
    {
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }

        // Foreign Key
        public int MatchId { get; set; }

        // Navigation Property
        public Match? Match { get; set; }
    }
}