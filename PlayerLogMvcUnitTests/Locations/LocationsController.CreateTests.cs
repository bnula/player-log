using AutoMapper;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PlayerLogMvc.Campaigns;
using PlayerLogMvc.Locations;
using PlayerLogMvc.Mappings;
using PlayerLogMvc.Npcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PlayerLogMvcUnitTests.Locations
{
    public class CreateTests
    {
        private readonly Mock<ILocationRepository> _mockRepo;
        private readonly LocationsController _sut;
        private LocationDetailsVM _newLocation;

        public CreateTests()
        {
            _mockRepo = new Mock<ILocationRepository>();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile(new Maps());
            });
            var mapper = mapperConfig.CreateMapper();
            var campRepo = new Mock<ICampaignRepository>();
            var npcRepo = new Mock<INpcRepository>();
            var logger = new Mock<ILogger<LocationsController>>();
            _sut = new LocationsController(_mockRepo.Object, logger.Object, mapper, campRepo.Object, npcRepo.Object);
            _newLocation = new LocationDetailsVM
            {
                LocationName = "test",
                LocationType = "test",
                LocationInventory = "test",
                Description = "test",
                Notes = "test",
                CampaignId = 1

            };
        }

        [Fact]
        public async Task ValidModel_CreateNewItemInDbAndReturnToIndex()
        {
            // Arrange
            Location savedLocation = null;
            _mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<Location>()))
                .ReturnsAsync(true)
                .Callback<Location>(x => savedLocation = x);

            // Act
            var result = await _sut.Create(_newLocation);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            _mockRepo.Verify(repo => repo.CreateAsync(It.IsAny<Location>()), Times.Once);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal(_newLocation.LocationName, savedLocation.LocationName);
        }

        [Fact]
        public async Task InvalidModel_ReturnViewWithModel()
        {
            // Arrange
            _sut.ModelState.AddModelError("x", "test error");
            _mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<Location>()))
                .ReturnsAsync(true);

            // Act
            var result = await _sut.Create(_newLocation);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            _mockRepo.Verify(repo => repo.CreateAsync(It.IsAny<Location>()), Times.Never);
            Assert.Equal("Create", viewResult.ViewName);
            Assert.False(viewResult.ViewData.ModelState.IsValid);
        }

        [Fact]
        public async Task ThrowError_ReturnInternalServerError()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<Location>()))
                .ThrowsAsync(new Exception());

            // Act
            var result = await _sut.Create(_newLocation);

            // Assert
            var redirectToPageResult = Assert.IsType<RedirectToPageResult>(result);
            Assert.Equal("/InternalServerError", redirectToPageResult.PageName);
        }
    }
}
