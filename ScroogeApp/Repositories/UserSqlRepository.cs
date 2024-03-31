namespace ScroogeApp.Repositories;

using System.Data.SqlClient;
using Dapper;
using ScroogeApp.Models;

public class UserSqlRepository
{
    private const string connectionString = "Server=localhost;Database=ScroogeDb;User Id=sa;Password=Admin9264!!;";

    public async Task<IEnumerable<User>> GetAllUsersAsync() {
        var connection = new SqlConnection(connectionString);
        return await connection.QueryAsync<User>("select * from Users");
    }
}