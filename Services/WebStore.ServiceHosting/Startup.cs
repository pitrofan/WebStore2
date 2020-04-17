using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using WebStore.DAL.Context;
using WebStore.Data;
using WebStore.Domain.Entities.Identity;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Services.InCookies;
using WebStore.Infrastructure.Services.InMemory;
using WebStore.Infrastructure.Services.InSQL;
using WebStore.Interfaces.Services;
using WebStore.Logger;

namespace WebStore.ServiceHosting
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
            services.AddControllers();

            services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();
            services.AddScoped<IProductData, SqlProductData>();
            services.AddScoped<IOrderService, SqlOrderService>();
            services.AddScoped<ICartService, CookiesCartService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDbContext<WebStoreDB>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<WebStoreDBInitializer>();

            services.AddIdentity<User, Role>(opt =>
                {
                    opt.Password.RequiredLength = 3;    // Минимальная длина пароля = 
                    opt.Password.RequireDigit = false;  // В пароле только цифры?
                    opt.Password.RequireUppercase = false;  // В пароле буквы вернего регистра?
                    opt.Password.RequireLowercase = true;   // В пароле буквы нижнего регистра?
                    opt.Password.RequireNonAlphanumeric = false;    // В пароле обязательно должны быть символы НЕ алфавита
                    opt.Password.RequiredUniqueChars = 3;   // В пароле уникальных символов = 

                    //opt.User.AllowedUserNameCharacters = "abcdefghjklmnopqrstuvwxyzABCD....0123456789";	// Набор разрешенных символов для User
                    opt.User.RequireUniqueEmail = false;    // Уникальный Email

                    //opt.SignIn.;	// Подтверждение аккаунта/email/телефона ...

                    opt.Lockout.AllowedForNewUsers = true;  // АвтоРАЗблокировка новыхюзеров (false - надо вручную подтверждать нового юзера)
                    opt.Lockout.MaxFailedAccessAttempts = 10;   // Количество ввода не верного пароля доблокировки
                    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);  // Время блокировки при неверном пароле
                })
                .AddEntityFrameworkStores<WebStoreDB>()
                .AddDefaultTokenProviders();

            services.AddSwaggerGen(opt => 
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "WebStore.API", Version = "v1" });
                opt.IncludeXmlComments("WebStore.ServiceHosting.xml");

                const string domainDocXml = "WebStore.Domain.xml";
                const string debugPath = @"bin\Debug\netcoreapp3.1";
                if(File.Exists(domainDocXml))
                    opt.IncludeXmlComments("WebStore.Domain.xml");
                else if(File.Exists(Path.Combine(debugPath, domainDocXml)))
                    opt.IncludeXmlComments(Path.Combine(debugPath, domainDocXml));

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WebStoreDBInitializer db, ILoggerFactory log)
        {
            log.AddLog4net();

            db.Initialize();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(opt => 
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "WebStore.API");
                opt.RoutePrefix = "";
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
