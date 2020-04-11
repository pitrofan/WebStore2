using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Data
{
    public class WebStoreDBInitializer
    {
        private readonly WebStoreDB db;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;

        public WebStoreDBInitializer(WebStoreDB _dB, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            db = _dB;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public void Initialize() => InitializeAsync().Wait();

        public async Task InitializeAsync()
        {
            var database = db.Database;

            //// Создание БД в ручную (без миграций)
            //if (await database.EnsureDeletedAsync().ConfigureAwait(false))
            //    if (!await database.EnsureCreatedAsync().ConfigureAwait(false))
            //        throw new InvalidOperationException("Не удалось создать БД");

            await database.MigrateAsync().ConfigureAwait(false);

            await InitializeIdentityAsync().ConfigureAwait(false);

            await InitializeProductsAsync().ConfigureAwait(false);


        }


        async Task InitializeIdentityAsync()
        {
            if (!await roleManager.RoleExistsAsync(Role.Administrator))
                await roleManager.CreateAsync(new Role { Name = Role.Administrator });

            if (!await roleManager.RoleExistsAsync(Role.User))
                await roleManager.CreateAsync(new Role { Name = Role.User });

            if(await userManager.FindByNameAsync(User.Administrator) is null)
            {
                var admin = new User
                {
                    UserName = User.Administrator,
                    Email = "mail@server.ru"
                };

                var createResult = await userManager.CreateAsync(admin, User.AdminDefaultPassword);


                if (createResult.Succeeded)
                    await userManager.AddToRoleAsync(admin, Role.Administrator);
                else
                {
                    var errors = createResult.Errors.Select(x => x.Description);
                    throw new InvalidOperationException($"Ошибка при создании пользователя Администратор: {string.Join(", ", errors)}");
                }
            }
        }


        async Task InitializeProductsAsync()
        {
            if (await db.Products.AnyAsync())
                return;

            var database = db.Database;
            //if (!await db.Employees.AnyAsync())
            //{
            using (var Transaction = await database.BeginTransactionAsync().ConfigureAwait(false))
            {
                await db.Employees.AddRangeAsync(TestData.Employees).ConfigureAwait(false);

                await database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Employees] ON");// Разрешаем базе вставлять свой первичный кюч ID и внешний ключ ~ParentId, т.к. в TestData у нас прописаны эти данные
                await db.SaveChangesAsync().ConfigureAwait(false);
                await database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Employees] OFF");

                await Transaction.CommitAsync().ConfigureAwait(false);
            }
            //}

            

            using (var Transaction = await database.BeginTransactionAsync().ConfigureAwait(false))
            {
                await db.Sections.AddRangeAsync(TestData.Sections).ConfigureAwait(false);

                await database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Sections] ON");// Разрешаем базе вставлять свой первичный кюч ID и внешний ключ ~ParentId, т.к. в TestData у нас прописаны эти данные
                await db.SaveChangesAsync().ConfigureAwait(false);
                await database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Sections] OFF");

                await Transaction.CommitAsync().ConfigureAwait(false);
            }

            using (var Transaction = await database.BeginTransactionAsync().ConfigureAwait(false))
            {
                await db.Brands.AddRangeAsync(TestData.Brands).ConfigureAwait(false);

                await database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Brands] ON");// Разрешаем базе вставлять свой первичный кюч ID и внешний ключ ~ParentId, т.к. в TestData у нас прописаны эти данные
                await db.SaveChangesAsync().ConfigureAwait(false);
                await database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Brands] OFF");

                await Transaction.CommitAsync().ConfigureAwait(false);
            }

            using (var Transaction = await database.BeginTransactionAsync().ConfigureAwait(false))
            {
                await db.Products.AddRangeAsync(TestData.Products).ConfigureAwait(false);

                await database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Products] ON");// Разрешаем базе вставлять свой первичный кюч ID и внешний ключ ~ParentId, т.к. в TestData у нас прописаны эти данные
                await db.SaveChangesAsync().ConfigureAwait(false);
                await database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Products] OFF");

                await Transaction.CommitAsync().ConfigureAwait(false);
            }

        }
    }
}
