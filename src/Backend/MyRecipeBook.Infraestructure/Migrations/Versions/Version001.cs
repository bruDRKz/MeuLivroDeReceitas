using FluentMigrator;

namespace MyRecipeBook.Infraestructure.Migrations.Versions
{
    [Migration(DatabaseVersions.TABLE_USER, "Create table to save the user information")]
    public class Version001 : VersionBase
    {
        //Na classe "padrão" do FluentMigrator, existe também um método Down, para desfazer migrations especificas,
        //porém como dificilmente ele é utilizado, a equipe do Fluent criou essa ForwardOnly, que so contém o método Up.
        public override void Up()
        {
            //Método abstrato herdado da VersionBase, que cria as colunas padrão.
            CreateTable("Users")                
                .WithColumn("Name").AsString(255).NotNullable()
                .WithColumn("Email").AsString(255).NotNullable()
                .WithColumn("Password").AsString(2000).NotNullable();
        }
    }
}