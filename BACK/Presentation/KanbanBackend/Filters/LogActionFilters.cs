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
        // Nada a fazer antes da execução da ação.
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
                var cardRequest = context.Result;
                //ActionArguments["cardRequest"] as CardRequest;

                //_logger.LogInformation($"{date.ToString("dd/MM/yyyy HH:mm:ss")} - Card {card.Id} - {card.Titulo} - Alterado");

                //Console.WriteLine($"{DateTime.Now} - Card {id} - {cardRequest?.Titulo} - Alterado");
            }
            else if (context.HttpContext.Request.Method == "DELETE")
            {
                var id = context.RouteData.Values["id"];
                var card = context.Controller.GetType().Name.Replace("Controller", "");
                Console.WriteLine($"{DateTime.Now} - Card {id} - {card} - Removido");
            }
        }
    }
}