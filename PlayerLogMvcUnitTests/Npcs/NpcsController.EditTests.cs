using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PlayerLogMvc.Campaigns;
using PlayerLogMvc.Locations;
using PlayerLogMvc.Mappings;
using PlayerLogMvc.Npcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PlayerLogMvcUnitTests.Npcs
{
    public class EditTests
    {
        private readonly Mock<INpcRepository> _mockRepo;
        private readonly NpcsController _sut;
        private readonly IMapper _mapper;
        private Npc _savedNpc;
        private NpcDetailsVM _updatedNpc;

        public EditTests()
        {
            _mockRepo = new Mock<INpcRepository>();
            var mockLogger = new Mock<ILogger<NpcRepository>>();
            var mockCampRepo = new Mock<ICampaignRepository>();
            var mockLocRepo = new Mock<ILocationRepository>();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile(new Maps());
            });
            _mapper = mapperConfig.CreateMapper();
            _sut = new NpcsController(_mockRepo.Object, mockLogger.Object, campRepo: mockCampRepo.Object, locRepo: mockLocRepo.Object, mapper: _mapper);
            _savedNpc = new Npc
            {
                NpcName = "test1",
                Description = "test1",
                Notes = "test1",
                Allegiance = "test1",
                Campaign = new Campaign { CampaignId = 1 },
                CurrentLocation = new Location { LocationId = 1 },
                HomeLocation = new Location { LocationId = 1 }
            };
            _updatedNpc = new NpcDetailsVM
            {
                NpcName = "changed",
                Description = "changed",
                Notes = "changed",
                Allegiance = "changed",
                Campaign = new Campaign { CampaignId = 1 },
                CurrentLocation = new Location { LocationId = 1 },
                HomeLocation = new Location { LocationId = 1 }
            };
        }

        [Fact]
        public async Task ValidId_ReturnViewWithModel()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.FindByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(_savedNpc);

            // Act
            var result = await _sut.Edit(1);

            // Assert
            _mockRepo.Verify(repo => repo.FindByIdAsync(It.IsAny<int>()), Times.Once);
            _mockRepo.Verify(repo => repo.UpdateAsync(It.IsAny<Npc>()), Times.Never);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<NpcDetailsVM>(viewResult.Model);
            Assert.Equal("Edit", viewResult.ViewName);
            Assert.Equal(_savedNpc.NpcName, model.NpcName);
        }

        [Fact]
        public async Task PostValidModel_UpdateItemInDbAndReturnToIndex()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Npc>()))
                .ReturnsAsync(true)
                .Callback<Npc>(x => _savedNpc = x);

            // Act
            var result = await _sut.Edit(_updatedNpc);

            // Assert
            _mockRepo.Verify(repo => repo.UpdateAsync(It.IsAny<Npc>()), Times.Once);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal(_updatedNpc.NpcName, _savedNpc.NpcName);
        }

        [Fact]
        public async Task PostFailedUpdate_ReturnViewWithModel()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Npc>()))
                .ReturnsAsync(false);

            // Act
            var result = await _sut.Edit(_updatedNpc);

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
            var result = await _sut.Edit(_updatedNpc);

            // Assert
            _mockRepo.Verify(repo => repo.UpdateAsync(It.IsAny<Npc>()), Times.Never);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<NpcDetailsVM>(viewResult.Model);
            Assert.False(viewResult.ViewData.ModelState.IsValid);
            Assert.Equal(_updatedNpc.NpcName, model.NpcName);
        }

        [Fact]
        public async Task NullModel_ReturnBadRequest()
        {
            // Arrange
            NpcDetailsVM npc = null;

            // Act
            var result = await _sut.Edit(npc);

            // Assert
            _mockRepo.Verify(repo => repo.UpdateAsync(It.IsAny<Npc>()), Times.Never);
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
            Npc npc = null;

            _mockRepo.Setup(repo => repo.FindByIdAsync(2))
                .ReturnsAsync(npc);

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
