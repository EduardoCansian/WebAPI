using WebAPI.Dto.Autor;
using WebAPI.Dto.Livro;
using WebAPI.Models;

namespace WebAPI.Services.Livro
{
    public interface ILivroInterface
    {
        Task<ResponseModel<List<LivroModel>>> ListarLivros();  //Método para listar autores usando List<>
        Task<ResponseModel<LivroModel>> BuscarLivroPorId(int idLivro);  //Método para buscar autor pelo id
        Task<ResponseModel<List<LivroModel>>> BuscarLivroPorIdAutor(int idAutor); //Método para buscar autor pelo id do livro que pertence ao mesmo
        Task<ResponseModel<List<LivroModel>>> CriarLivro(LivroCriacaoDto livroCriacaoDto);  //LivroCricaoDto é usada pra exibir só o nome e sobrenome do autor no cadastro
        Task<ResponseModel<List<LivroModel>>> EditarLivro(LivroEdicaoDto livroEdicaoDto);
        Task<ResponseModel<List<LivroModel>>> ExcluirLivro(int idLivro);
    }
}
