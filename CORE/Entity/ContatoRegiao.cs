namespace CORE.Entity
{
    public class ContatoRegiao : EntityBase
    {
        public int ContatoId { get; set; }
        public int RegiaoId { get; set; }

        public virtual Contato Contato { get; set; }
        public virtual Regiao Regiao { get; set; }
    }
}
