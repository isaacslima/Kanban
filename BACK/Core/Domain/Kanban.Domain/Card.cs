namespace Kanban.Domain;

public class Card
{
    public Guid Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string Conteudo { get; set; } = null!;

    public string Lista { get; set; } = null!;

    public bool IsValid()
    {
        if (string.IsNullOrEmpty(Titulo) || 
            string.IsNullOrEmpty(Conteudo) || 
            string.IsNullOrEmpty(Lista))
        {
            return false;
        }

        return true;
    }
}