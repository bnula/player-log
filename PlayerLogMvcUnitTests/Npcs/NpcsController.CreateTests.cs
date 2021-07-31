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
    public class CreateTests
    {
        private readonly Mock<INpcRepository> _mockRepo;
        private readonly Mock<ILogger<NpcRepository>> _mockLogger;
        private readonly NpcsController _sut;
        private readonly IMapper _mapper;
        private NpcDetailsVM  _newNpc;

        public CreateTests()
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
            _newNpc = new NpcDetailsVM
            {
                NpcName = "test1",
                Description = "test1",
                Notes = "test1",
                Allegiance = "test1",
                Campaign = new Campaign { CampaignId = 1 },
                CurrentLocation = new Location { LocationId = 1 },
                HomeLocation = new Location { LocationId = 1 }
            };
        }

        [Fact]
        public async Task ValidModel_CreateItemInDbAndReturnToIndex()
        {
            // Arrange
            Npc savedNpc = null;
            _mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<Npc>()))
                .ReturnsAsync(true)
                .Callback<Npc>(x => savedNpc = x);

            // Act
            var result = await _sut.Create(_newNpc);

            // Assert
            _mockRepo.Verify(repo => repo.CreateAsync(It.IsAny<Npc>()), Times.Once);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal(_newNpc.NpcName, savedNpc.NpcName);
        }

        [Fact]
        public async Task InvalidModel_ReturnViewWithModel()
        {
            // Arrange
            _sut.ModelState.AddModelError("x", "test error");

            // Act
            var result = await _sut.Create(_newNpc);

            // Assert
            _mockRepo.Verify(repo => repo.CreateAsync(It.IsAny<Npc>()), Times.Never);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(_newNpc, viewResult.Model);
        }

        [Fact]
        public async Task CreationFailed_ReturnInternalServerError()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<Npc>()))
                .ReturnsAsync(false);

            // Act
            var result = await _sut.Create(_newNpc);

            // Assert
            var redirectToPageResult = Assert.IsType<RedirectToPageResult>(result);
            Assert.Equal("/InternalServerError", redirectToPageResult.PageName);
        }

        [Fact]
        public async Task ThrowsError_ReturnInternalServerError()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<Npc>()))
                .Throws(new Exception ());

            // Act
            var result = await _sut.Create(_newNpc);

            // Assert
            var redirectToPageResult = Assert.IsType<RedirectToPageResult>(result);
            Assert.Equal("/InternalServerError", redirectToPageResult.PageName);
        }
    }
}
