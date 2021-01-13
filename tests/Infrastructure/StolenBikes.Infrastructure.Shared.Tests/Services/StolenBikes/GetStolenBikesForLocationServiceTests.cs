using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using FakeItEasy;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

using RestEase;

using StolenBikes.Core.Application.DTOs;
using StolenBikes.Infrastructure.Shared.Services.StolenBikes;
using StolenBikes.Infrastructure.Shared.Services.StolenBikes.Clients;

namespace StolenBikes.Infrastructure.Shared.Tests.Services.StolenBikes
{
    [TestClass]
    public class GetStolenBikesForLocationServiceTests
    {
        private IStolenBikesApi _stolenBikesApi;
        private GetStolenBikesForLocationService _stolenBikesForLocationService;

        [TestInitialize]
        public void Initialize()
        {
            _stolenBikesApi = A.Fake<IStolenBikesApi>();

            _stolenBikesForLocationService = new GetStolenBikesForLocationService(_stolenBikesApi);
        }

        [TestMethod]
        public void Constructor_WithNullForStolenBikesApi_ShouldThrowArgumentNullException()
        {
            // Arrange
            const string expectedNameOfParam = "stolenBikesApi";

            // Act
            Action act = () => _ = new GetStolenBikesForLocationService(null);

            // Assert
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be(expectedNameOfParam);
        }

        [TestMethod]
        public void GetStolenBikesForLocation_WithInvalidProximity_ShouldThrowArgumentNullException()
        {
            // Arrange
            const int proximitySquare = 2;
            const string expectedNameOfParam = "proximity";

            // Act
            Func<Task> act = async () => await _stolenBikesForLocationService.GetStolenBikesForLocation(null, proximitySquare);

            // Assert
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be(expectedNameOfParam);
        }


        [DataTestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public async Task GetStolenBikesForLocation_WithInvalidProximitySquare_ShouldThrowArgumentOutOfRangeException(int proximitySquare)
        {
            // Arrange
            const string proximityPlace = "Athens";

            // Act
            Func<Task> act = async () =>
            {
                await _stolenBikesForLocationService.GetStolenBikesForLocation(proximityPlace, proximitySquare);
            };

            // Assert
            await act.Should().ThrowAsync<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public async Task GetStolenBikesForLocation_WithValidInput_ReturnsStolenBikesForLocation()
        {
            // Arrange
            var proximity = "Athens";
            var proximitySquare = 2;

            const int stolenBikesCount = 76;

            var testDataFilePath = Directory.GetCurrentDirectory() + "/TestData/ApiResponse.json";
            var jsonData = await File.ReadAllTextAsync(testDataFilePath);
            var apiResponseContent = JsonConvert.DeserializeObject<StolenBikesApiResponse>(jsonData);
            var apiResponse = new Response<StolenBikesApiResponse>(null, new HttpResponseMessage(HttpStatusCode.OK),
                () => apiResponseContent);
            apiResponse.ResponseMessage.Headers.Add("total", stolenBikesCount.ToString());
            //A.CallTo() apiResponse.ResponseMessage.Headers

            A.CallTo(() => _stolenBikesApi.GetStolenBikesForLocationAsync(1, 1, proximity, proximitySquare))
                .Returns(apiResponse);

            var expected = new StolenBikesForLocationDto
            {
                Proximity = proximity,
                ProximitySquare = proximitySquare,
                StolenBikesCount = stolenBikesCount
            };

            // Act
            var actual = await _stolenBikesForLocationService.GetStolenBikesForLocation(proximity, proximitySquare);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}

