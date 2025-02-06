using CORE.Entity;
using CORE.Repository;
using MassTransit;

namespace TechChallange.Core.ServiceRabbitMQ
{
    public class ContatoRegiaoDeleteConsumidor : IConsumer<ContatoRegiao>
    {
        private readonly IContatoRegiaoRepository contatoRegiaoRepository;

        public ContatoRegiaoDeleteConsumidor(IContatoRegiaoRepository contatoRegiaoRepository)
        {
            this.contatoRegiaoRepository = contatoRegiaoRepository;
        }

        public async Task Consume(ConsumeContext<ContatoRegiao> context)
        {
            try
            {
                if (context is null)
                {
                    throw new Exception("Não foi possível obter as informações da filacontatoregiaodelete.");
                }

                if (context.Message.Id == 0)
                {
                    throw new Exception("Não foi possível obter as informações do identificador.");
                }

                await this.contatoRegiaoRepository.DeleteAsync(context.Message.Id);

                Console.WriteLine();

                Console.WriteLine($"ContatoRegiao excluído - Id: {context.Message.Id}");

                Console.WriteLine();
            }
            catch
            {
                throw;
            }
        }
    }
}
