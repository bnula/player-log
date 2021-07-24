using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PlayerLogMvc.Campaign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PlayerLogMvcUnitTests
{
    public class CampaignsControllerTests
    {

        private readonly Mock<ICampaignRepository> _mockRepo;
        private readonly Mock<ILogger<CampaignRepository>> _mockLogger;
        private readonly CampaignsController _controller;

        public CampaignsControllerTests()
        {
            _mockRepo = new Mock<ICampaignRepository>();
            _mockLogger = new Mock<ILogger<CampaignRepository>>();
            _controller = new CampaignsController(_mockRepo.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfCampaigns()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.FindAllAsync())
                .ReturnsAsync(GetTestCamps());

            // Act
            var result = await _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<CampaignVM>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task Index_NoExistingCampaigns_ReturnNotFound()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.FindAllAsync().Result).Returns(new List<Campaign>());

            // Act
            var result = await _controller.Index();

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        private List<Campaign> GetTestCamps()
        {
            var camps = new List<Campaign>();
            camps.Add(new Campaign
            {
                CampaignId = 1,
                CampaignName = "test 1"
            });
            camps.Add(new Campaign
            {
                CampaignId = 2,
                CampaignName = "test 2"
            });
            
            return camps;
        }
    }
}