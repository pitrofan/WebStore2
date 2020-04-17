using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Infrastructure.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ErrorHandlingMiddleware> logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        //public async Task Invoke(HttpContext Context)
        //{
        //    // Действие, выполняемое в нашу очередь в конвейере.
        //    var next_taks = next(Context); //  Запускаем следующее звено конвейера асинхронно
        //    // Можем параллельно выполнять какие-то другие действия
        //    await next_taks; // Ожидаем завершения оставшейся части конвейера
        //    // Действия, Выполняемые после завершения работы оставшейся части конвейера
        //}

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                HandleException(context, e);
                //throw e; // Неправильно. Повреждает стек вызова. Так делать не надо!!!
                throw;
            }
        }

        private void HandleException(HttpContext context, Exception error)
        {
            logger.LogError(error, "Ошибка при обработке запроса {0}", context.Request.Path);
        }
    }
}
