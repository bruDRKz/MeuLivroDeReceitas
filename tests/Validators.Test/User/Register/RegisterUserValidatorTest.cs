using CommonTestUtilities.Requests;
using FluentAssertions;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Exceptions;

namespace Validators.Test.User.Register
{
    public class RegisterUserValidatorTest
    {
        // Sucesso em todas as validações.
        [Fact]
        public void Success()
        {
            var validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();            

            var result = validator.Validate(request);
            
            
            // Nesse caso eu uso o FluentAssertions para fazer o Assert, mas tambem pode ser feito da seguinte forma:
            // -> Assert.True(result.IsValid);
            // O Fluent Assertions se tornou pago em Jan/25, então somente posso usá-lo em projetos open-source ou para estudos

            result.IsValid.Should().BeTrue();
        }

        // Erro de nome vazio.
        [Fact]
        public void Error_Name_Empty()
        {
            var validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();
            request.Name = string.Empty;

            var result = validator.Validate(request);

            // Faço o assert para verificar se a validação falhou e se a mensagem de erro está correta.
            result.IsValid.Should().BeFalse();

            // Garanto que existe apenas um erro e que a mensagem de erro corresponde à esperada.
            result.Errors.Should().ContainSingle()
                .And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesExceptions.NAME_EMPTY));
        }

        // Erro de email vazio.
        [Fact]
        public void Error_Email_Empty()
        {
            var validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();
            request.Email = string.Empty;

            var result = validator.Validate(request);

            // Faço o assert para verificar se a validação falhou e se a mensagem de erro está correta.
            result.IsValid.Should().BeFalse();

            // Garanto que existe apenas um erro e que a mensagem de erro corresponde à esperada.
            result.Errors.Should().Contain(e => e.ErrorMessage.Equals(ResourceMessagesExceptions.EMAIL_EMPTY));
        }

        // Erro de email inválido.
        [Fact]
        public void Error_Email_Invalid()
        {
            var validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();
            request.Email = "email.invalido";

            var result = validator.Validate(request);

            // Faço o assert para verificar se a validação falhou e se a mensagem de erro está correta.
            result.IsValid.Should().BeFalse();

            // Garanto que existe apenas um erro e que a mensagem de erro corresponde à esperada.
            result.Errors.Should().Contain(e => e.ErrorMessage.Equals(ResourceMessagesExceptions.EMAIL_INVALID));
        }

        // Erro de senha inválida.
        // O Theory permite rodar o mesmo teste com diferentes parâmetros, várias vezes.
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void Error_Password_Invalid(int PasswordLength)
        {
            var validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build(PasswordLength);            

            var result = validator.Validate(request);

            // Faço o assert para verificar se a validação falhou e se a mensagem de erro está correta.
            result.IsValid.Should().BeFalse();

            // Garanto que existe apenas um erro e que a mensagem de erro corresponde à esperada.
            // A mensagem que passo está propositalmente errada, sempre mando a de senha vazia, pois não finalizei os resources de mensagens de erro.
            result.Errors.Should().Contain(e => e.ErrorMessage.Equals(ResourceMessagesExceptions.PASSWORD_EMPTY));
        }
    }
}
