using Moq;
using MyRecipeBook.Domain.Repositories;

namespace CommonTestUtilities.Repositories
{
    public class UnitOfWorkBuilder
    {
        public static IUnitOfWork Build()
        {
            // Cria um mock do IUnitOfWork -> Um mock é um objeto simulado que imita o comportamento de objetos reais em testes.
            var mock = new Mock<IUnitOfWork>();
            return mock.Object;
        }
    }
}
