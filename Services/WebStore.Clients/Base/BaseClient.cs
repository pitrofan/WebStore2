using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace WebStore.Clients.Base
{
    public abstract class BaseClient : IDisposable
    {
        protected readonly HttpClient client;
        protected readonly string serviceAddress;


        protected BaseClient(IConfiguration configuration, string ServiceAddress)
        {
            this.serviceAddress = ServiceAddress;
            client = new HttpClient()
            {
                BaseAddress = new Uri(configuration["WebApiURL"])
            };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
        }


        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (disposed || !disposing) return;
            disposed = true;
            client.Dispose();
        }
        #endregion

    }
}
