using Kanban.Domain;

namespace Kanban.Application.Common.Interfaces.Persistence;

public interface ICardRepository
{
    Task CreateCardAsync(Card card);
    Task Delete(int id);
    Task<IEnumerable<Card>> GetAll();
    Task<Card> GetById(int id);
    Task Update(Card card);
}
