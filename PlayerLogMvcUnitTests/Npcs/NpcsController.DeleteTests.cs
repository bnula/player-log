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
    public class DeleteTests
    {
        private readonly Mock<INpcRepository> _mockRepo;
        private readonly NpcsController _sut;
        private readonly IMapper _mapper;

        public DeleteTests()
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
        }

        [Fact]
        public async Task ValidId_DeleteRecordFromDb()
        {
            // Arrange
            var npc = new Npc { NpcId = 1, NpcName = "test" };
            _mockRepo.Setup(repo => repo.FindByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(npc);
            _mockRepo.Setup(repo => repo.DeleteAsync(It.IsAny<Npc>()))
                .ReturnsAsync(true);

            // Act
            var result = await _sut.Delete(1);

            // Assert
            _mockRepo.Verify(repo => repo.FindByIdAsync(It.IsAny<int>()), Times.Once);
            _mockRepo.Verify(repo => repo.DeleteAsync(It.IsAny<Npc>()), Times.Once);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task InvalidId_ReturnBadRequest()
        {
            // Arrange

            // Act
            var result = await _sut.Delete(0);
            var redirectResult = Assert.IsType<RedirectToPageResult>(result);

            // Assert
            _mockRepo.Verify(repo => repo.FindByIdAsync(It.IsAny<int>()), Times.Never);
            _mockRepo.Verify(repo => repo.DeleteAsync(It.IsAny<Npc>()), Times.Never);
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
            var result = await _sut.Delete(2);
            var redirectResult = Assert.IsType<RedirectToPageResult>(result);

            // Assert
            _mockRepo.Verify(repo => repo.FindByIdAsync(It.IsAny<int>()), Times.Once);
            _mockRepo.Verify(repo => repo.DeleteAsync(It.IsAny<Npc>()), Times.Never);
            Assert.Equal("/NotFound", redirectResult.PageName);
        }

        [Fact]
        public async Task DeleteFailed_ReturnInternalServerError()
        {
            // Arrange
            var npc = new Npc { NpcId = 1, NpcName = "test" };
            _mockRepo.Setup(repo => repo.FindByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(npc);
            _mockRepo.Setup(repo => repo.DeleteAsync(It.IsAny<Npc>()))
                .ReturnsAsync(false);

            // Act
            var result = await _sut.Delete(1);

            // Assert
            _mockRepo.Verify(repo => repo.FindByIdAsync(It.IsAny<int>()), Times.Once);
            _mockRepo.Verify(repo => repo.DeleteAsync(It.IsAny<Npc>()), Times.Once);
            var redirectToPageResult = Assert.IsType<RedirectToPageResult>(result);
            Assert.Equal("/InternalServerError", redirectToPageResult.PageName);
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
