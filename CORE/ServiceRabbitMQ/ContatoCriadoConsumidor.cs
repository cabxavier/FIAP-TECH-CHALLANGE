using CORE.Entity;
using MassTransit;
using CORE.Repository;

namespace TechChallange.Core.ServiceRabbitMQ
{
    public class ContatoCriadoConsumidor : IConsumer<Contato>
    {
        private readonly IContatoRepository contatoRepository;

        public ContatoCriadoConsumidor(IContatoRepository contatoRepository)
        {
            this.contatoRepository = contatoRepository;
        }

        public async Task Consume(ConsumeContext<Contato> context)
        {
            try
            {
                //throw new Exception("DEU RUIM");

                var rotulo = string.Empty;

                if (context is null)
                {
                    throw new Exception("Não foi possível obter as informações da filacontato.");
                }

                var contato = new Contato()
                {
                    Id = context.Message.Id,
                    Nome = context.Message.Nome,
                    Telefone = context.Message.Telefone,
                    Email = context.Message.Email
                };

                if (contato.Id == 0)
                {
                    await this.contatoRepository.AddAsync(contato);

                    rotulo = "Contato criado";
                }
                else
                {
                    await this.contatoRepository.UpdateAsync(contato);

                    rotulo = "Contato alterado";
                }

                Console.WriteLine();

                Console.WriteLine($"{rotulo} - Id: {context.Message.Id + " || Nome: " + context.Message.Nome + " || Telefone: " + context.Message.Telefone + " || Email: " + context.Message.Email}");

                Console.WriteLine();
            }
            catch
            {
                throw;
            }
        }
    }
}
