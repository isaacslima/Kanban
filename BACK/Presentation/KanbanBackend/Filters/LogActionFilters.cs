using Kanban.Application.Common.Models.Request;
using Kanban.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KanbanBackend.Filters;
public class LogActionFilter : IActionFilter
{
    public readonly ILogger<LogActionFilter> _logger;

    public LogActionFilter(ILogger<LogActionFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var action = context.ActionDescriptor.DisplayName;
        _logger.LogInformation($"Executando: {action}");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception == null)
        {
            var controller = context.Controller as ControllerBase;
            var action = context.ActionDescriptor.DisplayName;

            if (context.HttpContext.Request.Method == "PUT")
            {
                var id = context.RouteData.Values["id"];
                var okResult = context.Result as OkObjectResult;
                var card = okResult?.Value as Card;

                _logger.LogInformation($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} - Card {id} - {card?.Titulo} - Alterado");

            }
            else if (context.HttpContext.Request.Method == "DELETE")
            {
                var id = context.RouteData.Values["id"];

                _logger.LogInformation($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} - Card {id} - Removido");
            }
        }
    }
}