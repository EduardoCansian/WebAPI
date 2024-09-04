namespace WebAPI.Models
{
    public class LivroModel     //Classe que servirá de tabela para o banco de dados e vai se relacionar com a tabela Autor
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public AutorModel Autor { get; set; }   //Propriedade que será atribuido um autor para um livro

    }
}
