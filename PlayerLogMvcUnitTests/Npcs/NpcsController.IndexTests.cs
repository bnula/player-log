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

namespace PlayerLogMvcUnitTests.Npcs
{
    public class IndexTests
    {
        private readonly Mock <INpcRepository> _mockRepo;
        private readonly Mock<ILogger<NpcRepository>> _mockLogger;
        private readonly NpcsController _sut;
        private readonly IMapper _mapper;

        public IndexTests()
        {
            _mockRepo = new Mock<INpcRepository>();
            _mockLogger = new Mock<ILogger<NpcRepository>>();
            var mockCampRepo = new Mock<ICampaignRepository>();
            var mockLocRepo = new Mock<ILocationRepository>();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile(new Maps());
            });
            _mapper = mapperConfig.CreateMapper();
            _sut = new NpcsController(_mockRepo.Object, _mockLogger.Object, campRepo: mockCampRepo.Object, locRepo: mockLocRepo.Object, mapper: _mapper);
        }

        [Fact]
        public async Task ReturnsAViewResult_WithAListOfCampaigns()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.FindAllAsync())
                .ReturnsAsync(SampleDataCreators.GetTestNpcs());

            // Act
            var result = await _sut.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<NpcVM>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task NoExistingCampaigns_ReturnEmptyList()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.FindAllAsync())
                .ReturnsAsync(new List<Npc>());

            // Act
            var result = await _sut.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<NpcVM>>(viewResult.ViewData.Model);
            Assert.Empty(model);
        }
        [Fact]
        public async Task ThrowsError_ReturnInternalServerError()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.FindAllAsync())
                .Throws(new Exception());

            // Act
            var result = await _sut.Index();

            // Assert
            var redirectToPageResult = Assert.IsType<RedirectToPageResult>(result);
            Assert.Equal("/InternalServerError", redirectToPageResult.PageName);
        }
    }
}
