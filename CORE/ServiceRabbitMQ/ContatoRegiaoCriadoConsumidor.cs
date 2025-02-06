using CORE.Entity;
using MassTransit;
using CORE.Repository;

namespace TechChallange.Core.ServiceRabbitMQ
{
    public class ContatoRegiaoCriadoConsumidor : IConsumer<ContatoRegiao>
    {
        private readonly IContatoRegiaoRepository contatoRegiaoRepository;

        public ContatoRegiaoCriadoConsumidor(IContatoRegiaoRepository contatoRegiaoRepository)
        {
            this.contatoRegiaoRepository = contatoRegiaoRepository;
        }

        public async Task Consume(ConsumeContext<ContatoRegiao> context)
        {
            try
            {
                var rotulo = string.Empty;

                if (context is null)
                {
                    throw new Exception("Não foi possível obter as informações da filacontatoregiao.");
                }

                var contatoRegiao = new ContatoRegiao()
                {
                    Id = context.Message.Id,
                    ContatoId = context.Message.ContatoId,
                    RegiaoId = context.Message.RegiaoId
                };

                if (contatoRegiao.Id == 0)
                {
                    await this.contatoRegiaoRepository.AddAsync(contatoRegiao);

                    rotulo = "ContatoRegiao criado";
                }
                else
                {
                    await this.contatoRegiaoRepository.UpdateAsync(contatoRegiao);

                    rotulo = "ContatoRegiao alterado";
                }

                Console.WriteLine();

                Console.WriteLine($"{rotulo} - Id: {context.Message.Id + " || ContatoId: " + context.Message.ContatoId + " || RegiaoId: " + context.Message.RegiaoId}");

                Console.WriteLine();
            }
            catch
            {
                throw;
            }
        }
    }
}
