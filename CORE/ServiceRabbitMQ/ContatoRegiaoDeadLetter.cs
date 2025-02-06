using CORE.Entity;
using MassTransit;

namespace TechChallange.Core.ServiceRabbitMQ
{
    public class ContatoRegiaoDeadLetter : IConsumer<ContatoRegiao>
    {
        public Task Consume(ConsumeContext<ContatoRegiao> context)
        {
            Console.WriteLine();

            Console.WriteLine(string.Format("ContatoRegiao - mensagem movida para Dead Letter Queue: {0} || {1} || {2}",
                context.Message?.Id, context.Message?.ContatoId, context.Message?.RegiaoId));

            Console.WriteLine();

            return Task.CompletedTask;
        }
    }
}
