namespace SportsLeague.Domain.Entities
{
    public class Match : AuditBase
    {
        public DateTime MatchDate { get; set; }
        public string Location { get; set; } = string.Empty;

        // Foreign Keys
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public int RefereeId { get; set; }
        public int TournamentId { get; set; }

        // Navigation Properties
        public Team? HomeTeam { get; set; }
        public Team? AwayTeam { get; set; }
        public Referee? Referee { get; set; }
        public Tournament? Tournament { get; set; }
    }
}