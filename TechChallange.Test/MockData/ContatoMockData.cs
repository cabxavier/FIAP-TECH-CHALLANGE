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
                    Nome = "Cesar_1",
                    Telefone = "1434962861",
                    Email = "cesar1@gmail.com"
                },
                new Contato{
                    Id = 2,
                    Nome = "Cesar_2",
                    Telefone = "1434962862",
                    Email = "cesar2@gmail.com"
                },
                new Contato{
                    Id = 3,
                    Nome = "Cesar_3",
                    Telefone = "1434962863",
                    Email = "cesar3@gmail.com"
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
                Nome = "Cesar_1",
                Telefone = "1434962861",
                Email = "cesar1@gmail.com"
            };
        }

        public static ContatoInput ContatoInputNovo()
        {
            return new ContatoInput
            {
                Nome = "Cesar_1",
                Telefone = "1434962861",
                Email = "cesar1@gmail.com"
            };
        }
    }
}
