using Assesment.Models;
using Assesment.Services.CountryService;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Tests.Services.CountryApiServiceTests
{
    public class CountryApiServiceTests
    {
        [Fact]
        public async Task GetCountriesFromApiAsync_ValidResponse_ReturnsListOfCountries()
        {
            // Arrange
            var httpClient = new HttpClient(new FakeHttpMessageHandler());
            var apiService = new CountryApiService(httpClient);

            var fakeApiResponse = GetFakeApiResponse();

            // Use FakeHttpMessageHandler to simulate HTTP response
            FakeHttpMessageHandler.SetFakeResponse(fakeApiResponse);

            // Act
            var result = await apiService.GetCountriesFromApiAsync("https://fakeapi.com");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Country>>(result);
            Assert.NotEmpty(result);
        }

        private string GetFakeApiResponse()
        {
            // Provide a sample fake response for testing
            var fakeResponse = "[{\"name\":{\"common\":\"Country1\"},\"capital\":[\"Capital1\"],\"borders\":[\"Border1\",\"Border2\"]}]";
            return fakeResponse;
        }

        public class FakeHttpMessageHandler : HttpMessageHandler
        {
            private static string _fakeResponse;

            public static void SetFakeResponse(string fakeResponse)
            {
                _fakeResponse = fakeResponse;
            }

            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                return new HttpResponseMessage
                {
                    Content = new StringContent(_fakeResponse),
                    RequestMessage = request,
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
        }
    }
}
