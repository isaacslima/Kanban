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

        ValidateCard(card);

        await _cardRepository.CreateCardAsync(card);
        return card;
    }

    private void ValidateCard(Card card)
    {
        var unvalidProperties = card.ValidateProperties();
        if (unvalidProperties.Count() > 0)
        {
            var properties = string.Join(",", unvalidProperties);

            throw (new Exception($"O(s) campo(s) obrigatório(s) não foi(foram) preenchido(s) Campos: {properties}"));
        }
    }

    public async Task<Card> Update(Card card)
    {
        ValidateCard(card);

        await _cardRepository.Update(card);

        var date = DateTime.Now;
        _logger.LogInformation($"{date.ToString("dd/MM/yyyy HH:mm:ss")} - Card {card.Id} - {card.Titulo} - Alterado");
        return card;
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
