using CORE.Entity;
using CORE.Input;

namespace TechChallange.Test.MockData
{
    public class RegiaoMockData
    {
        public static List<Regiao> GetRegiaoAll()
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
                },
                new Regiao
                {
                    Id = 4,
                    Ddd = "41",
                    ContatosRegioes = new List<ContatoRegiao>()
                }
            };
        }

        public static List<Regiao> GetRegiaoVazia()
        {
            return new List<Regiao>();
        }

        public static Regiao RegiaoNovo()
        {
            return new Regiao
            {
                Id = 1,
                Ddd = "11",
                ContatosRegioes = new List<ContatoRegiao>()
            };
        }

        public static RegiaoInput RegiaoInputNovo()
        {
            return new RegiaoInput
            {
                Ddd = "11"
            };
        }
    }
}
