using CORE.Repository;
using CORE.Validator;
using INFRASTRUCTURE.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

/*var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();*/

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//{
//    options.UseSqlServer(configuration.GetConnectionString("ConnectionString"));

//}, ServiceLifetime.Scoped);

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseInMemoryDatabase("BDContato"));

builder.Services.AddScoped<IContatoRepository, ContatoRepository>();
builder.Services.AddScoped<IRegiaoRepository, RegiaoRepository>();
builder.Services.AddScoped<IContatoRegiaoRepository, ContatoRegiaoRepository>();
builder.Services.AddScoped<ContatoValidator>();
builder.Services.AddScoped<RegiaoValidator>();
builder.Services.AddScoped<ContatoRegiaoValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
