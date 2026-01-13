using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

namespace MyRecipeBook.Infraestructure.Migrations.Versions
{
    public abstract class VersionBase : ForwardOnlyMigration
    {
        // Método auxiliar para criar tabelas com colunas padrão.
        // Faço a herança "original" do FluentMigrator aqui, mas como a classe é abstrata,
        // eu passo a responsabilidade da implementação para o chamador, assim crio as colunas base por aqui, enquanto chamo esse método na implementação.
        protected ICreateTableColumnOptionOrWithColumnSyntax CreateTable(string table)
        {
           return Create.Table(table)
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("CreateOn").AsDateTime().NotNullable()
                .WithColumn("Active").AsBoolean().NotNullable();
        }
    }
}
