using CORE.Entity;
using CORE.Repository;
using MassTransit;

namespace TechChallange.Core.ServiceRabbitMQ
{
    public class RegiaoCriadoConsumidor : IConsumer<Regiao>
    {
        private readonly IRegiaoRepository regiaoRepository;

        public RegiaoCriadoConsumidor(IRegiaoRepository regiaoRepository)
        {
            this.regiaoRepository = regiaoRepository;
        }

        public async Task Consume(ConsumeContext<Regiao> context)
        {
            try
            {
                throw new Exception("DEU RUIM");

                var rotulo = string.Empty;

                if (context is null)
                {
                    throw new Exception("Não foi possível obter as informações da filaregiao.");
                }

                var regiao = new Regiao()
                {
                    Id = context.Message.Id,
                    Ddd = context.Message.Ddd
                };

                if (regiao.Id == 0)
                {
                    await this.regiaoRepository.AddAsync(regiao);

                    rotulo = "Regiao criado";
                }
                else
                {
                    await this.regiaoRepository.UpdateAsync(regiao);

                    rotulo = "Regiao alterado";
                }

                Console.WriteLine();

                Console.WriteLine($"{rotulo} - Id: {context.Message.Id + " || Ddd: " + context.Message.Ddd}");

                Console.WriteLine();
            }
            catch
            {
                throw;
            }
        }
    }
}
