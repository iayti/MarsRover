using MarsRover.Application.Interfaces;
using FluentAssertions;
using MarsRover.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace MarsRover.UnitTests
{
    public class RoverMovementServiceTests
    {
        private readonly IRoverMovementService _roverMovementService;

        public RoverMovementServiceTests()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<IRoverMovementService, RoverMovementService>();

            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            _roverMovementService = scope.ServiceProvider.GetService<IRoverMovementService>();
        }

        [Theory]
        [InlineData("N", 'L', "W")]
        [InlineData("N", 'R', "E")]
        [InlineData("S", 'R', "W")]
        public void CalculateDirection_Should_Be_Valid(string roverDirection, char route, string expectedDirection)
        {
            // Arrange
            string allDirections = "WNES";

            // Act
            var result = _roverMovementService.CalculateDirection(roverDirection, route);

            // Assert
            allDirections.Should().Contain(result);
            result.Should().BeEquivalentTo(expectedDirection);
        }

        [Theory]
        [InlineData(1, 2, "N", 1, 3)]
        [InlineData(3, 3, "W", 2, 3)]
        [InlineData(1, 2, "S", 1, 1)]
        public void MoveRover_Should_Be_Valid(int x, int y, string direction, int expectedX, int expectedY)
        {
            // Act
            var result = _roverMovementService.MoveRover(x, y, direction);

            // Assert
            result.Item1.Should().Be(expectedX);
            result.Item2.Should().Be(expectedY);
        }
    }
}