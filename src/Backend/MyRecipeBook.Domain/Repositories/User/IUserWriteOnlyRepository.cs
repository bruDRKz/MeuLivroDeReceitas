using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Domain.Repositories.User
{
    public interface IUserWriteOnlyRepository
    {
        public Task AddUserAsync(Entities.User user);
    }
}
