using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using FakeItEasy;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using StolenBikes.Core.Application.DTOs;
using StolenBikes.Core.Domain.Entities;
using StolenBikes.Infrastructure.Shared.Services.StolenBikes;
using StolenBikes.Infrastructure.Shared.Services.StolenBikes.Helpers;

namespace StolenBikes.Infrastructure.Shared.Tests.Services.StolenBikes
{
    [TestClass]
    public class GetStolenBikesForLocationServiceTests
    {
        private IStolenBikesDataHelper _stolenBikesDataHelper;
        private GetStolenBikesForLocationService _stolenBikesForLocationService;

        [TestInitialize]
        public void Initialize()
        {
            _stolenBikesDataHelper = A.Fake<IStolenBikesDataHelper>();

            _stolenBikesForLocationService = new GetStolenBikesForLocationService(_stolenBikesDataHelper);
        }

        [TestMethod]
        public void Constructor_WithNullForStolenBikesDataHelper_ShouldThrowArgumentNullException()
        {
            // Arrange
            const string expectedNameOfParam = "stolenBikesDataHelper";

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
            var stolenBikesFromLocation = new List<StolenBikeIncident>()
            {
                new StolenBikeIncident
                {
                    Id = 1
                },
                new StolenBikeIncident
                {
                    Id = 2
                }
            };

            A.CallTo(() => _stolenBikesDataHelper.FetchAllStolenBikes(proximity, proximitySquare))
                .Returns(stolenBikesFromLocation);

            var expected = new StolenBikesForLocationDto
            {
                Proximity = proximity,
                ProximitySquare = proximitySquare,
                StolenBikesCount = stolenBikesFromLocation.Count
            };

            // Act
            var actual = await _stolenBikesForLocationService.GetStolenBikesForLocation(proximity, proximitySquare);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}

