using CORE.Entity;
using CORE.Repository;
using MassTransit;

namespace TechChallange.Core.ServiceRabbitMQ
{
    public class ContatoDeleteConsumidor : IConsumer<Contato>
    {
        private readonly IContatoRepository contatoRepository;

        public ContatoDeleteConsumidor(IContatoRepository contatoRepository)
        {
            this.contatoRepository = contatoRepository;
        }

        public async Task Consume(ConsumeContext<Contato> context)
        {
            try
            {
                if (context is null)
                {
                    throw new Exception("Não foi possível obter as informações da filacontatodelete.");
                }

                if (context.Message.Id == 0)
                {
                    throw new Exception("Não foi possível obter as informações do identificador.");
                }

                await this.contatoRepository.DeleteAsync(context.Message.Id);

                Console.WriteLine();

                Console.WriteLine($"Contato excluído - Id: {context.Message.Id}");

                Console.WriteLine();
            }
            catch
            {
                throw;
            }
        }
    }
}
