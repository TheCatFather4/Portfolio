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

        public void AddServer(Server server)
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

                cn.Execute(sql, parameters);
            }
        }

        public List<Server> GetAllServers()
        {
            List<Server> servers = new List<Server>();

            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM [Server];";

                servers = cn.Query<Server>(sql).ToList();
            }

            return servers;
        }

        public Server GetServerById(int serverId)
        {
            Server server = new Server();

            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT * FROM [Server] AS s WHERE s.ServerID = @ServerID;";

                var parameter = new
                {
                    ServerID = serverId,
                };

                server = cn.QueryFirstOrDefault<Server>(sql, parameter);
            }

            return server;
        }

        public void UpdateServer(Server server)
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

                cn.Execute(sql, parameters);
            }
        }
    }
}