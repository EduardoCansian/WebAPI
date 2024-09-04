using WebAPI.Dto.Autor;
using WebAPI.Models;

namespace WebAPI.Services.Autor
{
    public interface IAutorInterface    //Classe que terá os métodos
    {
        Task<ResponseModel<List<AutorModel>>> ListarAutores();  //Método para listar autores usando List<>
        Task<ResponseModel<AutorModel>> BuscarAutorPorId(int idAutor);  //Método para buscar autor pelo id
        Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(int idLivro); //Método para buscar autor pelo id do livro que pertence ao mesmo
        Task<ResponseModel<List<AutorModel>>> CriarAutor(AutorCriacaoDto autorCriacaoDto);  //AutorCricaoDto é usada pra exibir só o nome e sobrenome do autor no cadastro
        Task<ResponseModel<List<AutorModel>>> EditarAutor(AutorEdicaoDto autorEdicaoDto);
        Task<ResponseModel<List<AutorModel>>> ExcluirAutor(int idAutor);

    }
}
