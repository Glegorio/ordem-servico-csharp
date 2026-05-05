using System;
using System.Configuration;
using Npgsql;

namespace OrdemServico.Infra.Database
{
    /// <summary>
    /// Fábrica de conexões com o PostgreSQL
    /// </summary>
    public static class ConnectionFactory
    {
        private const string ConnectionStringName = "PostgreSql";

        public static NpgsqlConnection CreateOpenConnection()
        {
            var settings = ConfigurationManager.ConnectionStrings[ConnectionStringName];
            if (settings == null)
                throw new InvalidOperationException(
                    "Connection string 'PostgreSql' nao encontrada no App.config.");
            var conn = new NpgsqlConnection(settings.ConnectionString);
            conn.Open();
            return conn;
        }
    }
}