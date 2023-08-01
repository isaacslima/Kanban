using Kanban.Application.Common.Interfaces.Persistence;
using Kanban.Application.Common.Interfaces.Services;
using Kanban.Application.Common.Models.Request;
using Kanban.Domain;
using Microsoft.Extensions.Logging;

namespace Kanban.Application.Services;

public class CardService : ICardService
{
    public readonly ILogger<Card> _logger;
    public readonly ICardRepository _cardRepository;

    public CardService(ILogger<Card> logger, ICardRepository cardRepository)
    {
        _logger = logger;
        _cardRepository = cardRepository;
    }

    public async Task<Card> InsertCard(CardRequest cardRequest)
    {
        var card = new Card
        {
            Conteudo = cardRequest.conteudo,
            Lista = cardRequest.lista,
            Titulo = cardRequest.titulo
        };

        if (card.IsValid())
        {
            await _cardRepository.CreateCardAsync(card);
            return card;
        }

        return null;
    }

    public async Task<Card> Update(Card card)
    {
        if (card.IsValid())
        {

            await _cardRepository.Update(card);

            var date = DateTime.Now;
            _logger.LogInformation($"{date.ToString("dd/MM/yyyy HH:mm:ss")} - Card {card.Id} - {card.Titulo} - Alterado");
            return card;
        }

        return null;
    }

    public async Task RemoveCard(Card card)
    {
        var date = DateTime.Now;
        _logger.LogInformation($"{date.ToString("dd/MM/yyyy HH:mm:ss")} - Card {card.Id} - {card.Titulo} - Removido");

        await _cardRepository.Delete(card.Id);
    }

    public async Task<IEnumerable<Card>> GetAllCards()
    {
        return await _cardRepository.GetAll();
    }

    public async Task<Card> GetById(int id)
    {
        return await _cardRepository.GetById(id);
    }

}
