using Microsoft.AspNetCore.Mvc;

namespace KanbanBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardsController : ControllerBase
    {
        private readonly ILogger<CardsController> _logger;

        public CardsController(ILogger<CardsController> logger)
        {
            _logger = logger;
        }
    }
}