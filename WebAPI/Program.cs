using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Services.Autor;
using WebAPI.Services.Livro;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAutorInterface, AutorService>();    //Estabelecendo a relação entre as classes de Autor
builder.Services.AddScoped<ILivroInterface, LivroService>();    //Estabelecendo a relação entre as classes de Livro

builder.Services.AddDbContext<AppDbContext>(options =>      //Aqui estabelecemos a conexão com o banco de dados quando iniciarmos o programa
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));   //Indicando para o Program.cs a string de conexão
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
