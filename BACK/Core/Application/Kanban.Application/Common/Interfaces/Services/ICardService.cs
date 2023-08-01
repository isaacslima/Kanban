using Kanban.Application.Common.Models.Request;
using Kanban.Domain;

namespace Kanban.Application.Common.Interfaces.Services;

public interface ICardService
{
    Task<IEnumerable<Card>> GetAllCards();
    Task<Card> InsertCard(CardRequest cardRequest);
    Task<IEnumerable<Card>> RemoveCard(Card card);
    Task<Card> Update(Card card);
}
