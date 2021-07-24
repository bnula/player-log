using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PlayerLogMvc.Campaign;
using PlayerLogMvcUnitTests.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PlayerLogMvcUnitTests
{
    public class IndexTests
    {

        private readonly Mock<ICampaignRepository> _mockRepo;
        private readonly Mock<ILogger<CampaignRepository>> _mockLogger;
        private readonly CampaignsController _sut;

        public IndexTests()
        {
            _mockRepo = new Mock<ICampaignRepository>();
            _mockLogger = new Mock<ILogger<CampaignRepository>>();
            _sut = new CampaignsController(_mockRepo.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task ReturnsAViewResult_WithAListOfCampaigns()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.FindAllAsync())
                .ReturnsAsync(SampleDataCreators.GetTestCamps());

            // Act
            var result = await _sut.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<CampaignVM>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task NoExistingCampaigns_ReturnEmptyList()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.FindAllAsync())
                .ReturnsAsync(new List<Campaign>());

            // Act
            var result = await _sut.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<CampaignVM>>(viewResult.ViewData.Model);
            Assert.Empty(model);
        }
    }
}