using System.Collections;

namespace WebApi.Test.InlineData
{
    public class CultureInlineDataTests : IEnumerable<object[]>
    {
        // Esta classe é utilizada para fornecer dados de teste para os testes de unidade, especificamente para testar diferentes culturas (localizações) na API.
        // Ela implementa a interface IEnumerable<object[]> para permitir que os testes sejam executados com diferentes conjuntos de dados.
        // Torna mais fácil testar a API com diferentes culturas e adicionar novas culturas no futuro, basta adicionar mais entradas ao método GetEnumerator.
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "en" };
            yield return new object[] { "pt-PT" };
            yield return new object[] { "pt-BR" };
            yield return new object[] { "fr" };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }
}
