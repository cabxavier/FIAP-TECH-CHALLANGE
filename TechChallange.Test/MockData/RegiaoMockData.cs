using CORE.Entity;
using CORE.Input;
using Microsoft.Identity.Client;


namespace TechChallange.Test.MockData
{
    public class RegiaoMockData
    {
        public static List<Regiao> GetRegiaos()
        {
            return new List<Regiao>
            {
                new Regiao
                {
                    Id = 1, // Defina um Id, se aplicável
                    Ddd = "11",
                    ContatosRegioes = new List<ContatoRegiao>() // Adicione contatos se necessário
                },
                new Regiao
                {
                    Id = 2,
                    Ddd = "21",
                    ContatosRegioes = new List<ContatoRegiao>() // Adicione contatos se necessário
                },
                new Regiao
                {
                    Id = 3,
                    Ddd = "31",
                    ContatosRegioes = new List<ContatoRegiao>() // Adicione contatos se necessário
                },
                new Regiao
                {
                    Id = 4,
                    Ddd = "41",
                    ContatosRegioes = new List<ContatoRegiao>() // Adicione contatos se necessário
                }

            };

          
        }   
    }
}
