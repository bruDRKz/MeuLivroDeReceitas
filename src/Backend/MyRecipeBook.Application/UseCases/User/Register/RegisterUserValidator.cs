using FluentValidation;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Exceptions;

namespace MyRecipeBook.Application.UseCases.User.Register
{
    public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
    {
        public RegisterUserValidator()
        {
            // Crio uma classe de validação para o RequestRegisterUserJson com a biblioteca FluentValidation, onde posso definir regras para cada propriedade do objeto
            // e personalizar as mensagens de erro -> uso um Resource dentro do projeto de exceções para utilizar uma forma de exceções dinamicas.
            // O FluentValidation é interessante de ser usado em combinação com os testes de unidade


            RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceMessagesExceptions.NAME_EMPTY);
            RuleFor(user => user.Email).NotEmpty().WithMessage(ResourceMessagesExceptions.EMAIL_EMPTY);            
            RuleFor(user => user.Password.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMessagesExceptions.PASSWORD_EMPTY);

            When(user => string.IsNullOrEmpty(user.Email) == false, () =>
            {
                RuleFor(user => user.Email).EmailAddress().WithMessage(ResourceMessagesExceptions.EMAIL_INVALID);
            });
        }
    }
}
