using CORE.Entity;
using CORE.Input;

namespace TechChallange.Test.MockData
{
    public class ContatoMockData
    {
        public static List<Contato> GetContatoAll()
        {
            return new List<Contato>
            {
                new Contato{
                    Id = 1,
                    Nome = "Contato_1",
                    Telefone = "1411111111",
                    Email = "contato1@gmail.com"
                },
                new Contato{
                    Id = 2,
                    Nome = "Contato_2",
                    Telefone = "1422222222",
                    Email = "contato2@gmail.com"
                },
                new Contato{
                    Id = 3,
                    Nome = "Contato_3",
                    Telefone = "1433333333",
                    Email = "contato3@gmail.com"
                }
            };
        }

        public static List<Contato> GetContatoVazia()
        {
            return new List<Contato>();
        }

        public static Contato ContatoNovo()
        {
            return new Contato
            {
                Id = 1,
                Nome = "Contato_1",
                Telefone = "1411111111",
                Email = "contato1@gmail.com"
            };
        }

        public static ContatoInput ContatoInputNovo()
        {
            return new ContatoInput
            {
                Nome = "Contato_1",
                Telefone = "1411111111",
                Email = "contato1@gmail.com"
            };
        }
    }
}
