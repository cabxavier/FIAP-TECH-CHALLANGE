namespace CORE.Dto
{
    public class RegiaoDto
    {
        public int Id { get; set; }
        public required string Ddd { get; set; }
        public DateTime DataCriacao { get; set; }

        public ICollection<ContatoDto> Contatos { get; set; }
    }
}
