namespace CORE.Dto
{
    public class RegiaoDto
    {
        public int Id { get; set; }
        public required int Ddd { get; set; }
        public DateTime DataCriacao { get; set; }

        public ICollection<ContatoRegiaoDto> ContatosRegioes { get; set; }
    }
}
