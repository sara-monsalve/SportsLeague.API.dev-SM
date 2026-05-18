using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Enums;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.Domain.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IRefereeRepository _refereeRepository;

        public CardService(
            ICardRepository cardRepository,
            IMatchRepository matchRepository,
            IPlayerRepository playerRepository,
            IRefereeRepository refereeRepository)
        {
            _cardRepository = cardRepository;
            _matchRepository = matchRepository;
            _playerRepository = playerRepository;
            _refereeRepository = refereeRepository;
        }

        public async Task<IEnumerable<Card>> GetByMatchAsync(int matchId)
        {
            return await _cardRepository.GetByMatchAsync(matchId);
        }

        public async Task<Card> CreateAsync(Card card)
        {
            var match = await _matchRepository.GetByIdAsync(card.MatchId);

            if (match == null)
                throw new KeyNotFoundException("Match not found");

            if (match.Status != MatchStatus.InProgress)
                throw new InvalidOperationException("Cards can only be registered when match is InProgress");

            var player = await _playerRepository.GetByIdAsync(card.PlayerId);

            if (player == null)
                throw new KeyNotFoundException("Player not found");

            var referee = await _refereeRepository.GetByIdAsync(card.RefereeId);

            if (referee == null)
                throw new KeyNotFoundException("Referee not found");

            return await _cardRepository.CreateAsync(card);
        }
    }
}