using Application.Interfaces;
using FluentAssertions;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Case_Study_Mars_Rover.UnitTests
{
    public class FormatCheckServiceTests
    {
        private readonly IFormatCheckService _formatCheckService;

        public FormatCheckServiceTests()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<IFormatCheckService, FormatCheckService>();
            
            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            _formatCheckService = scope.ServiceProvider.GetService<IFormatCheckService>();
            
        }
        
        [Theory]
        [InlineData("LLLMMMRRR")]
        [InlineData("LMLMLMLM")]
        [InlineData("MMMRRRLLL")]
        public void RoverRoute_Should_Be_True(string dimension)
        {
            // Act
            var result = _formatCheckService.RoverRoute(dimension);
            
            // Assert
            result.Should().BeTrue();
        }
        
        [Theory]
        [InlineData("LRCCCL")]
        [InlineData("CLKME")]
        [InlineData("12312312")]
        [InlineData("")]
        [InlineData(null)]
        public void RoverRoute_Should_Be_False(string dimension)
        {
            // Act
            var result = _formatCheckService.RoverRoute(dimension);
            
            // Assert
            result.Should().BeFalse();
        }
        
        [Theory]
        [InlineData("2 2 N")]
        [InlineData("5 5 S")]
        [InlineData("2 3 E")]
        public void PositionDirection_Should_Be_True(string positionDirection)
        {
            // Act
            var result = _formatCheckService.PositionDirection(positionDirection);
            
            // Assert
            result.Should().BeTrue();
        }
        
        [Theory]
        [InlineData("2 2 C")]
        [InlineData("3 C N")]
        [InlineData("3  3 N")]
        [InlineData("")]
        [InlineData(null)]
        public void PositionDirection_Should_Be_False(string positionDirection)
        {
            // Act
            var result = _formatCheckService.PositionDirection(positionDirection);
            
            // Assert
            result.Should().BeFalse();
        }
        
        [Theory]
        [InlineData("2 2")]
        [InlineData("5 5")]
        [InlineData("2 3")]
        public void Dimension_Should_Be_True(string dimension)
        {
            // Act
            var result = _formatCheckService.Dimension(dimension);
            
            // Assert
            result.Should().BeTrue();
        }
        
        [Theory]
        [InlineData("2  2")]
        [InlineData("3 C")]
        [InlineData("3 5 ")]
        [InlineData("")]
        [InlineData(null)]
        public void Dimension_Should_Be_False(string dimension)
        {
            // Act
            var result = _formatCheckService.Dimension(dimension);
            
            // Assert
            result.Should().BeFalse();
        }
    }
}