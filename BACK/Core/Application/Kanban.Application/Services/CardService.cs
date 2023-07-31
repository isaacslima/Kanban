using Kanban.Application.Common.Interfaces.Services;
using Kanban.Application.Common.Models.Request;
using Kanban.Domain;

namespace Kanban.Application.Services;

public class CardService : ICardService
{
    public async Task<Card> InsertCard(CreateCardRequest createCardRequest)
    {
        var card = new Card
        {
            Conteudo = createCardRequest.conteudo,
            Lista = createCardRequest.lista,
            Titulo = createCardRequest.titulo,
            Id = Guid.NewGuid()
        };

        if (card.IsValid())
        {
            return card;
        }

        return null;
    }
}
