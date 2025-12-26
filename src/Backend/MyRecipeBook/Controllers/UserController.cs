using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Communication.Requests;

namespace MyRecipeBook.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ProducesResponseType(typeof(ResponseRegisterUserJson), StatusCodes.Status201Created)]
    public class UserController : ControllerBase
    {
        [HttpPost]  
        public async Task<IActionResult> Register(
            [FromServices] IRegisterUserUseCase useCase,
            [FromBody] RequestRegisterUserJson request)            
        {            
            var result = await useCase.Execute(request);
            return Created(string.Empty, result); //retorno uma string vazia porque esse parametro não é importante
        
        }
    }
}
