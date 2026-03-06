using FilmesMoura.WebAPI.BdContectFilme;
using FilmesMoura.WebAPI.Interface;
using FilmesMoura.WebAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

//adiciona o contexto do banco de dados ao serviço

builder.Services.AddDbContext<FilmeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adiciona os repositórios ao serviço para injeção de dependência
builder.Services.AddScoped<IFilmesRepository, FilmeRepository>();
builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

//Adiciona serviços de autenticação e autorização Jwt Bearer
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
})

    .AddJwtBearer("JwtBearer", options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            //valida quem está solicitando o token (emissor)
            ValidateIssuer = true,

            //valida quem está recebendo o token (destinatário)
            ValidateAudience = true,

            //valida o tempo de expiração do token
            ValidateLifetime = true,

            //valida a assinatura do token para garantir que ele não foi alterado
            ValidateIssuerSigningKey = true,

            //validador do emissor do token
            ValidIssuer = "api_filmes",

            //validador do destinatário do token
            ValidAudience = "api_filmes",

            //chave de assinatura do token (deve ser a mesma que foi usada para gerar o token)
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("filmes-chave-autenticacao-webapi-dev")),

            //tempo de tolerância para expiração do token (caso haja diferença de horário entre o servidor e o cliente)
            ClockSkew = TimeSpan.FromMinutes(5)
        };
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
    {
        Version = "v1",
        Title = "Filmes API",
        Description = "API para gerenciamento de filmes, gêneros e usuários.",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new Microsoft.OpenApi.OpenApiContact
        {
            Name = "marcaumdev",
            Url = new Uri("https://gihub.com/marcaumdev"),
        },
        License = new Microsoft.OpenApi.OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license"),
        }
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT seu botzinho:"
    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = Array.Empty<string>().ToList()
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});



//Adiciona os controladores ao serviço
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
        app.UseSwagger(options => { });
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
        });
}

app.UseCors("CorsPolicy");   

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();


//adiciona o middleware de roteamento e mapeia os controladores
app.MapControllers();

app.Run();
