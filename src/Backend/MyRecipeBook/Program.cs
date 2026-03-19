using MyRecipeBook.Application;
using MyRecipeBook.Filters;
using MyRecipeBook.Infraestructure;
using MyRecipeBook.Infraestructure.Migrations;
using MyRecipeBook.Middleware;
using MyRecipeBook.Infraestructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CultureMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

MigrateDatabase();

app.Run();

void MigrateDatabase()
{
    if (builder.Configuration.IsUnitTestEnviroment())
        return;

    // Cria um escopo de serviço para realizar a migração do banco de dados -> Esse serviceScope é um contêiner temporário para resolver dependências necessárias para a migração.
    var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
    var connectionString = builder.Configuration.ConnectionString();
    DataBaseMigration.Migrate(connectionString, serviceScope.ServiceProvider);
}

public partial class Program 
{ 
}