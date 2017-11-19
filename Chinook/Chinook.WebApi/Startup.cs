using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Chinook.UnitOfWork;
using Chinook.Repositories.Dapper.Chinook;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using Chinook.WebApi.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Chinook.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IUnitOfWork>
            (options => new ChinookUnitOfWork
                (
                    Configuration.GetConnectionString("Chinook")
                )
            );

            services.AddMvc().AddFluentValidation();
            //services.AddTransient<IValidator<Customer>, CustomerValidator>();

            services.AddResponseCompression();
            services.Configure<GzipCompressionProviderOptions>(options =>
                options.Level = CompressionLevel.Optimal);

            var tokenProvider = new RsaJwtTokenProvider("issuer", "audience", "token_cibertec_2017");

            services.AddSingleton<ITokenProvider>(tokenProvider);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = tokenProvider.GetValidationParameters();
                });

            services.AddAuthorization(auth =>
            {
                auth.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                        .RequireAuthenticatedUser()
                        .Build();
            });

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(option =>
            {
                option.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
            });

            app.UseAuthentication();

            app.UseResponseCompression();
            app.UseMvc();
        }
    }
}
