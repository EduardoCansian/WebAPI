using WebAPI.Dto.Vinculo;
using WebAPI.Models;

namespace WebAPI.Dto.Livro
{
    public class LivroCriacaoDto
    {
        public string Titulo { get; set; }
        public AutorVinculoDto Autor { get; set; }
    }
}
