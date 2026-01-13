using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.User;

namespace MyRecipeBook.Infraestructure.DataAccess.Repositories
{
    public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository
    {
        private readonly MyRecipeBookDbContext _context;
        public UserRepository(MyRecipeBookDbContext context) =>  _context = context;

        public async Task AddUserAsync(User user) => await _context.Users.AddAsync(user);
        
        public async Task<bool> ExistActiveUserWithEmail(string email) => await _context.Users.AnyAsync(user => user.Email == email && user.Active);        
    }
}
