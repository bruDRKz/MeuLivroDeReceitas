using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;

namespace MyRecipeBook.Infraestructure.DataAccess
{
    public class MyRecipeBookDbContext : DbContext
    {
        //Configuração do meu contexto do Entity Framework, para mapear as entidades com as tabelas do banco de dados.
        public MyRecipeBookDbContext(DbContextOptions<MyRecipeBookDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //sobreescreve o método OnModelCreating para aplicar as configurações de mapeamento das entidades.
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyRecipeBookDbContext).Assembly);
        }
    }
}
