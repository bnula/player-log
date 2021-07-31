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
    public class EditTests
    {
        private readonly Mock<ICampaignRepository> _mockRepo;
        private readonly Mock<ILogger<CampaignRepository>> _mockLogger;
        private readonly CampaignsController _sut;
        private Campaign _savedCamp;
        private CampaignVM _updatedCamp;
        private readonly IMapper _mapper;

        public EditTests()
        {
            _mockRepo = new Mock<ICampaignRepository>();
            _mockLogger = new Mock<ILogger<CampaignRepository>>();
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new Maps());
            });
            _mapper = mapperConfig.CreateMapper();
            _sut = new CampaignsController(_mockRepo.Object, _mockLogger.Object, _mapper);
            _savedCamp = new Campaign
            {
                CampaignId = 1,
                CampaignName = "test"
            };
            _updatedCamp = new CampaignVM
            {
                CampaignId = 1,
                CampaignName = "changed"
            };
    }

        [Fact]
        public async Task ValidId_ReturnViewWithModel()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.FindByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(_savedCamp);

            // Act
            var result = await _sut.Edit(1);

            // Assert
            _mockRepo.Verify(repo => repo.FindByIdAsync(It.IsAny<int>()), Times.Once);
            _mockRepo.Verify(repo => repo.UpdateAsync(It.IsAny<Campaign>()), Times.Never);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<CampaignVM>(viewResult.Model);
            Assert.Equal("Edit", viewResult.ViewName);
            Assert.Equal(_savedCamp.CampaignName, model.CampaignName);
        }

        [Fact]
        public async Task Post_UpdateItemAndReturnToIndex()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Campaign>()))
                .ReturnsAsync(true)
                .Callback<Campaign>(x => _savedCamp = x);

            // Act
            var result = await _sut.Edit(_updatedCamp);

            // Assert
            _mockRepo.Verify(repo => repo.UpdateAsync(It.IsAny<Campaign>()), Times.Once);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal(_savedCamp.CampaignName, _updatedCamp.CampaignName);
        }

        [Fact]
        public async Task PostFailedUpdate_ReturnViewWithModel()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Campaign>()))
                .ReturnsAsync(false);

            // Act
            var result = await _sut.Edit(_updatedCamp);

            // Assert
            var redirectToPageResult = Assert.IsType<RedirectToPageResult>(result);
            Assert.Equal("/InternalServerError", redirectToPageResult.PageName);
        }

        [Fact]
        public async Task InvalidModel_ReturnViewWithModel()
        {
            // Arrange
            _sut.ModelState.AddModelError("x", "Test error");

            // Act
            var result = await _sut.Edit(_updatedCamp);

            // Assert
            _mockRepo.Verify(repo => repo.UpdateAsync(It.IsAny<Campaign>()), Times.Never);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<CampaignVM>(viewResult.Model);
            Assert.False(viewResult.ViewData.ModelState.IsValid);
            Assert.Equal(_updatedCamp.CampaignName, model.CampaignName);
        }

        [Fact]
        public async Task NullModel_ReturnBadRequest()
        {
            // Arrange
            CampaignVM camp = null;

            // Act
            var result = await _sut.Edit(camp);

            // Assert
            _mockRepo.Verify(repo => repo.UpdateAsync(It.IsAny<Campaign>()), Times.Never);
            var redirectToPageResult = Assert.IsType<RedirectToPageResult>(result);
            Assert.Equal("/BadRequest", redirectToPageResult.PageName);
        }

        [Fact]
        public async Task InvalidId_ReturnBadRequest()
        {
            // Arrange

            // Act
            var result = await _sut.Edit(0);
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
            var result = await _sut.Edit(2);
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
