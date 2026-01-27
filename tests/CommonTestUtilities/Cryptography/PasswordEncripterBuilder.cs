using MyRecipeBook.Application.Services.Criptografia;

namespace CommonTestUtilities.Cryptography
{
    public class PasswordEncripterBuilder
    {
        public static PasswordEncripter Build() => new PasswordEncripter("testes");
    }
}
