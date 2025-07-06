using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UsuariosApi.Data;
using UsuariosApi.ModeloEntidade;
using UsuariosApi.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//neste momento eu inicio a configura��o da minha conex��o com base de dados
builder.Services.AddDbContext<UsuarioDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("UsuarioConnection"));
});
//identity eu quero adicionar o  conceito de identidade para este class (Usuario)
//e tambem o papel deste usuario ser� gerenciado pela (IdentityRole)
builder.Services
    .AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<UsuarioDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAutoMapper
    (AppDomain.CurrentDomain.GetAssemblies());

//AddScoped sempre ser� instanciado quando houver uma requisi��o nova que demande 
//uma nova requisi��o.
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
