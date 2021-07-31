using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PlayerLogMvc.Campaigns;
using PlayerLogMvc.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PlayerLogMvcUnitTests.Campaigns
{
    public class DetailsTests
    {
        private readonly Mock<ICampaignRepository> _mockRepo;
        private readonly CampaignsController _sut;
        private readonly Mock<ILogger<CampaignRepository>> _mockLogger;
        private readonly IMapper _mapper;

        public DetailsTests()
        {
            _mockRepo = new Mock<ICampaignRepository>();
            _mockLogger = new Mock<ILogger<CampaignRepository>>();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile(new Maps());
            });
            _mapper = mapperConfig.CreateMapper();
            _sut = new CampaignsController(_mockRepo.Object, _mockLogger.Object, _mapper);
        }

        [Fact]
        public async Task ValidId_ReturnViewWithViewModel()
        {
            // Arrange
            var camp = new Campaign
            {
                CampaignId = 1,
                CampaignName = "test"
            };

            _mockRepo.Setup(repo => repo.FindByIdAsync(1))
                .ReturnsAsync(camp);
            

            // Act
            var result = await _sut.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<CampaignDetailsVM>(viewResult.Model);
            Assert.Equal(camp.CampaignName, model.CampaignName);
        }

        [Fact]
        public async Task InvalidId_ReturnBadRequest()
        {
            // Arrange

            // Act
            var result = await _sut.Details(0);
            var redirectResult = Assert.IsType<RedirectToPageResult>(result);

            // Assert
            _mockRepo.Verify(repo => repo.FindByIdAsync(It.IsAny<int>()), Times.Never);
            Assert.Equal("/BadRequest", redirectResult.PageName);
        }

        [Fact]
        public async Task NonExistentId_ReturnNotFound()
        {
            // Arrange
            Campaign camp = null;

            _mockRepo.Setup(repo => repo.FindByIdAsync(2))
                .ReturnsAsync(camp);

            // Act
            var result = await _sut.Details(2);
            var redirectResult = Assert.IsType<RedirectToPageResult>(result);

            // Assert
            _mockRepo.Verify(repo => repo.FindByIdAsync(It.IsAny<int>()), Times.Once);
            Assert.Equal("/NotFound", redirectResult.PageName);
        }

        [Fact]
        public async Task ThrowsError_ReturnInternalServerError()
        {
            // Assert
            _mockRepo.Setup(repo => repo.FindByIdAsync(It.IsAny<int>()))
                .Throws(new AccessViolationException());

            // Act
            var result = await _sut.Edit(1);

            // Assert
            var redirectToPageResult = Assert.IsType<RedirectToPageResult>(result);
            Assert.Equal("/InternalServerError", redirectToPageResult.PageName);
        }
    }
}
