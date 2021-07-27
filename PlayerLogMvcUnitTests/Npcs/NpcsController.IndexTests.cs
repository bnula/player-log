using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PlayerLogMvc.Campaign;
using PlayerLogMvc.Location;
using PlayerLogMvc.Npc;
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
    }
}
