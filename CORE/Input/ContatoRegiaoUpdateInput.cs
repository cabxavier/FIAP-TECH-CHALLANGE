namespace CORE.Input
{
    public class ContatoRegiaoUpdateInput
    {
        public int Id { get; set; }
        public required int ContatoId { get; set; }
        public required int RegiaoId { get; set; }
    }
}
