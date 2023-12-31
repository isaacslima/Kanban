﻿using Kanban.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

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

            var routeData = context.RouteData;

            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];

            if (controllerName is not null && controllerName.Equals("Cards"))
            {
                if (actionName is not null && actionName.Equals("UpdateCard"))
                {
                    var id = routeData.Values["id"];
                    var okResult = context.Result as OkObjectResult;
                    var card = okResult?.Value as Card;

                    _logger.LogInformation($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} - Card {id} - {card?.Titulo} - Alterado");
                }
                else if (actionName is not null && actionName.Equals("DeleteCard"))
                {
                    var id = routeData.Values["id"];

                    _logger.LogInformation($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} - Card {id} - Removido");
                }
            }


        }
    }
}