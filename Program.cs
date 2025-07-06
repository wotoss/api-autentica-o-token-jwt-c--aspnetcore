using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UsuariosApi.Data;
using UsuariosApi.ModeloEntidade;
using UsuariosApi.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//neste momento eu inicio a configuração da minha conexção com base de dados
builder.Services.AddDbContext<UsuarioDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("UsuarioConnection"));
});
//identity eu quero adicionar o  conceito de identidade para este class (Usuario)
//e tambem o papel deste usuario será gerenciado pela (IdentityRole)
builder.Services
    .AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<UsuarioDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAutoMapper
    (AppDomain.CurrentDomain.GetAssemblies());

//AddScoped sempre será instanciado quando houver uma requisição nova que demande 
//uma nova requisição.
//posso usar [ AddSingleton, AddTrasient ]
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<TokenService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
