using Dapper;
using Kanban.Application.Common.Interfaces;
using Kanban.Application.Common.Interfaces.Persistence;
using Kanban.Domain;

namespace Kanban.Infrastructure.Data.Persistence.Repositories;

public class CardRepository : ICardRepository
{
    private DataContext _context;

    public CardRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Card>> GetAll()
    {
        using var connection = _context.CreateConnection();
        var sql = """
            SELECT * FROM Cards
        """;
        return await connection.QueryAsync<Card>(sql);
    }

    public async Task CreateCardAsync(Card card)
    {
        using var connection = _context.CreateConnection();
        var sql = """
            INSERT INTO Cards (Titulo, Conteudo, Lista)
            VALUES (@Titulo, @Conteudo, @Lista)
        """;
        await connection.ExecuteAsync(sql, card);
    }

    public async Task<Card> GetById(int id)
    {
        using var connection = _context.CreateConnection();
        var sql = """
            SELECT * FROM Cards 
            WHERE Id = @id
        """;
        return await connection.QuerySingleOrDefaultAsync<Card>(sql, new { id });
    }

    public async Task Update(Card card)
    {
        using var connection = _context.CreateConnection();
        var sql = """
            UPDATE Cards 
            SET Titulo = @Titulo,
                Conteudo = @Conteudo,
                Lista = @Lista
            WHERE Id = @Id
        """;
        await connection.ExecuteAsync(sql, card);
    }

    public async Task Delete(int id)
    {
        using var connection = _context.CreateConnection();
        var sql = """
            DELETE FROM Cards 
            WHERE Id = @id
        """;
        await connection.ExecuteAsync(sql, new { id });
    }
}
