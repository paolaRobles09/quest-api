using Microsoft.AspNetCore.Mvc;
using Moq;
using Quest.API.Controllers;
using Quest.Core.Dto.Requests;
using Quest.Core.Dto.Responses;
using Quest.Core.Entities;
using Quest.Core.Interfaces;
using Quest.Infrastructure.Data;

namespace Quest.Tests
{
    public class QuestControllerTests
    {
        private readonly Mock<IQuestService> _mockService;
        private readonly QuestController _controller;

        public QuestControllerTests()
        {
            _mockService = new Mock<IQuestService>();
            _controller = new QuestController(_mockService.Object);
        }

        [Fact]
        public async Task GetState_ReturnsOkResult_WithStateResponse()
        {
            // Arrange
            var playerQuestState = new PlayerState
            {
                PlayerId = "player1",
                TotalQuestPercentCompleted = 4,
                LastMilestoneIndexCompleted = 1,
                QuestPointsEarned = 201
            };

            var progressRequest = new ProgressRequest(playerQuestState.PlayerId, 1, 200);

            _mockService.Setup(x => x.GetPlayerState(playerQuestState.PlayerId)).ReturnsAsync(playerQuestState);

            // Act
            var result = await _controller.Get(playerQuestState.PlayerId);

            // Assert
            var typeResult = Assert.IsAssignableFrom<ActionResult>(result);
            var okResult = Assert.IsType<OkObjectResult>(typeResult);
            var stateResponse = Assert.IsType<StateResponse>(okResult.Value);
            Assert.Equal(1, stateResponse.LastMilestoneIndexCompleted);
            Assert.Equal(4, stateResponse.TotalQuestPercentCompleted);
        }

        [Fact]
        public async Task GetState_ReturnsNotFound()
        {
            // Arrange
            string playerId = "player1";
            _mockService.Setup(x => x.GetPlayerState(playerId));

            // Act
            var result = await _controller.Get(playerId);

            // Assert
            var typeResult = Assert.IsAssignableFrom<ActionResult>(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task PostProgress_ReturnsOkResult_WithProgressResponse_NoMilestones() 
        {
            // Arrange
            var playerQuestState = new PlayerQuestState
            {
                PlayerId = "player1",
                TotalQuestPercentCompleted = 5.0f,
                LastMilestoneIndexCompleted = 0,
                QuestPointsEarned = 30
            };

            var response = new ProgressResponse(30, 5, []);

            var progressRequest = new ProgressRequest(playerQuestState.PlayerId, 1, 200);

            _mockService.Setup(x => x.AddOrUpdatePlayerQuestState(progressRequest)).ReturnsAsync(response);

            // Act
            var result = await _controller.PostProgress(progressRequest);

            // Assert
            var typeResult = Assert.IsAssignableFrom<ActionResult>(result);
            var okResult = Assert.IsType<OkObjectResult>(typeResult);
            var progressResponse = Assert.IsType<ProgressResponse>(okResult.Value);
            Assert.Equal(30, progressResponse.QuestPointsEarned);
            Assert.Equal(5, progressResponse.TotalQuestPercentCompleted);
            Assert.Equal(0, progressResponse.MilestonesCompleted?.Count);

        }

        [Fact]
        public async Task PostProgress_ReturnsOkResult_WithProgressResponse_WithMilestones()
        {
            // Arrange
            var playerQuestState = new PlayerQuestState
            {
                PlayerId = "player1",
                TotalQuestPercentCompleted = 20.0f,
                LastMilestoneIndexCompleted = 0,
                QuestPointsEarned = 150
            };

            var response = new ProgressResponse(150, 20, [new(1, 200)]);

            var request = new ProgressRequest(playerQuestState.PlayerId, 1, 500);

            _mockService.Setup(x => x.AddOrUpdatePlayerQuestState(request)).ReturnsAsync(response);

            // Act
            var result = await _controller.PostProgress(request);

            // Assert
            var typeResult = Assert.IsAssignableFrom<ActionResult>(result);
            var okResult = Assert.IsType<OkObjectResult>(typeResult);
            var progressResponse = Assert.IsType<ProgressResponse>(okResult.Value);
            Assert.Equal(150, progressResponse.QuestPointsEarned);
            Assert.Equal(20, progressResponse.TotalQuestPercentCompleted);
            Assert.Equal(1, progressResponse.MilestonesCompleted?.Count);

        }
    }
}
