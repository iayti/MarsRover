using Application.Interfaces;
using FluentAssertions;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Case_Study_Mars_Rover.UnitTests
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
        [InlineData("N",'L')]
        [InlineData("N",'R')]
        [InlineData("S",'R')]
        public void CalculateDirection_Must_Be_Valid(string roverDirection, char route)
        {
            // Arrange
            string allDirections = "WNES";
            
            // Act
            var result = _roverMovementService.CalculateDirection(roverDirection, route);
            
            // Assert
            allDirections.Should().Contain(result);
        }
        
    }
}