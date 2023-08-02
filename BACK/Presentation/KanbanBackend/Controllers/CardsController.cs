using Kanban.Application.Common.Interfaces.Services;
using Kanban.Application.Common.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanbanBackend.Controllers;

[ApiController]
[Route("cards")]
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
    [Authorize]
    public async Task<IActionResult> InsertCard(CardRequest cardRequest)
    {
        try
        {
            var card = await _cardService.InsertCard(cardRequest);

            return CreatedAtAction(nameof(InsertCard), new { id = card.Id }, card);
        }
        catch (Exception ex)
        {
            _logger.LogError($"[CardsController.InsertCard] Error: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var listOfCards = await _cardService.GetAllCards();

            return Ok(listOfCards);

        }
        catch (Exception ex)
        {
            _logger.LogError($"[CardsController.GetAll] Error: {ex.Message}");
            return BadRequest(ex.Message);
        }

    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateCard(int id, CardRequest cardRequest)
    {
        try
        {
            var card = await _cardService.GetById(id);

            if (card is null)
            {
                return NotFound();
            }

            card.Conteudo = cardRequest.conteudo;
            card.Lista = cardRequest.lista;
            card.Titulo = cardRequest.titulo;

            await _cardService.Update(card);

            return Ok(card);

        }
        catch (Exception ex)
        {
            _logger.LogError($"[CardsController.UpdateCard] Error: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteCard(int id)
    {
        try
        {
            var card = await _cardService.GetById(id);

            if (card is null)
            {
                return NotFound();
            }

            var listOfRemaingCards = await _cardService.RemoveCard(card);

            return Ok(listOfRemaingCards);

        }
        catch (Exception ex)
        {
            _logger.LogError($"[CardsController.Delete] Error: {ex.Message}");
            return BadRequest(ex.Message);
        }

    }
}
