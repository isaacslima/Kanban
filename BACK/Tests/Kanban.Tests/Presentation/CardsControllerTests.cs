using Kanban.Application.Common.Interfaces.Services;
using Kanban.Application.Common.Models.Request;
using Kanban.Domain;
using KanbanBackend.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.Tests.Presentation
{
    public class CardsControllerTests
    {
        private readonly CardsController _controller;
        private readonly Mock<ILogger<CardsController>> _mockLogger;
        private readonly Mock<ICardService> _mockCardService;

        public CardsControllerTests()
        {
            _mockLogger = new Mock<ILogger<CardsController>>();
            _mockCardService = new Mock<ICardService>();
            _controller = new CardsController(_mockLogger.Object, _mockCardService.Object);
        }

        [Fact]
        public async Task InsertCard_ValidRequest_ReturnsOkResult()
        {
            var cardRequest = new CardRequest("titulo", "conteudo", "lista");
            var insertedCard = new Card 
            { 
                Id = 1,
                Conteudo = "conteudo",
                Lista = "lista",
                Titulo = "titulo"
            };

            _mockCardService.Setup(service => service.InsertCard(cardRequest))
                            .ReturnsAsync(insertedCard);

            var result = await _controller.InsertCard(cardRequest);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<Card>(okResult.Value);
            Assert.Equal(insertedCard, response);
        }

        [Fact]
        public async Task UpdateCard_ExistingCard_ReturnsOkResultWithUpdatedCard()
        {
            var id = 1;
            var cardRequest = new CardRequest("titulo", "conteudo", "lista");
            var existingCard = new Card
            {
                Id = 1,
                Conteudo = "conteudo",
                Lista = "lista",
                Titulo = "titulo"
            };

            _mockCardService.Setup(service => service.GetById(id))
                            .ReturnsAsync(existingCard);

            var result = await _controller.UpdateCard(id, cardRequest);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<Card>(okResult.Value);
            Assert.Equal(existingCard, response);
        }

        [Fact]
        public async Task UpdateCard_NonExistingCard_ReturnsNotFoundResult()
        {
            var id = 1;
            var cardRequest = new CardRequest("titulo", "conteudo", "lista");

            _mockCardService.Setup(service => service.GetById(id))
                            .ReturnsAsync((Card)null);

            var result = await _controller.UpdateCard(id, cardRequest);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetAll_ReturnsListOfCards()
        {
            var listOfCards = new List<Card> 
            {
                new Card
                {
                    Id = 1,
                    Conteudo = "conteudo1",
                    Lista = "lista1",
                    Titulo = "titulo1"
                },
                new Card
                {
                    Id = 2,
                    Conteudo = "conteudo2",
                    Lista = "lista2",
                    Titulo = "titulo2"
                }
            };

            _mockCardService.Setup(service => service.GetAllCards())
                            .ReturnsAsync(listOfCards);

            var result = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsAssignableFrom<List<Card>>(okResult.Value);
            Assert.Equal(listOfCards, response);
        }

        [Fact]
        public async Task Delete_ExistingCard_ReturnsOkResultWithDeletedCard()
        {
            var id = 1;
            var existingCard = new Card
            {
                Id = 1,
                Conteudo = "conteudo",
                Lista = "lista",
                Titulo = "titulo"
            };

            _mockCardService.Setup(service => service.GetById(id))
                            .ReturnsAsync(existingCard);

            var result = await _controller.Delete(id);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<Card>(okResult.Value);
            Assert.Equal(existingCard, response);
        }

        [Fact]
        public async Task Delete_NonExistingCard_ReturnsNotFoundResult()
        {
            var id = 1;

            _mockCardService.Setup(service => service.GetById(id))
                            .ReturnsAsync((Card)null);

            var result = await _controller.Delete(id);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task InsertCard_ExceptionInCardService_ReturnsBadRequestWithErrorMessage()
        {
            var cardRequest = new CardRequest("titulo", "conteudo", "lista");
            var errorMessage = "Some error occurred during card insertion.";

            _mockCardService.Setup(service => service.InsertCard(cardRequest))
                            .ThrowsAsync(new Exception(errorMessage));

            var result = await _controller.InsertCard(cardRequest);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(errorMessage, badRequestResult.Value);
        }

        [Fact]
        public async Task UpdateCard_ExceptionInCardService_ReturnsBadRequestWithErrorMessage()
        {
            var id = 1;
            var cardRequest = new CardRequest("titulo", "conteudo", "lista");
            var errorMessage = "Some error occurred during card update.";

            _mockCardService.Setup(service => service.GetById(id))
                            .ReturnsAsync(new Card());

            _mockCardService.Setup(service => service.Update(It.IsAny<Card>()))
                            .ThrowsAsync(new Exception(errorMessage));

            var result = await _controller.UpdateCard(id, cardRequest);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(errorMessage, badRequestResult.Value);
        }

        [Fact]
        public async Task GetAll_ExceptionInCardService_ReturnsBadRequestWithErrorMessage()
        {
            var errorMessage = "Some error occurred during fetching the list of cards.";

            _mockCardService.Setup(service => service.GetAllCards())
                            .ThrowsAsync(new Exception(errorMessage));

            var result = await _controller.GetAll();

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(errorMessage, badRequestResult.Value);
        }

        [Fact]
        public async Task Delete_ExceptionInCardService_ReturnsBadRequestWithErrorMessage()
        {
            var id = 1;
            var errorMessage = "Some error occurred during card deletion.";

            _mockCardService.Setup(service => service.GetById(id))
                            .ReturnsAsync(new Card());

            _mockCardService.Setup(service => service.RemoveCard(It.IsAny<Card>()))
                            .ThrowsAsync(new Exception(errorMessage));

            var result = await _controller.Delete(id);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(errorMessage, badRequestResult.Value);
        }
    }
}
