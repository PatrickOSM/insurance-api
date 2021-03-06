using Insurance.Api.Application.Auth;
using Insurance.Api.Application.DTOs;
using Insurance.Api.Application.Interfaces;
using Insurance.Api.Application.Services;
using Insurance.Api.Domain.Auth.Interfaces;
using Insurance.Api.Domain.Repositories;
using Insurance.Api.Extensions;
using Insurance.Api.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

namespace Insurance.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Extension method for less clutter in startup
            services.AddApplicationDbContext(Configuration);

            //DI Services and Repos
            services.AddScoped<IInsurancePolicyRepository, InsurancePolicyRepository>();
            services.AddScoped<IInsurancePolicyService, InsurancePolicyService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ISession, Session>();

            // WebApi Configuration
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });


            IConfigurationSection tokenConfig = Configuration.GetSection("TokenConfiguration");
            services.Configure<TokenConfiguration>(tokenConfig);

            // configure jwt authentication
            TokenConfiguration appSettings = tokenConfig.Get<TokenConfiguration>();
            byte[] key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = Environment.IsProduction();
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = appSettings.Issuer,
                        ValidAudience = appSettings.Audience
                    };
                });


            // AutoMapper settings
            services.AddAutoMapperSetup();

            // HttpContext for log enrichment 
            services.AddHttpContextAccessor();

            // Swagger settings
            services.AddApiDoc();
            // GZip compression
            services.AddCompression();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCustomSerilogRequestLogging();
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseRouting();
            app.UseApiDoc();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //added request logging


            app.UseHttpsRedirection();


            app.UseResponseCompression();
        }
    }
}
