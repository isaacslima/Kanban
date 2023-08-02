namespace Kanban.Domain;

public class Card
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string Conteudo { get; set; } = null!;

    public string Lista { get; set; } = null!;

    public string[] ValidateProperties()
    {
        var unvalidProperties = new List<string>();

        if (string.IsNullOrEmpty(Titulo))
        {
            unvalidProperties.Add(nameof(Titulo));
        }

        if (string.IsNullOrEmpty(Conteudo))
        {
            unvalidProperties.Add(nameof(Conteudo));
        }

        if (string.IsNullOrEmpty(Lista))
        {
            unvalidProperties.Add(nameof(Lista));
        }

        return unvalidProperties.ToArray();
    }
}