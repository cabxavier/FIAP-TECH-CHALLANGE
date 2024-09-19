namespace CORE.Input
{
    public class ContatoUpdateInput
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Telefone { get; set; }
        public required string Email { get; set; }
    }
}
