using CORE.Entity;
using CORE.Repository;
using MassTransit;

namespace TechChallange.Core.ServiceRabbitMQ
{
    public class RegiaoDeleteConsumidor : IConsumer<Regiao>
    {
        private readonly IRegiaoRepository regiaoRepository;

        public RegiaoDeleteConsumidor(IRegiaoRepository regiaoRepository)
        {
            this.regiaoRepository = regiaoRepository;
        }

        public async Task Consume(ConsumeContext<Regiao> context)
        {
            try
            {
                if (context is null)
                {
                    throw new Exception("Não foi possível obter as informações da filaregiaodelete.");
                }

                if (context.Message.Id == 0)
                {
                    throw new Exception("Não foi possível obter as informações do identificador.");
                }

                await this.regiaoRepository.DeleteAsync(context.Message.Id);

                Console.WriteLine();

                Console.WriteLine($"Regiao excluído - Id: {context.Message.Id}");

                Console.WriteLine();
            }
            catch
            {
                throw;
            }
        }
    }
}
