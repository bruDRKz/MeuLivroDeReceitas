using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRecipeBook.Communication.Reponses;
using MyRecipeBook.Communication.Requests;

namespace MyRecipeBook.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ProducesResponseType(typeof(ResponseRegisterUserJson), StatusCodes.Status201Created)]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public IActionResult Register(RequestRegisterUserJson request)
        {

            return Created();
        }
    }
}
