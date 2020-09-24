using BooksBackEnd;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooksBackEndIntegrationTests
{
    public class WebTestFixture : WebApplicationFactory<Startup>
    {
    }
}
