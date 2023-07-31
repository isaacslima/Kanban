using Kanban.Application.Common.Interfaces.Services;
using Kanban.Application.Common.Models.Request;
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
    public IActionResult InsertCard(CreateCardRequest createCardRequest)
    {
        try
        {
            var card = _cardService.InsertCard(createCardRequest);

            return Ok(createCardRequest);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}