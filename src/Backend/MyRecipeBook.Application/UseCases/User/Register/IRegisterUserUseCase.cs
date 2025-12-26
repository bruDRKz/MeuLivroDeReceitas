using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Application.UseCases.User.Register
{
    public interface IRegisterUserUseCase
    {
        public Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request);
    }
}
