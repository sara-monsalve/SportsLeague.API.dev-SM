using SportsLeague.Domain.Enums;

namespace SportsLeague.Domain.Entities
{
    public class Match : AuditBase
    {
        public DateTime MatchDate { get; set; }

        public string Venue { get; set; } = string.Empty;

        public MatchStatus Status { get; set; } = MatchStatus.Scheduled;

        // Foreign Keys
        public int HomeTeamId { get; set; }

        public int AwayTeamId { get; set; }

        public int TournamentId { get; set; }

        public int? RefereeId { get; set; }

        // Navegation Properties
        public Team HomeTeam { get; set; } = null!;

        public Team AwayTeam { get; set; } = null!;

        public Tournament Tournament { get; set; } = null!;

        public Referee? Referee { get; set; }
    }
}