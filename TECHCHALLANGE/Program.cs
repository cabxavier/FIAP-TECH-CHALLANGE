using CORE.Repository;
using CORE.Validator;
using INFRASTRUCTURE.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
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

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
