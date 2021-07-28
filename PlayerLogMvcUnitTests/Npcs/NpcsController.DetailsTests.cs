using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PlayerLogMvc.Campaigns;
using PlayerLogMvc.Locations;
using PlayerLogMvc.Npcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PlayerLogMvcUnitTests.Npcs
{
    public class DetailsTests
    {
        Mock<INpcRepository> _mockRepo;
        NpcsController _sut;
        Npc _savedNpc;
        NpcDetailsVM _updatedNpc;

        public DetailsTests()
        {
            _mockRepo = new Mock<INpcRepository>();
            var mockLogger = new Mock<ILogger<NpcRepository>>();
            var mockCampRepo = new Mock<ICampaignRepository>();
            var mockLocRepo = new Mock<ILocationRepository>();
            _sut = new NpcsController(_mockRepo.Object, mockLogger.Object, campRepo: mockCampRepo.Object, locRepo: mockLocRepo.Object);
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
        public async Task ValidId_ReturnViewWithViewModel()
        {
            // Arrange
            var npc = new Npc
            {
                NpcId = 1,
                NpcName = "test"
            };

            _mockRepo.Setup(repo => repo.FindByIdAsync(1))
                .ReturnsAsync(npc);


            // Act
            var result = await _sut.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<NpcDetailsVM>(viewResult.Model);
            Assert.Equal(npc.NpcName, model.NpcName);
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
            Npc npc = null;

            _mockRepo.Setup(repo => repo.FindByIdAsync(2))
                .ReturnsAsync(npc);

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
