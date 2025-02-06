using CORE.Entity;
using MassTransit;

namespace TechChallange.Core.ServiceRabbitMQ
{
    public class ContatoDeadLetter: IConsumer<Contato>
    {
        public Task Consume(ConsumeContext<Contato> context)
        {
            Console.WriteLine();

            Console.WriteLine(string.Format("Contato - mensagem movida para Dead Letter Queue: {0} || {1} || {2} || {3}",
                context.Message?.Id, context.Message?.Nome, context.Message?.Telefone, context.Message?.Email));

            Console.WriteLine();

            return Task.CompletedTask;
        }
    }
}
