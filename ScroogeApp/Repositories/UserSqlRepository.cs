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

    public async Task<User?> GetUserByIdAsync(int id) {
        var connection = new SqlConnection(connectionString);
        return await connection.QueryFirstOrDefaultAsync<User>(
            "select * from Users where Id = @Id",
             new { Id = id});
    }

    public async Task CreateNewUserAsync(User newUser) {
        var connection = new SqlConnection(connectionString);
        await connection.ExecuteAsync(
            @"insert into Users (Name, Surname, BirthDate)
            values(@Name, @Surname, @BirthDate)",
            newUser);
    }
}