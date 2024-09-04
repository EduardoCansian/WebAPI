namespace WebAPI.Models
{
    public class ResponseModel<T>   // "T" significa que o tipo será genérico, onde o ResponseModel será válido para Autores e Livros
    {
        public T? Dados { get; set; }    // O "T" é o tipo para termos tanto dados de Autor quanto para Livros

        public string Mensagem { get; set; } = string.Empty;

        public bool Status { get; set; } = true;


    }
}
