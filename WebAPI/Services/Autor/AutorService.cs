using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using WebAPI.Data;
using WebAPI.Dto.Autor;
using WebAPI.Models;

namespace WebAPI.Services.Autor
{
    public class AutorService : IAutorInterface
    {
        private readonly AppDbContext _context;
        public AutorService(AppDbContext context)   //Construtor para ter acesso ao banco de dados
        {
            _context = context;
        }


        public async Task<ResponseModel<AutorModel>> BuscarAutorPorId(int idAutor)
        {
            ResponseModel<AutorModel> resposta = new ResponseModel<AutorModel>();
            try
            {
                var autor = await _context.Autores.FirstOrDefaultAsync(autorBanco => autorBanco.Id == idAutor);   //Verifica linha por linha se o autor que está procurando pelo BuscarAutorPorId existe no banco

                if(autor == null)   //Se o autor não for encontrado
                {
                    resposta.Mensagem = "Nenhum registro foi localizado";
                    return resposta;
                }
                else
                {
                    resposta.Dados = autor;
                    resposta.Mensagem = "Autor localizado";

                    return resposta;
                }
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(int idLivro)
        {
            ResponseModel<AutorModel> resposta = new ResponseModel<AutorModel>();
            try
            {
                var livro = await _context.Livros.Include(a => a.Autor)     //Acessamos a tabela Livros e com o Include acessamos os dados de AutorModel
                    .FirstOrDefaultAsync(livroBanco => livroBanco.Id == idLivro);   //Acessamos cada id de livro para fazer relação com o autor

                if (livro == null)
                {
                    resposta.Mensagem = "Nenhum autor foi encontrado";
                    return resposta;
                }

                else
                {
                    resposta.Dados = livro.Autor;

                    resposta.Mensagem = "O autor foi localizado com sucesso";
                    return resposta;
                }
                

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<AutorModel>>> CriarAutor(AutorCriacaoDto autorCriacaoDto)
        {
            ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();

            try
            {
                var autor = new AutorModel()
                {
                    Nome = autorCriacaoDto.Nome,
                    Sobrenome = autorCriacaoDto.Sobrenome
                };

                _context.Add(autor);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Autores.ToListAsync();
                resposta.Mensagem = "Autor criado com sucesso";

                return resposta;

            }
            catch(Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<AutorModel>>> EditarAutor(AutorEdicaoDto autorEdicaoDto)
        {
            ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();

            try
            {

                var autor = await _context.Autores.FirstOrDefaultAsync(autorBanco => autorBanco.Id == autorEdicaoDto.Id);

                if (autor == null)
                {
                    resposta.Mensagem = "Nenhum autor foi encontrado";
                    return resposta;
                }
                else
                {
                    autor.Nome = autorEdicaoDto.Nome;
                    autor.Sobrenome = autorEdicaoDto.Sobrenome;

                    _context.Update(autor);
                    await _context.SaveChangesAsync();

                    resposta.Dados = await _context.Autores.ToListAsync();
                    resposta.Mensagem = "Autor editado com sucesso";
                    return resposta;

                }

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<AutorModel>>> ExcluirAutor(int idAutor)
        {
            ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();

            try
            {

                var autor = await _context.Autores.FirstOrDefaultAsync(autorBanco => autorBanco.Id == idAutor);

                if(autor == null)
                {
                    resposta.Mensagem = "Nenhum autor foi encontrado";
                    return resposta;
                }
                else
                {
                    _context.Remove(autor);
                    await _context.SaveChangesAsync();

                    resposta.Dados = await _context.Autores.ToListAsync();
                    resposta.Mensagem = "Autor removido com sucesso";

                    return resposta;

                }

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<AutorModel>>> ListarAutores()
        {
            ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();
            try
            {

                var autores = await _context.Autores.ToListAsync();

                resposta.Dados = autores;
                resposta.Mensagem = "Todos os autores foram coletados";

                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }
    }
}
