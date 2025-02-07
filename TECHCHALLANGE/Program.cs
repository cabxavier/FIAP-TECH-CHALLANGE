using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using CORE.Repository;
using CORE.Validator;
using INFRASTRUCTURE.Repository;
using MassTransit;
using TechChallange.Api;
using CORE.Entity;
using TechChallange.Core.ServiceRabbitMQ;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddHostedService<ConsoleWorker>();

builder.Services.AddOpenTelemetry()
    .WithMetrics(metrics =>
    {
        metrics.SetResourceBuilder(ResourceBuilder.CreateDefault()
            .AddService("webapimetric"))
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddRuntimeInstrumentation()
            .AddProcessInstrumentation()
            .AddPrometheusExporter();
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseInMemoryDatabase("BDContato"));

builder.Services.AddScoped<IContatoRepository, ContatoRepository>();
builder.Services.AddScoped<IRegiaoRepository, RegiaoRepository>();
builder.Services.AddScoped<IContatoRegiaoRepository, ContatoRegiaoRepository>();
builder.Services.AddScoped<ContatoValidator>();
builder.Services.AddScoped<RegiaoValidator>();
builder.Services.AddScoped<ContatoRegiaoValidator>();

builder.Services.AddSingleton<RabbitMQProdutorService>();

var configuration = builder.Configuration;
var filaContato = configuration.GetSection("RabbitMQ")["NomeFilaContato"] ?? string.Empty;
var filaContatoDelete = configuration.GetSection("RabbitMQ")["NomeFilaContatoDelete"] ?? string.Empty;
var filaRegiao = configuration.GetSection("RabbitMQ")["NomeFilaRegiao"] ?? string.Empty;
var filaRegiaoDelete = configuration.GetSection("RabbitMQ")["NomeFilaRegiaoDelete"] ?? string.Empty;
var filaContatoRegiao = configuration.GetSection("RabbitMQ")["NomeFilaContatoRegiao"] ?? string.Empty;
var filaContatoRegiaoDelete = configuration.GetSection("RabbitMQ")["NomeFilaContatoRegiaoDelete"] ?? string.Empty;
var servidor = configuration.GetSection("RabbitMQ")["Servidor"] ?? string.Empty;
var usuario = configuration.GetSection("RabbitMQ")["Usuario"] ?? string.Empty;
var senha = configuration.GetSection("RabbitMQ")["Senha"] ?? string.Empty;

builder.Services.AddMassTransit((x =>
{
    x.AddConsumer<ContatoCriadoConsumidor>();
    x.AddConsumer<ContatoDeleteConsumidor>();
    x.AddConsumer<ContatoDeadLetter>();
    x.AddConsumer<RegiaoCriadoConsumidor>();
    x.AddConsumer<RegiaoDeleteConsumidor>();
    x.AddConsumer<RegiaoDeadLetter>();
    x.AddConsumer<ContatoRegiaoCriadoConsumidor>();
    x.AddConsumer<ContatoRegiaoDeleteConsumidor>();
    x.AddConsumer<ContatoRegiaoDeadLetter>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(servidor, "/", h =>
        {
            h.Username(usuario);
            h.Password(senha);
        });

        cfg.ReceiveEndpoint(filaContato, e =>
        {
            e.ConfigureConsumer<ContatoCriadoConsumidor>(context);
        });

        cfg.ReceiveEndpoint(filaContatoDelete, e =>
        {
            e.ConfigureConsumer<ContatoDeleteConsumidor>(context);
        });

        cfg.ReceiveEndpoint($"{filaContato}_error", e =>
        {
            e.ConfigureConsumer<ContatoDeadLetter>(context);
        });

        cfg.ReceiveEndpoint(filaRegiao, e =>
        {
            e.ConfigureConsumer<RegiaoCriadoConsumidor>(context);
        });

        cfg.ReceiveEndpoint(filaRegiaoDelete, e =>
        {
            e.ConfigureConsumer<RegiaoDeleteConsumidor>(context);
        });

        cfg.ReceiveEndpoint($"{filaRegiao}_error", e =>
        {
            e.ConfigureConsumer<RegiaoDeadLetter>(context);
        });

        cfg.ReceiveEndpoint(filaContatoRegiao, e =>
        {
            e.ConfigureConsumer<ContatoRegiaoCriadoConsumidor>(context);
        });

        cfg.ReceiveEndpoint(filaContatoRegiaoDelete, e =>
        {
            e.ConfigureConsumer<ContatoRegiaoDeleteConsumidor>(context);
        });

        cfg.ReceiveEndpoint($"{filaContatoRegiao}_error", e =>
        {
            e.ConfigureConsumer<ContatoRegiaoDeadLetter>(context);
        });

        cfg.ConfigureEndpoints(context);
    });

}));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.UseAuthorization();
app.MapControllers();

app.MapPrometheusScrapingEndpoint("/metrics");

app.Run();
