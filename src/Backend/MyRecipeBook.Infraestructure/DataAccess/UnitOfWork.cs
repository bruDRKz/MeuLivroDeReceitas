using MyRecipeBook.Domain.Repositories;

namespace MyRecipeBook.Infraestructure.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyRecipeBookDbContext _context;
        public UnitOfWork(MyRecipeBookDbContext context) => _context = context;

        //Salva as alterações no banco de dados, é importante separar esse commit das operações de repositório
        //para garantir a atomicidade das operações.
        public async Task Commit() => await _context.SaveChangesAsync();
    }
}
