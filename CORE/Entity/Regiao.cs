namespace CORE.Entity
{
    public class Regiao : EntityBase
    {
        public required string Ddd { get; set; }

        public virtual ICollection<ContatoRegiao> ContatosRegioes { get; set; }
    }
}
