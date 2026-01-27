using Moq;
using MyRecipeBook.Domain.Repositories.User;

namespace CommonTestUtilities.Repositories
{
    public class UserWriteOnlyRepositoryBuilder
    {
        public static IUserWriteOnlyRepository Build()
        {
            // Cria um mock do IUserWriteOnlyRepository -> Um mock é um objeto simulado que imita o comportamento de objetos reais em testes.
            var mock = new Mock<IUserWriteOnlyRepository>();
            return mock.Object;
        }
    }
}
