using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebBetApp.Main;
using WebBetApp.Main.Validation;
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
            services.AddScoped(typeof(Validation<TypeOfObjectToValidate>), typeof(WalletValidation));
            services.AddDbContext<WebBetDbContext>(options =>
                                                   options.UseSqlServer(Configuration.GetConnectionString("WebBetDbContext")));


            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
               app.UseDeveloperExceptionPage();
            }

            app.UseCors(options => options.WithOrigins("http://localhost:4200")
                                          .AllowAnyMethod()
                                          .AllowAnyHeader());

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
