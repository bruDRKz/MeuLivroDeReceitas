using AutoMapper;
using MyRecipeBook.Application.Services.Criptografia;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.User.Register
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    { 
        private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PasswordEncripter _passwordEncripter;
        public RegisterUserUseCase(IUserWriteOnlyRepository userWriteOnlyRepository, 
                                   IUserReadOnlyRepository userReadOnlyRepository,
                                   IUnitOfWork unitOfWork,
                                   PasswordEncripter passwordEncripter,
                                   IMapper mapper)
        {
            _userReadOnlyRepository = userReadOnlyRepository;
            _userWriteOnlyRepository = userWriteOnlyRepository;
            _unitOfWork = unitOfWork;
            _passwordEncripter = passwordEncripter;
            _mapper = mapper;
            
        }
        public async Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request)
        { 
            await Validate(request);           

            var user = _mapper.Map<Domain.Entities.User>(request);
            user.Password = _passwordEncripter.Encrypt(request.Password);

            await _userWriteOnlyRepository.AddUserAsync(user);
            await _unitOfWork.Commit();

            return new ResponseRegisterUserJson
            {
                Name = user.Name,
            };
        }

        private async Task Validate(RequestRegisterUserJson request)
        {
            var validator = new RegisterUserValidator();

            var result = validator.Validate(request);

            var emailExiste = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);
            if (emailExiste)
            {
                // Adiciona a mensagem de erro de validação se o e-mail já estiver em uso
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesExceptions.EMAIL_INVALID));
            }

            if (!result.IsValid)
            {                
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errors);
            }            
        }
    }
}
