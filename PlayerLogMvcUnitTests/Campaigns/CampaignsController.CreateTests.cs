using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PlayerLogMvc.Campaign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PlayerLogMvcUnitTests.Campaigns
{
    public class CreateTests
    {
        private readonly Mock<ICampaignRepository> _mockRepo;
        private readonly CampaignsController _sut;
        private readonly Mock<ILogger<CampaignRepository>> _logger;

        public CreateTests()
        {
            _mockRepo = new Mock<ICampaignRepository>();
            _logger = new Mock<ILogger<CampaignRepository>>();
            _sut = new CampaignsController(_mockRepo.Object, _logger.Object);
        }


        [Fact]
        public async Task ValidModel_CreateCampaignAndReturnToList()
        {
            // Arrange
            Campaign savedCamp = null;

            _mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<Campaign>()))
                .ReturnsAsync(true)
                .Callback<Campaign>(x => savedCamp = x);

            var newCamp = new CampaignVM
            {
                CampaignName = "test"
            };

            // Act
            var result = await _sut.Create(newCamp);

            // Assert
            _mockRepo.Verify(x => x.CreateAsync(It.IsAny<Campaign>()), Times.Once);

            Assert.Equal(newCamp.CampaignName, savedCamp.CampaignName);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);

        }

        [Fact]
        public async Task InvalidModel_DoNotCallTheDb()
        {
            // Arrange
            _sut.ModelState.AddModelError("x", "Test Error");

            var newCamp = new CampaignVM();

            // Act
            var result = await _sut.Create(newCamp);

            // Assert
            _mockRepo.Verify(repo => repo.CreateAsync(It.IsAny<Campaign>()), Times.Never);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<CampaignVM>(viewResult.Model);
            Assert.Equal(newCamp.CampaignName, model.CampaignName);
        }

        [Fact]
        public async Task UnsuccessfulCreation_ReturnViewWithModel()
        {
            // Arrange
            var newCamp = new CampaignVM
            {
                CampaignName = "test"
            };

            _mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<Campaign>()))
                .ReturnsAsync(false);

            // Act
            var result = await _sut.Create(newCamp);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<CampaignVM>(viewResult.Model);
            Assert.Equal(newCamp.CampaignName, model.CampaignName);
        }
    }
}
