using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UsuariosApi.Authorization;
using UsuariosApi.Data;
using UsuariosApi.ModeloEntidade;
using UsuariosApi.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//neste momento eu inicio a configuração da minha conexção com base de dados
builder.Services.AddDbContext<UsuarioDbContext>(options =>
{
    //estou usando a "conceito" - user-secrets como se fosse uma "variavel de ambiente".
    options.UseSqlServer(builder.Configuration["ConnectionStrings:UsuarioConnection"]);
});
//identity eu quero adicionar o  conceito de identidade para este class (Usuario)
//e tambem o papel deste usuario será gerenciado pela (IdentityRole)
builder.Services
    .AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<UsuarioDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAutoMapper
    (AppDomain.CurrentDomain.GetAssemblies());

//atraves desta declaração(AddSingleton)estou fazendo uma injeção de dependencia
//IAuthorizationHandler que será IdadeAuthorization
builder.Services.AddSingleton<IAuthorizationHandler, IdadeAuthorization>();

//configuração do uso do token
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
    JwtBearerDefaults.AuthenticationScheme;

//iremos definir quais são as configurações respectivas jwtbearer
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new
    Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
      //neste momento iremos definir ( campo a campo ou propriedades ) as opções de "validação do token"
      //validação da nossa (chave ou key)
      ValidateIssuerSigningKey = true,
      //quero utilizar esta chave já que aqui faremos a validação
      IssuerSigningKey = new SymmetricSecurityKey
         (Encoding.UTF8.GetBytes(builder.Configuration["SymmetricSecurityKey"])),
      //é uma parte de segurança para não ser "reutizado em outro site"
      //mas neste momento deixaremos como false.
      ValidateAudience = false,
      //redirecionamento quando temos aplicações multi-tanent
      ValidateIssuer = false,
      //o alinhamento do relogio para o "ciclo de vida" deste "token"
      ClockSkew = TimeSpan.Zero

    };
});

//montar a minha politica de acesso
builder.Services.AddAuthorization(options =>
{
    /*esta é a nossa politica representada "IdadeMinima" que vai na controller 
      e pela - AddRequirements - da class IdadeMinima criada.
      este parametro IdadeMinima(18) esta sendo enviado para o construtor da class */
    options.AddPolicy("IdadeMinima", policy =>
       policy.AddRequirements(new IdadeMinima(18))
     );

});

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
