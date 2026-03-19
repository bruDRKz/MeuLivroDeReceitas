using CommonTestUtilities.Requests;
using FluentAssertions;
using MyRecipeBook.Exceptions;
using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using WebApi.Test.InlineData;

namespace WebApi.Test.User.Register
{
    public class RegisterUserTest : IClassFixture<CustomWebApplicationFactory>
    {
        public readonly HttpClient _httpClient;
        public RegisterUserTest(CustomWebApplicationFactory factory)
        {
            // Cria um cliente HTTP para enviar requisições à API durante os testes.
            _httpClient = factory.CreateClient();
        }
        [Fact]
        public async Task Success()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            // Envia uma requisição POST para o endpoint "User" com os dados do usuário a ser registrado.

            var response = await _httpClient.PostAsJsonAsync("User", request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            // Lê o corpo da resposta. Não é bom que seja lido como string, pois dificulta a validação do conteúdo, por isso trato como stream.
            await using var responseBody = await response.Content.ReadAsStreamAsync();
            // Faz o parse do corpo da resposta para um documento JSON.
            var responseData = await JsonDocument.ParseAsync(responseBody);
            // Acesso o documento JSON para validar se o nome retornado é igual ao nome enviado na requisição e se não é nulo ou espaço em branco.
            responseData.RootElement.GetProperty("name").GetString().Should().NotBeNullOrWhiteSpace().And.Be(request.Name);
        }
        [Theory]
        [ClassData(typeof(CultureInlineDataTests))]
        public async Task Error_Empty_Name(string culture)
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Name = string.Empty;

            if (_httpClient.DefaultRequestHeaders.Contains("Accept-Language"))
                _httpClient.DefaultRequestHeaders.Remove("Accept-Language");

            _httpClient.DefaultRequestHeaders.Add("Accept-Language", culture);

            var response = await _httpClient.PostAsJsonAsync("User", request);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            await using var responseBody = await response.Content.ReadAsStreamAsync();
            var responseData = await JsonDocument.ParseAsync(responseBody);

            // Como a resposta de erro pode conter múltiplos erros, é necessário enumerar o array de erros para validar se a mensagem esperada está presente.
            var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

            // Não posso passar a mensagem de erro diretamente, pois ela é localizada e depende da cultura, por isso é necessário buscar a mensagem esperada no ResourceManager utilizando a cultura atual.
            var expectedMessage = ResourceMessagesExceptions.ResourceManager.GetString("NAME_EMPTY", new CultureInfo(culture));

            errors.Should().ContainSingle().And.Contain(error => error.GetString()!.Equals(expectedMessage));
        }
    }

}
