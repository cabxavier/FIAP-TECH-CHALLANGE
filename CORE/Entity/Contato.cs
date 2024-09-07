namespace CORE.Entity
{
    public class Contato : EntityBase
    {
        public int IdContato { get; set; }
        public required string Nome { get; set; }
        public required string Telefone { get; set; }
        public string? Email { get; set; }

        public ICollection<ContatoRegiao> ContatosRegioes { get; set; }=new List<ContatoRegiao>();
    }
}
