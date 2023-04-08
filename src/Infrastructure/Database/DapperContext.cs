using System.Data;
using MySqlConnector;

namespace Infrastructure.Database;

public class DapperContext
{
    private readonly string _connectionString;

    public DapperContext(string? connectionString)
    {
        _connectionString = connectionString ?? throw new ArgumentException("Connection string can not be null");
    }

    public IDbConnection CreateConnection() => new MySqlConnection(_connectionString);
}