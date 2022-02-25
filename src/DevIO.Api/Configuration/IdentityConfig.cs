using System.Text;
using DevIO.Api.Data;
using DevIO.Api.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace DevIO.Api.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfig(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddErrorDescriber<IdentityMensagensPortugues>()
                .AddDefaultTokenProviders();

            // JWT

            
            var appSettingsSection = configuration.GetSection("AppSettings");

            // configura no aspnet core, fazendo com q AppSettings repsente um trecho do appsettings.json
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x =>
            {
                // sempre q for autenticar é pra gerar um token
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                // depois tem q verificar esse token
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                // basicamente informa q app funciona via token

            }).AddJwtBearer(x =>
            {
                // evita ataq mand in the middle
                x.RequireHttpsMetadata = true;
                // se o token deve ser guardado no http authentication properties apos autenticação feita com sucesso. 
                x.SaveToken = true;
                
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    // valida se quem emitiu o token é o mesmo que esta tentando usa-lo, com base na chave abaixo
                    ValidateIssuerSigningKey = true,
                    // transforma a chave de ascii em uma chave criptografada
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    //valida o quem emitiu o issuer, conforme o nome
                    ValidateIssuer = true,
                    // valida onde o token é valido
                    ValidateAudience = true,
                    
                    // dizendo quem é a audiencia e quem é o issuer para os validates acima
                    // essas duas informacoes vem no token, se não vier ele estara invalido
                    ValidAudience = appSettings.ValidoEm, // lembrando que app.Settings remete ao appsettings.json
                    ValidIssuer = appSettings.Emissor
                };
            });

            return services;
        }
    }
}