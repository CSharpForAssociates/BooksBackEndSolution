using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace BooksBackEndIntegrationTests
{
    public class GettingStatus : IClassFixture<WebTestFixture>
    {
        private HttpClient _client;

        public GettingStatus(WebTestFixture fixture)
        {
            _client = fixture.CreateClient();
        }

        // Do we get a 200 Status Code?
        [Fact]
        public async void WeGetASuccessStatusCode()
        {
            var response = await _client.GetAsync("/status");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }
        // Do we get the status encoded as application/json?
        [Fact]
        public async void FormattedAsJson()
        {
            var response = await _client.GetAsync("/status");

            var mediaType = response.Content.Headers.ContentType.MediaType;
            Assert.Equal("application/json", mediaType);
        }
        // Does the representation (model) that comes back look right?
        [Fact]
        public async void HasCorrectBody()
        {
            var response = await _client.GetAsync("/status");

            var content = await response.Content.ReadAsAsync<StatusResponse>();

            Assert.Equal("Looks good, Boss!", content.message);
            Assert.Equal("Joe", content.checkedBy);
        }
        // etc. etc.
    }


    public class StatusResponse
    {
        public string message { get; set; }
        public string checkedBy { get; set; }
        public DateTime lastChecked { get; set; }
    }

}
