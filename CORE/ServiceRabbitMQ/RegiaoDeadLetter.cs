using CORE.Entity;
using MassTransit;

namespace TechChallange.Core.ServiceRabbitMQ
{
    public class RegiaoDeadLetter : IConsumer<Regiao>
    {
        public Task Consume(ConsumeContext<Regiao> context)
        {
            Console.WriteLine();

            Console.WriteLine(string.Format("Regiao - mensagem movida para Dead Letter Queue: {0} || {1}",
                context.Message?.Id, context.Message?.Ddd));

            Console.WriteLine();

            return Task.CompletedTask;
        }
    }
}
