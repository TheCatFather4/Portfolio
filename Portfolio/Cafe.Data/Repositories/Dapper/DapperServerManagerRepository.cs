using Cafe.Core.Entities;
using Cafe.Core.Interfaces.Repositories;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Cafe.Data.Repositories.Dapper
{
    public class DapperServerManagerRepository : IServerManagerRepository
    {
        private readonly string _connectionString;

        public DapperServerManagerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddServerAsync(Server server)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = @"INSERT INTO [Server] (FirstName, LastName, HireDate, TermDate, DoB) 
                            VALUES (@FirstName, @LastName, @HireDate, @TermDate, @DoB);";

                var parameters = new
                {
                    server.FirstName,
                    server.LastName,
                    server.HireDate,
                    server.TermDate,
                    server.DoB
                };

                await cn.ExecuteAsync(sql, parameters);
            }
        }

        public async Task<List<Server>> GetAllServersAsync()
        {
            List<Server> servers = new List<Server>();

            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM [Server];";

                servers = (await cn.QueryAsync<Server>(sql)).ToList();
            }

            return servers;
        }

        public async Task<Server?> GetServerByIdAsync(int serverId)
        {
            Server? server = new Server();

            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT * FROM [Server] AS s WHERE s.ServerID = @ServerID;";

                var parameter = new
                {
                    ServerID = serverId,
                };

                server = await cn.QueryFirstOrDefaultAsync<Server>(sql, parameter);
            }

            return server;
        }

        public async Task UpdateServerAsync(Server server)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = @"UPDATE [Server] SET 
                            FirstName = @FirstName, 
                            LastName = @LastName, 
                            DoB = @DoB, 
                            HireDate = @HireDate, 
                            TermDate = @TermDate 
                            WHERE ServerID = @ServerID;";

                var parameters = new
                {
                    server.ServerID,
                    server.FirstName,
                    server.LastName,
                    server.DoB,
                    server.HireDate,
                    server.TermDate
                };

                await cn.ExecuteAsync(sql, parameters);
            }
        }
    }
}