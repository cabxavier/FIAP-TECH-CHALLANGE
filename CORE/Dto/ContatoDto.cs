namespace CORE.Dto
{
    public class ContatoDto
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Telefone { get; set; }
        public string? Email { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
