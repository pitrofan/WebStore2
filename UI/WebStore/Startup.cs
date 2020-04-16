using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStore.Clients.Employees;
using WebStore.Clients.Identity;
using WebStore.Clients.Orders;
using WebStore.Clients.Products;
using WebStore.Clients.Values;
using WebStore.Controllers;
using WebStore.DAL.Context;
using WebStore.Data;
using WebStore.Domain.Entities.Identity;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Services.InCookies;
using WebStore.Infrastructure.Services.InMemory;
using WebStore.Infrastructure.Services.InSQL;
using WebStore.Interfaces.Api;
using WebStore.Interfaces.Services;

namespace WebStore
{
    public class Startup
	{
		private IConfiguration configuration { get; }

		public Startup(IConfiguration configuration) => this.configuration = configuration;
		
		
		
		public void ConfigureServices(IServiceCollection services)
		{
			//services.AddDbContext<WebStoreDB>(opt =>
			//	opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
			//services.AddTransient<WebStoreDBInitializer>();

			services.AddIdentity<User, Role>()
				//.AddEntityFrameworkStores<WebStoreDB>()
				.AddDefaultTokenProviders();

			#region WebApi Identity Client stores

			services
				.AddTransient<IUserStore<User>, UsersClient>()
				.AddTransient<IUserPasswordStore<User>, UsersClient>()
				.AddTransient<IUserEmailStore<User>, UsersClient>()
				.AddTransient<IUserPhoneNumberStore<User>, UsersClient>()
				.AddTransient<IUserTwoFactorStore<User>, UsersClient>()
				.AddTransient<IUserLockoutStore<User>, UsersClient>()
				.AddTransient<IUserClaimStore<User>, UsersClient>()
				.AddTransient<IUserLoginStore<User>, UsersClient>()
				;
			services
				.AddTransient<IRoleStore<Role>, RolesClient>()
				;

			#endregion

			// Настройки конфигурации Identity 
			services.Configure<IdentityOptions>(opt =>
			{
				opt.Password.RequiredLength = 3;	// Минимальная длина пароля = 
				opt.Password.RequireDigit = false;	// В пароле только цифры?
				opt.Password.RequireUppercase = false;	// В пароле буквы вернего регистра?
				opt.Password.RequireLowercase = true;	// В пароле буквы нижнего регистра?
				opt.Password.RequireNonAlphanumeric = false;	// В пароле обязательно должны быть символы НЕ алфавита
				opt.Password.RequiredUniqueChars = 3;	// В пароле уникальных символов = 

				//opt.User.AllowedUserNameCharacters = "abcdefghjklmnopqrstuvwxyzABCD....0123456789";	// Набор разрешенных символов для User
				opt.User.RequireUniqueEmail = false;    // Уникальный Email

				//opt.SignIn.;	// Подтверждение аккаунта/email/телефона ...

				opt.Lockout.AllowedForNewUsers = true;	// АвтоРАЗблокировка новыхюзеров (false - надо вручную подтверждать нового юзера)
				opt.Lockout.MaxFailedAccessAttempts = 10;	// Количество ввода не верного пароля доблокировки
				opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);	// Время блокировки при неверном пароле
			});


			// Настройки конфиграции Cookies
			services.ConfigureApplicationCookie(opt =>
			{
				opt.Cookie.Name = "WebStore";	// Имя куки
				opt.Cookie.HttpOnly = true;   // Куки только по Http протоколу
				opt.ExpireTimeSpan = TimeSpan.FromDays(10);   // Время жизни куки

				opt.LoginPath = "/Account/Login";   // Путь к авторизации, когда юзер не авторизован, а для просмотра нужна авторизация, по этому пути будет редирект
				opt.LogoutPath = "/Account/Logout"; // Путь для разавторизации
				opt.AccessDeniedPath = "/Account/AccessDenied"; // Путь по запрету юзера. Когда юзеру отказано в доступе


				opt.SlidingExpiration = true;	// Заменять идентификатор сессии юзера, после того как он стал авторизованным, после не авторизованного. Безопасность сеанса.

			});



			//services.AddMvc(); // dspjd для core 2.2
			services.AddControllersWithViews().AddRazorRuntimeCompilation(); // Добавляем контроллеры
																			 // AddRazorRuntimeCompilation - при изменении представления - автоматически перекомпилируется при запущенном приложении


			// Регистрируем в сервисе
			// AddTransient - каждый раз будет создаваться экземпляр сервиса
			// AddScoped - один экземпляр на область видимости
			// AddSingleton - один объект на все время жизни приложения
			//services.AddTransient<IEmployeesData, InMemoryEmployeesData>();
			//services.AddScoped<IEmployeesData, InMemoryEmployeesData>();
			//services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();
			//services.AddScoped<IEmployeesData, SqlEmployeeData>();
			services.AddTransient<IEmployeesData, EmployeesClient>();
			//services.AddSingleton<IProductData, InMemoryProductData>();
			//services.AddScoped<IProductData, SqlProductData>();
			services.AddScoped<IProductData, ProductsClient>();
			services.AddScoped<ICartService, CookiesCartService>();
			//services.AddScoped<IOrderService, SqlOrderService>();
			services.AddScoped<IOrderService, OrdersClient>();

			services.AddScoped<IValuesService, ValuesClient>();


		}


		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env/*, WebStoreDBInitializer db*/)
		{
			//db.Initialize();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage(); // Отображает все ошибки при разработке
				app.UseBrowserLink();
			}

			app.UseStaticFiles(); // Статические файлы
			app.UseDefaultFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseWelcomePage("/welcome");

			app.UseEndpoints(endpoints =>
			{
				//endpoints.MapGet("/greetings", async context =>
				//{
				//	await context.Response.WriteAsync(configuration["CustomGreetings"]);
				//});

				endpoints.MapControllerRoute(
					name: "areas",
					pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
					);


				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}"
					); 
				// http://localhost:5000/Home/Index/id (Home = Controller Name; Index = Action Name; id = параметр)
			});
		}
	}
}
