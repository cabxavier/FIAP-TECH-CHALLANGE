namespace CORE.Entity
{
    public class Regiao : EntityBase
    {
        public required int Ddd { get; set; }

        public virtual ICollection<ContatoRegiao> ContatosRegioes { get; set; }
    }
}
