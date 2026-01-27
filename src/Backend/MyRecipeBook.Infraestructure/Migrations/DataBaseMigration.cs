using Dapper;
using FluentMigrator.Runner;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;

namespace MyRecipeBook.Infraestructure.Migrations
{
    public static class DataBaseMigration
    {
        public static void Migrate(string connectionString, IServiceProvider serviceProvider)
        {
            EnsureDatabaseCreated(connectionString);
            MigrationDatabase(serviceProvider);
        }
        private static void EnsureDatabaseCreated(string connectionString)
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
            var databaseName = connectionStringBuilder.InitialCatalog;

            connectionStringBuilder.Remove("Database");

            using var dbConnection = new SqlConnection(connectionStringBuilder.ConnectionString);

            var parameters = new DynamicParameters();
            parameters.Add("name", databaseName);

            var records = dbConnection.Query("SELECT * FROM sys.databases WHERE name = @name", parameters);

            if(records.Any() == false)
                dbConnection.Execute($"CREATE DATABASE {databaseName}");
        }

        //Lista e aplica as migrations pendentes
        private static void MigrationDatabase(IServiceProvider serviceProvider)
        {
            // Pega o serviço de MigrationRunner do provedor de serviços
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            runner.ListMigrations();

            runner.MigrateUp();
        }
    }
}
