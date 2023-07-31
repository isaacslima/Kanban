namespace Kanban.Application.Common.Models.Request
{
    public record CreateCardRequest(string titulo, string conteudo, string lista);
}
