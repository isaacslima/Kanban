using Kanban.Application.Common.Interfaces.Services;
using Kanban.Application.Common.Models.Request;
using Kanban.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
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
    public async Task<IActionResult> InsertCard(CardRequest cardRequest)
    {
        try
        {
            var card = await _cardService.InsertCard(cardRequest);

            return Ok(card);
        }
        catch (Exception ex)
        {
            _logger.LogError($"[CardsController.InsertCard] Error: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
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

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var listOfCards = await _cardService.GetAllCards();

            return Ok(listOfCards);

        }
        catch(Exception ex)
        {
            _logger.LogError($"[CardsController.GetAll] Error: {ex.Message}");
            return BadRequest(ex.Message);
        }
        
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var card = await _cardService.GetById(id);

            if (card is null)
            {
                return NotFound();
            }

            await _cardService.RemoveCard(card);

            return Ok(card);

        }
        catch (Exception ex)
        {
            _logger.LogError($"[CardsController.Delete] Error: {ex.Message}");
            return BadRequest(ex.Message);
        }

    }
}