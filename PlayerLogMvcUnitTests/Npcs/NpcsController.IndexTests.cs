using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PlayerLogMvc.Campaigns;
using PlayerLogMvc.Locations;
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
        Mock<INpcRepository> _mockRepo;
        Mock<ILogger<NpcRepository>> _mockLogger;
        NpcsController _sut;

        public IndexTests()
        {
            _mockRepo = new Mock<INpcRepository>();
            _mockLogger = new Mock<ILogger<NpcRepository>>();
            var mockCampRepo = new Mock<ICampaignRepository>();
            var mockLocRepo = new Mock<ILocationRepository>();
            _sut = new NpcsController(_mockRepo.Object, _mockLogger.Object, campRepo: mockCampRepo.Object, locRepo: mockLocRepo.Object);
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
