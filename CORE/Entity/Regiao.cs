namespace CORE.Entity
{
    public class Regiao : EntityBase
    {
        public required int Ddd {get;set;}

        public ICollection<ContatoRegiao>? ContatosRegioes { get; set; } = new List<ContatoRegiao>();
    }
}
