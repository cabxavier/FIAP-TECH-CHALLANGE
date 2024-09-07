namespace CORE.Entity
{
    public class Regiao : EntityBase
    {
        public int IdRegiao { get; set; }
        public required int Ddd {get;set;}

        public ICollection<ContatoRegiao> ContatosRegioes { get; set; } = new HashSet<ContatoRegiao>();
    }
}
