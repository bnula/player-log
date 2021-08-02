using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PlayerLogMvc.Campaigns;
using PlayerLogMvc.Locations;
using PlayerLogMvc.Mappings;
using PlayerLogMvc.Npcs;
using PlayerLogMvcUnitTests.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PlayerLogMvcUnitTests.Locations
{
    public class IndexTests
    {
        private readonly LocationsController _sut;
        private readonly Mock<ILocationRepository> _mockRepo;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<LocationsController>> _logger;

        public IndexTests()
        {
            _mockRepo = new Mock<ILocationRepository>();
            var _campRepo = new Mock<ICampaignRepository>();
            var _npcRepo = new Mock<INpcRepository>();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile(new Maps());
            });
            _mapper = mapperConfig.CreateMapper();
            _logger = new Mock<ILogger<LocationsController>>();
            _sut = new LocationsController(
                repo: _mockRepo.Object,
                logger: _logger.Object,
                mapper: _mapper,
                campRepo: _campRepo.Object,
                npcRepo: _npcRepo.Object
                );
        }

        [Fact]
        public async Task OnCall_ReturnViewWithModel()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.FindAllAsync())
                .ReturnsAsync(SampleDataCreators.GetTestLocations());

            // Act
            var result = await _sut.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<LocationVM>>(viewResult.Model);
            Assert.Equal("Index", viewResult.ViewName);
            Assert.Equal(2, model.Count);
        }

        [Fact]
        public async Task NoLocations_ReturnViewWithEmptyList()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.FindAllAsync())
                .ReturnsAsync(new List<Location>());

            // Act
            var result = await _sut.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<LocationVM>>(viewResult.Model);
            Assert.Equal("Index", viewResult.ViewName);
            Assert.Empty(model);
        }

        [Fact]
        public async Task ThrowError_ReturnInternalServerError()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.FindAllAsync())
                .ThrowsAsync(new AccessViolationException());

            // Act
            var result = await _sut.Index();

            // Assert
            var redirectToPageResult = Assert.IsType<RedirectToPageResult>(result);
            _mockRepo.Verify(repo => repo.FindAllAsync(), Times.Once);
            Assert.Equal("/InternalServerError", redirectToPageResult.PageName);
        }
    }
}
