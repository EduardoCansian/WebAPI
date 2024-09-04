using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class AppDbContext : DbContext      //Classe para fazer a conexão com o banco de dados
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)     //Será feita a conexão e receberá as informações
        {

        }

        public DbSet<AutorModel> Autores { get; set; }      //Aqui será criada a tabela no banco de dados com seu nome
        public DbSet<LivroModel> Livros { get; set; }

        
    }
}
