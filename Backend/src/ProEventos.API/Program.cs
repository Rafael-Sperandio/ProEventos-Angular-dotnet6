using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProEventos.API.Helpers;
using ProEventos.Application.Services;
using ProEventos.Application.Services.Interfaces;
using ProEventos.Domain.Identity;
using ProEventos.Persistence.Contexts;
using ProEventos.Persistence.Repository;
using ProEventos.Persistence.Repository.Interfaces;
using System.Text;
using System.Text.Json.Serialization;

namespace ProEventos.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            var connection = builder.Configuration.GetConnectionString("SqlLiteConnection");
            // Add services to the container.
            builder.Services.AddDbContext<ProEventosContext>(
                context => context.UseSqlite(connection)
                );

            builder.Services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
            })
            .AddRoles<Role>()
            .AddRoleManager<RoleManager<Role>>()
            .AddSignInManager<SignInManager<User>>()
            .AddRoleValidator<RoleValidator<Role>>()
            .AddEntityFrameworkStores<ProEventosContext>()
            .AddDefaultTokenProviders();
            //configuration?.GetSection
            //builder.Configuration.GetSection("TokenKey")

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                    });

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    }

                )
/*                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling
                    = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                )*/
                ;
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddScoped<IEventoService, EventoService>();
            builder.Services.AddScoped<ILoteService, LoteService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IPalestranteService, PalestranteService>();
            builder.Services.AddScoped<IRedeSocialService, RedeSocialService>();
            builder.Services.AddScoped<IUtil, Util>();

            builder.Services.AddScoped<IGeralPersist, GeralPersist>();
            builder.Services.AddScoped<IEventoPersist, EventoPersist>();
            builder.Services.AddScoped<ILotePersist, LotePersist>(); 
            builder.Services.AddScoped<IUserPersist, UserPersist>();
            builder.Services.AddScoped<IPalestrantePersist, PalestrantePersist>();
            builder.Services.AddScoped<IRedeSocialPersist, RedeSocialPersist>();

            builder.Services.AddCors();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "ProEventos.API", Version = "v1" });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header usando Bearer.
                                Entre com 'Bearer ' [espaço] então coloque seu token.
                                Exemplo: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });

            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProEventos.API v1"));
            }
            //https://localhost:5001/swagger/index.html

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseCors(cors => cors.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin());

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
                RequestPath = new PathString("/Resources")
            });

            app.MapControllers();

            app.Run();
        }
    }
}
