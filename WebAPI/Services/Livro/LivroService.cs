using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebAPI.Data;
using WebAPI.Dto.Autor;
using WebAPI.Dto.Livro;
using WebAPI.Models;

namespace WebAPI.Services.Livro
{
    public class LivroService : ILivroInterface
    {
        private readonly AppDbContext _context;

        public LivroService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<LivroModel>> BuscarLivroPorId(int idLivro)
        {
            ResponseModel<LivroModel> resposta = new ResponseModel<LivroModel>();
            try
            {
                var livro = await _context.Livros.Include(a => a.Autor)
                    .FirstOrDefaultAsync(livroBanco => livroBanco.Id == idLivro);   //Verifica linha por linha se o autor que está procurando pelo BuscarAutorPorId existe no banco

                if (livro == null)   //Se o livro não for encontrado
                {
                    resposta.Mensagem = "Nenhum registro foi localizado";
                    return resposta;
                }
                else
                {
                    resposta.Dados = livro;
                    resposta.Mensagem = "Livro localizado";

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

        public async Task<ResponseModel<List<LivroModel>>> BuscarLivroPorIdAutor(int idAutor)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try
            {
                var livro = await _context.Livros.Include(a => a.Autor).Where(livroBanco => livroBanco.Autor.Id == idAutor).ToListAsync();

                if (livro == null)
                {
                    resposta.Mensagem = "Nenhum livro foi encontrado";
                    return resposta;
                }

                else
                {
                    resposta.Dados = livro;

                    resposta.Mensagem = "O livro foi localizado com sucesso";
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

        public async Task<ResponseModel<List<LivroModel>>> CriarLivro(LivroCriacaoDto livroCriacaoDto)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();

            try
            {

                var autor = await _context.Autores.FirstOrDefaultAsync(autorBanco => autorBanco.Id == livroCriacaoDto.Autor.Id);

                if(autor == null)
                {
                    resposta.Mensagem = "Nenhum registro de autor foi encontrado";
                }

                var livro = new LivroModel()
                {
                    Titulo = livroCriacaoDto.Titulo,
                    Autor = autor,

                };

                _context.Add(livro);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Livros.Include(a => a.Autor).ToListAsync(); 
                resposta.Mensagem = "Livro criado com sucesso";

                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> EditarLivro(LivroEdicaoDto livroEdicaoDto)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();

            try
            {

                var livro = await _context.Livros.Include(a => a.Autor).FirstOrDefaultAsync(livroBanco => livroBanco.Id == livroEdicaoDto.Id);
                var autor = await _context.Autores.FirstOrDefaultAsync(autorBanco => autorBanco.Id == livroEdicaoDto.Autor.Id);

                if (autor == null)
                {
                    resposta.Mensagem = "Nenhum autor foi encontrado";
                    return resposta;
                }

                if (livro == null)
                {
                    resposta.Mensagem = "Nenhum livro foi encontrado";
                    return resposta;
                }
                else
                {
                    livro.Titulo = livroEdicaoDto.Titulo;
                    livro.Autor = autor;

                    _context.Update(livro);
                    await _context.SaveChangesAsync();

                    resposta.Dados = await _context.Livros.ToListAsync();
                    resposta.Mensagem = "Livro editado com sucesso";
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

        public async Task<ResponseModel<List<LivroModel>>> ExcluirLivro(int idLivro)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();

            try
            {

                var livro = await _context.Livros.Include(a => a.Autor).FirstOrDefaultAsync(livroBanco => livroBanco.Id == idLivro);

                if (livro == null)
                {
                    resposta.Mensagem = "Nenhum livro foi encontrado";
                    return resposta;
                }
                else
                {
                    _context.Remove(livro);
                    await _context.SaveChangesAsync();

                    resposta.Dados = await _context.Livros.ToListAsync();
                    resposta.Mensagem = "Livro removido com sucesso";

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

        public async Task<ResponseModel<List<LivroModel>>> ListarLivros()
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try
            {

                var livros = await _context.Livros.Include(a => a.Autor).ToListAsync(); //Include() é usado para exibir os autores junto com os livros

                resposta.Dados = livros;
                resposta.Mensagem = "Todos os livros foram coletados";

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
