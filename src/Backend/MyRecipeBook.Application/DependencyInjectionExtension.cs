using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Application.Services.AutoMapper;
using MyRecipeBook.Application.Services.Criptografia;
using MyRecipeBook.Application.UseCases.User.Register;

namespace MyRecipeBook.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AddPasswordEncripter(services, configuration);
            AddAutoMapper(services);
            AddUseCases(services);            
        }
        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddScoped(option => new AutoMapper.MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapping());
            }).CreateMapper());
        }
        private static void AddUseCases(IServiceCollection services)
        {
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        }
        private static void AddPasswordEncripter(IServiceCollection services, IConfiguration configuration)
        {
            //Entro no objeto de configuração que criei e sigo o caminho até a chave, objeto a objeto.
            var additionalKey = configuration.GetValue<string>("Settings:Password:AdditionalKey"); 
            services.AddScoped(option => new PasswordEncripter(additionalKey!));
        }
    }
}
