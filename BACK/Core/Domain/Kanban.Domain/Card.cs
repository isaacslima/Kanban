namespace Kanban.Domain;

public class Card
{
    public Guid Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string Conteudo { get; set; } = null!;

    public string Lista { get; set; } = null!;
}