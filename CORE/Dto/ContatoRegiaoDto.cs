namespace CORE.Dto
{
    public class ContatoRegiaoDto
    {
        public int Id { get; set; }
        public int ContatoId { get; set; }
        public int RegiaoId { get; set; }
        public DateTime DataCriacao { get; set; }

        public virtual ContatoDto Contato { get; set; }
        public virtual RegiaoDto Regiao { get; set; }
    }
}
