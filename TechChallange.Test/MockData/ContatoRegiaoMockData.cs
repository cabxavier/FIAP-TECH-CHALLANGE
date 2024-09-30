using CORE.Entity;
using CORE.Input;

namespace TechChallange.Test.MockData
{
        public class ContatoRegiaoMockData 
    {

        public static List<ContatoRegiao> GetAll()
        {
            return new List<ContatoRegiao>
        {
            new ContatoRegiao
            {
                ContatoId = 1,
                RegiaoId = 101,
                Contato = new Contato
                {
                    Id = 1,
                    Nome = "Contato_1",
                    Telefone = "1411111111",
                    Email = "contato1@gmail.com"
                },
                Regiao = new Regiao
                {
                    Id = 101,
                   Ddd = "Região Norte"
                }
            },
            new ContatoRegiao
            {
                ContatoId = 2,
                RegiaoId = 102,
                Contato = new Contato
                {
                    Id = 2,
                    Nome = "Contato_2",
                    Telefone = "1422222222",
                    Email = "contato2@gmail.com"
                },
                Regiao = new Regiao
                {
                    Id = 102,
                    Ddd= "Região Sul"
                }
            },
            new ContatoRegiao
            {
                ContatoId = 3,
                RegiaoId = 103,
                Contato = new Contato
                {
                    Id = 3,
                    Nome = "Contato_3",
                    Telefone = "1433333333",
                    Email = "contato3@gmail.com"
                },
                Regiao = new Regiao
                {
                    Id = 104,
                    Ddd= "Região Leste"
                }
            }
        };
        }

            public static List<ContatoRegiao> GetAllVazio()
            {
                return new List<ContatoRegiao>();
            }
        public static ContatoRegiaoInput ContatoRegiaoInput()
        {
            return new ContatoRegiaoInput
            {
                ContatoId = 1,
                RegiaoId = 1,
            };
        }
        public static ContatoRegiaoInputUpdate ContatoRegiaoInputUpdate() 
        {
            return new ContatoRegiaoInputUpdate
            { 
                ContatoId = 2,
                RegiaoId = 2, 
                Id = 2,
            };
         }

        internal static ContatoRegiao ContatoRegiao()
        {
            throw new NotImplementedException();
        }
    }
}
