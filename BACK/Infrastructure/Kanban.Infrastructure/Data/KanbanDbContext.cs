using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Kanban.Infrastructure.Data;

public class DataContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IDbConnection CreateConnection()
    {
        return new SqliteConnection(Configuration.GetConnectionString("KanbanDatabase"));
    }

    public async Task Init()
    {
        using var connection = CreateConnection();
        await _initCards();
        async Task _initCards()
        {
            var sql = """
                CREATE TABLE IF NOT EXISTS 
                Cards (
                    Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    Titulo TEXT,
                    Lista TEXT,
                    Conteudo TEXT
                );
            """;
            if (connection is not null)
            {
                await connection.ExecuteAsync(sql);
            }


        }
    }
}
