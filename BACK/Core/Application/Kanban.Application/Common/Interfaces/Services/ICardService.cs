using Kanban.Application.Common.Models.Request;
using Kanban.Domain;

namespace Kanban.Application.Common.Interfaces.Services;

public interface ICardService
{
    Task<Card> InsertCard(CreateCardRequest createCardRequest);
}
