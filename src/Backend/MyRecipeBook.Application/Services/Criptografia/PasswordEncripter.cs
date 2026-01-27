using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;

namespace MyRecipeBook.Application.Services.Criptografia
{
    public class PasswordEncripter
    {
        //encripta a senha utilizando SHA-512, depois retorna a string em hexadecimal — A senha é encriptada, não criptografada

        private readonly string _additionalKey;
        public PasswordEncripter(string additionalKey) => _additionalKey = additionalKey;

        public string Encrypt(string password)
        {           
            var newPassword = $"{password}{_additionalKey}"; 
            password = newPassword;

            var bytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = SHA512.HashData(bytes);

            return StringBytes(hashBytes);
        }

        //Converte o array de bytes para string hexadecimal
        private static string StringBytes(byte[] bytes)
        { 
            var sb = new StringBuilder();
            foreach (byte b in bytes)
            { 
                var hex = b.ToString("x2");
                sb.Append(hex);
            }

            return sb.ToString();
        }
    }
}
