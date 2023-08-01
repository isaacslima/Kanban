using Kanban.Application.Common.Interfaces.Services;
using Kanban.Application.Common.Models.Request;
using Kanban.Domain;
using Microsoft.AspNetCore.Mvc;

namespace KanbanBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class CardsController : ControllerBase
{
    private readonly ILogger<CardsController> _logger;
    private readonly ICardService _cardService;

    public CardsController(ILogger<CardsController> logger, ICardService cardService)
    {
        _logger = logger;
        _cardService = cardService;
    }

    [HttpPost]
    public IActionResult InsertCard(CardRequest cardRequest)
    {
        try
        {
            var card = _cardService.InsertCard(cardRequest);

            return Ok(cardRequest);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCard(Guid id, CardRequest cardRequest)
    {
        //Todo get card
        //NotFound();

        try
        {
            var card = new Card
            {
                Id = id,
                Conteudo = cardRequest.conteudo,
                Lista = cardRequest.lista,
                Titulo = cardRequest.titulo
            };

            _cardService.Update(card);

            return Ok(card);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}