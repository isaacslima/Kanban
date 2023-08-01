using Kanban.Application.Common.Interfaces.Services;
using Kanban.Application.Common.Models.Request;
using Kanban.Domain;
using Microsoft.Extensions.Logging;

namespace Kanban.Application.Services;

public class CardService : ICardService
{
    public readonly ILogger<Card> _logger;

    public CardService(ILogger<Card> logger)
    {
        _logger = logger;
    }

    public async Task<Card> InsertCard(CardRequest cardRequest)
    {
        var card = new Card
        {
            Conteudo = cardRequest.conteudo,
            Lista = cardRequest.lista,
            Titulo = cardRequest.titulo,
            Id = Guid.NewGuid()
        };

        if (card.IsValid())
        {
            return card;
        }

        return null;
    }

    public async Task<Card> Update(Card card)
    {
        if (card.IsValid())
        {
            //Todo update card
            var date = DateTime.Now;
            _logger.LogInformation($"{date.ToString("dd/MM/yyyy HH:mm:ss")} - Card {card.Id} - {card.Titulo} - Alterado");
            return card;
        }

        return null;
    }

    public async Task<IEnumerable<Card>> RemoveCard(Card card)
    {
        //todo remove card
        var date = DateTime.Now;
        _logger.LogInformation($"{date.ToString("dd/MM/yyyy HH:mm:ss")} - Card {card.Id} - {card.Titulo} - Removido");

        return new List<Card>();
    }

    public async Task<IEnumerable<Card>> GetAllCards()
    {
        //Todo list all cards
        return new List<Card>();
    }
}
