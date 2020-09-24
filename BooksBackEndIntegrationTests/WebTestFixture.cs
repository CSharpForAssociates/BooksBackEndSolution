using BooksBackEnd;
using BooksBackEnd.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BooksBackEndIntegrationTests
{
    public class WebTestFixture : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // find the service that is *currently* implementing ISystemTime and remove it.
                var systemTimeDescription = services.SingleOrDefault(
                    d => d.ServiceType == typeof(ISystemTime)
                    );
                // Secretly replace it with folger's crystals. (our fake version)
                services.Remove(systemTimeDescription);
                services.AddTransient<ISystemTime, FakeSystemTime>();

            });
        }
    }

    public class FakeSystemTime : ISystemTime
    {
        public DateTime GetCurrent()
        {
            return new DateTime(1969, 04, 20, 23, 59, 00);
        }
    }
}
