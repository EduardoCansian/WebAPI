using System.Text.Json.Serialization;

namespace WebAPI.Models
{
    public class AutorModel     //Classe que servirá de tabela para o banco de dados
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        [JsonIgnore]    //Usado para ignorar a propriedade lista de livros somente na hora de cadastrar um novo autor
        public ICollection<LivroModel> Livros { get; set; }    //Propriedade que servirá de lista (Um autor pode ter vários livros)

    }
}
