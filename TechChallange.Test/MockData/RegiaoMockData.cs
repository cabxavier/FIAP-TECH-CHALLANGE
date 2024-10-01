using CORE.Entity;
using CORE.Input;

namespace TechChallange.Test.MockData
{
    public class RegiaoMockData
    {
        public static List<Regiao> GetAll()
        {
            return new List<Regiao>
            {
                new Regiao
                {
                    Id = 1,
                    Ddd = "11",
                    ContatosRegioes = new List<ContatoRegiao>()
                },
                new Regiao
                {
                    Id = 2,
                    Ddd = "21",
                    ContatosRegioes = new List<ContatoRegiao>()
                },
                new Regiao
                {
                    Id = 3,
                    Ddd = "31",
                    ContatosRegioes = new List<ContatoRegiao>()
                }
            };
        }

        public static List<Regiao> GetAllVazio()
        {
            return new List<Regiao>();
        }

        public static Regiao Regiao()
        {
            return new Regiao
            {
                Id = 1,
                Ddd = "11",
                ContatosRegioes = new List<ContatoRegiao>()
            };
        }

        public static RegiaoInput RegiaoInput()
        {
            return new RegiaoInput
            {
                Ddd = "11"
            };
        }

        public static RegiaoInputUpdate RegiaoInputUpdate()
        {
            return new RegiaoInputUpdate
            {
                Id = 1,
                Ddd = "14"
            };
        }
    }
}
