using System.Collections.Generic;
using MarsRover.Application.Interfaces;
using FluentAssertions;
using MarsRover.Domain.Entities;
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

        [Theory]
        [InlineData(1, 2)]
        [InlineData(3, 4)]
        [InlineData(1, 5)]
        public void CheckRoverPositions_Should_Be_False(int x, int y)
        {
            // Arrange
            List<Rover> roverList = new List<Rover>
            {
                new Rover(2, 2, "N"),
                new Rover(3, 3, "W")
            };

            // Act
            var result = _roverMovementService.CheckRoverPositions(roverList, x, y);

            // Assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        public void CheckRoverPositions_Should_Be_True(int x, int y)
        {
            // Arrange
            List<Rover> roverList = new List<Rover>
            {
                new Rover(2, 2, "N"),
                new Rover(3, 3, "W")
            };

            // Act
            var result = _roverMovementService.CheckRoverPositions(roverList, x, y);

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData(2, 5, 3, 5)]
        [InlineData(8, 10, 5, 5)]
        [InlineData(2, 4, 3, 4)]
        public void IsRoverInThePlateau_Should_Be_False(int roverLocationX, int plateauAreaX, int roverLocationY, int plateauAreaY)
        {
            // Act
            var result = _roverMovementService.IsRoverInThePlateau(roverLocationX, plateauAreaX, roverLocationY, plateauAreaY);

            // Assert
            result.Should().BeFalse();
        }
        
        [Theory]
        [InlineData(6, 5, 3, 5)]
        [InlineData(11, 10, 5, 5)]
        [InlineData(2, 4, 6, 4)]
        public void IsRoverInThePlateau_Should_Be_True(int roverLocationX, int plateauAreaX, int roverLocationY, int plateauAreaY)
        {
            // Act
            var result = _roverMovementService.IsRoverInThePlateau(roverLocationX, plateauAreaX, roverLocationY, plateauAreaY);

            // Assert
            result.Should().BeTrue();
        }
    }
}