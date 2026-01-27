using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using FluentAssertions;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace UseCases.Test.User.Register
{
    public class RegisterUserUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var request = RequestRegisterUserJsonBuilder.Build();            
            var useCase = CreateUseCase();
            var result = await useCase.Execute(request);

            result.Should().NotBeNull();
            result.Name.Should().Be(request.Name);
        }

        [Fact]
        public async Task Error_Email_Already_Registered()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            // Simula que já existe um usuário ativo com o email fornecido — Aproveito o email que o Bogus gerou para manter a consistência dos dados.
            var useCase = CreateUseCase(request.Email);

            // Salvo uma função assíncrona que executa o caso de uso para facilitar a verificação da exceção lançada.
            Func<Task> act = async () => { await useCase.Execute(request); };
            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorMessages.Count == 1 && e.ErrorMessages.Contains(ResourceMessagesExceptions.EMAIL_INVALID));
        }

        [Fact]
        public async Task Error_Name_Empty()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Name = string.Empty;

            var useCase = CreateUseCase();

            Func<Task> act = async () => { await useCase.Execute(request); };

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorMessages.Count == 1 && e.ErrorMessages.Contains(ResourceMessagesExceptions.NAME_EMPTY));
        }

        // O email é um parametro opcional para facilitar a reutilização do método CreateUseCase em outros testes  
        private RegisterUserUseCase CreateUseCase(string? email = null)
        {
            var mapper = MapperBuilder.Build();
            var passwordEncripter = PasswordEncripterBuilder.Build();
            var writeOnlyRepository = UserWriteOnlyRepositoryBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();

            // Builder que encapsula a criação de um mock (que é uma instância concreta do repositório)
            var readOnlyRepositoryBuilder = new UserReadOnlyRepositoryBuilder();

            // Como o email é um parametro opcional, só adiciona a configuração no mock se o email for fornecido.
            if (email != null)            
               readOnlyRepositoryBuilder.ExistActiveUserWithEmail(email);

            // Constrói o mock do repositório de leitura e injeta todas as dependências no use case
            return new RegisterUserUseCase(writeOnlyRepository, readOnlyRepositoryBuilder.Build(), unitOfWork, passwordEncripter, mapper);

        }
    }
}
