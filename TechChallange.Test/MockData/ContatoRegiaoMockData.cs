using CORE.Dto;
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
                new ContatoRegiao{

                    ContatoId = 1,
                    RegiaoId = 1
                },
                new ContatoRegiao{

                    ContatoId = 1,
                    RegiaoId = 2
                },
                new ContatoRegiao{

                    ContatoId = 2,
                    RegiaoId = 3
                },
            };
        }

        public static List<ContatoRegiao> GetAllVazio()
        {
            return new List<ContatoRegiao>();
        }

        public static ContatoRegiaoDto GetContatoRegiaoById()
        {
            return new ContatoRegiaoDto()
            {
                Id = 1,
                ContatoId = 1,
                RegiaoId = 1,

                Contato = new ContatoDto()
                {
                    Id = 1,
                    Nome = "Contato_1",
                    Telefone = "1411111111",
                    Email = "contato1@gmail.com"
                },
                Regiao = new RegiaoDto()
                {
                    Id = 1,
                    Ddd = "11"
                }
            };
        }

        public static ContatoRegiao ContatoRegiao()
        {
            return new ContatoRegiao
            {
                Id = 1,
                ContatoId = 1,
                RegiaoId = 1
            };
        }

        public static ContatoRegiaoInput ContatoRegiaoInput()
        {
            return new ContatoRegiaoInput
            {
                ContatoId = 1,
                RegiaoId = 1
            };
        }

        public static ContatoRegiaoInputUpdate ContatoRegiaoInputUpdate()
        {
            return new ContatoRegiaoInputUpdate
            {
                Id = 1,
                ContatoId = 1,
                RegiaoId = 2
            };
        }
    }
}
