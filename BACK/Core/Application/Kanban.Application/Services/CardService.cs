using Kanban.Application.Common.Interfaces.Persistence;
using Kanban.Application.Common.Interfaces.Services;
using Kanban.Application.Common.Models.Request;
using Kanban.Domain;

namespace Kanban.Application.Services;

public class CardService : ICardService
{
    public readonly ICardRepository _cardRepository;

    public CardService(ICardRepository cardRepository)
    {
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
        return card;
    }

    public async Task<IEnumerable<Card>> RemoveCard(Card card)
    {
        var date = DateTime.Now;

        var deleted = await _cardRepository.Delete(card.Id);

        if (deleted == 1)
        {
            return await GetAllCards();
        }

        throw (new Exception("Não foi possível remover o card"));
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
