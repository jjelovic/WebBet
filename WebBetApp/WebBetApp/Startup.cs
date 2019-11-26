using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using WebBetApp.Main;
using WebBetApp.Main.Validation;
using WebBetApp.Model;
using WebBetApp.Model.Database;
using WebBetApp.Model.Database.Testing;
using WebBetApp.Model.ViewModels;

namespace WebBetApp
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddScoped<IWebBetQueries, WebBetQuriesImpl>();
            services.AddScoped<IValidator<WebTicket>, TicketValidator>();
            //services.AddScoped<IValidator<WebWallet>, WalletValidator>();

            services.Configure<AppSettings>(Configuration.GetSection("ApplicationSettings"));
            services.AddDbContext<WebBetDbContext>(options =>
                                                   options.UseSqlServer(Configuration.GetConnectionString("WebBetDbContext")));

            services.AddDefaultIdentity<ApplicationUser>()
                    .AddEntityFrameworkStores<WebBetDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
            });

            services.AddSwaggerGen((options) =>
            {
                options.SwaggerDoc("v1", new Info { Title = "Webbet API", Version = "v1" });
            });

            services.AddCors();

            var signinigKey = Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:JsonWebTokenSecurityKey"].ToString());

            //Authentication
            services.AddAuthentication(x =>
           {
               x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               x.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
               x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
           }).AddJwtBearer( x => {
               x.RequireHttpsMetadata = false;
               x.SaveToken = false;
               x.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(signinigKey),
                   ValidateIssuer = false,
                   ValidateAudience = false,
                   ClockSkew = TimeSpan.Zero
               };
           });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
               app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            //Swagger JSON endponint
            app.UseSwaggerUI(s =>
            {
               s.SwaggerEndpoint("/swagger/v1/swagger.json", "Webbet API");
            });

            app.UseCors(options => options.WithOrigins(Configuration["ApplicationSettings:ClientUrl"].ToString())
                                          .AllowAnyMethod()
                                          .AllowAnyHeader()
                                          .AllowAnyOrigin());
            app.UseAuthentication(); 

            app.UseMvc();

            //Create database if not exist
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<WebBetDbContext>();
                //context.Database.EnsureCreated();
                context.Database.Migrate();

                //Fill with test data
                TestDataFactory.Fill(context);
            }
        }
    }
}
