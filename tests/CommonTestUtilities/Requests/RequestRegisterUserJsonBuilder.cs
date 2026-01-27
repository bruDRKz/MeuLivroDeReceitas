using Bogus;
using MyRecipeBook.Communication.Requests;

namespace CommonTestUtilities.Requests
{
    public class RequestRegisterUserJsonBuilder
    {        
        public static RequestRegisterUserJson Build(int PasswordLength = 10) // => Uso um valor padrão de 10 para o comprimento da senha, para servir como parametro opcional.
        {
            return new Faker<RequestRegisterUserJson>()
                .RuleFor(user => user.Name, (f) => f.Person.FirstName)
                .RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Name))
                .RuleFor(user => user.Password, (f) => f.Internet.Password(PasswordLength));
        }
    }
}
