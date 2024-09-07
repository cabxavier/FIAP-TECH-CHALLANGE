namespace CORE.Entity
{
    public class ContatoRegiao : EntityBase
    {
        public int IdContatoRegiao { get; set; }
        public int IdContato {  get; set; }
        public int IdRegiao { get; set; }

        public required Contato Contato { get; set; }
        public required Regiao Regiao { get; set; }
    }
}
