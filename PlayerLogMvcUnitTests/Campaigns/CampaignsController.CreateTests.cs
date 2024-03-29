﻿using AutoMapper;
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
    public class CreateTests
    {
        private readonly Mock<ICampaignRepository> _mockRepo;
        private readonly CampaignsController _sut;
        private readonly Mock<ILogger<CampaignRepository>> _mockLogger;
        private readonly IMapper _mapper;

        public CreateTests()
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
            Assert.False(viewResult.ViewData.ModelState.IsValid);
            Assert.Equal(newCamp.CampaignName, model.CampaignName);
        }

        [Fact]
        public async Task UnsuccessfulCreation_ReturnInternalServerError()
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
            var redirectToPageResult = Assert.IsType<RedirectToPageResult>(result);
            Assert.Equal("/InternalServerError", redirectToPageResult.PageName);
        }

        [Fact]
        public async Task ThrowsError_ReturnInternalServerError()
        {
            // Assert
            _mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<Campaign>()))
                .Throws(new AccessViolationException());

            // Act
            var result = await _sut.Create(new CampaignVM());

            // Assert
            var redirectToPageResult = Assert.IsType<RedirectToPageResult>(result);
            Assert.Equal("/InternalServerError", redirectToPageResult.PageName);
        }
    }
}
