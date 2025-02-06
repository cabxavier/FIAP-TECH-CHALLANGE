using MassTransit;
using Microsoft.Extensions.Configuration;

namespace TechChallange.Core.ServiceRabbitMQ
{
    public class RabbitMQProdutorService
    {
        private readonly IBus _bus;

        public IConfiguration configuration { get; }

        public RabbitMQProdutorService(IBus bus, IConfiguration configuration)
        {
            this._bus = bus;
            this.configuration = configuration;
        }

        public async Task SendMessage(object objeto, string queueNome)
        {
            try
            {
                var endPoint = await this._bus.GetSendEndpoint(new Uri($"queue:{queueNome}"));
                
                await endPoint.Send(objeto);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao enviar mensagem para a fila: ", ex);
            }
        }
    }
}