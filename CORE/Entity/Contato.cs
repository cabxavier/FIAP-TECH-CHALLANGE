﻿namespace CORE.Entity
{
    public class Contato : EntityBase
    {
        public required string Nome { get; set; }
        public required string Telefone { get; set; }
        public string Email { get; set; }

        public virtual ICollection<ContatoRegiao> ContatosRegioes { get; set; }
    }
}
