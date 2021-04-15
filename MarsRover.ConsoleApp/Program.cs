using System;
using System.Collections.Generic;
using MarsRover.Application.Interfaces;
using MarsRover.Domain.Entities;
using MarsRover.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MarsRover.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceProvider serviceProvider = new ServiceCollection()
                .AddTransient<IRoverMovementService, RoverMovementService>()
                .AddTransient<IFormatCheckService, FormatCheckService>()
                .BuildServiceProvider();
            
            IRoverMovementService roverMovementService = serviceProvider.GetService<IRoverMovementService>();
            IFormatCheckService formatCheckService = serviceProvider.GetService<IFormatCheckService>();


            if (formatCheckService != null && roverMovementService != null)
            {
                while (true)
                {
                    if (GetDimensions(formatCheckService, out var plateauAreaX, out var plateauAreaY)) 
                        continue;

                    var roverList = new List<Rover>();

                    while (true)
                    {
                        if (GetPositionDirection(formatCheckService, roverMovementService, plateauAreaX, plateauAreaY, out var roverLocationX, out var roverLocationY, out var roverDirection)) 
                            continue;

                        if (GetInstructions(formatCheckService, roverMovementService, plateauAreaX, plateauAreaY, roverList, ref roverDirection, ref roverLocationX, ref roverLocationY)) 
                            continue;

                        var rover = new Rover(roverLocationX, roverLocationY, roverDirection);
                        roverList.Add(rover);

                        if (!Helpers.AddOrGoRover())
                            break;
                    }

                    Console.WriteLine("Results of the locations and directions of the Rovers: ");

                    foreach (var rover in roverList)
                        Console.WriteLine(rover.LocationDirection);


                    if (!Helpers.ExitOrContinueProgram())
                        break;
                }
            }

            Console.WriteLine("Program closed!");
            Console.ReadLine();
        }
        
        /// <summary>
        /// Get Instructions of the rover from user.
        /// </summary>
        /// <param name="formatCheckService"></param>
        /// <param name="roverMovementService"></param>
        /// <param name="plateauAreaX"></param>
        /// <param name="plateauAreaY"></param>
        /// <param name="roverList"></param>
        /// <param name="roverDirection"></param>
        /// <param name="roverLocationX"></param>
        /// <param name="roverLocationY"></param>
        /// <returns></returns>
        private static bool GetInstructions(IFormatCheckService formatCheckService, IRoverMovementService roverMovementService, int plateauAreaX, int plateauAreaY, List<Rover> roverList, ref string roverDirection, ref int roverLocationX, ref int roverLocationY)
        {
            Console.WriteLine("Enter the rover instructions: ");
            string roverRoute = Console.ReadLine()?.Trim();

            if (!formatCheckService.RoverRoute(roverRoute))
            {
                Console.WriteLine("Please write the rover instructions correctly. For example: LMLMLMRM, L: Left, R: Right, M: Move");
                return true;
            }

            if (roverRoute == null)
                return true;

            foreach (var route in roverRoute)
            {
                switch (route)
                {
                    case 'L' or 'R':
                        roverDirection = roverMovementService.CalculateDirection(roverDirection, route);
                        break;
                    case 'M':
                        var result = roverMovementService.MoveRover(roverLocationX, roverLocationY, roverDirection);
                        roverLocationX = result.Item1;
                        roverLocationY = result.Item2;
                        break;
                }
            }

            if (roverMovementService.IsRoverInThePlateau(roverLocationX, plateauAreaX, roverLocationY, plateauAreaY))
            {
                Console.WriteLine("The rover is not in the plateau after instructions!!");
                return true;
            }

            if (!roverMovementService.CheckRoverPositions(roverList, roverLocationX, roverLocationY)) 
                return false;
            
            Console.WriteLine("Please change the instructions. Newly added rover in same location as another rover!!");
            return true;

        }

        /// <summary>
        /// Get Position and Direction info from user.
        /// </summary>
        /// <param name="formatCheckService"></param>
        /// <param name="roverMovementService"></param>
        /// <param name="plateauAreaX"></param>
        /// <param name="plateauAreaY"></param>
        /// <param name="roverLocationX"></param>
        /// <param name="roverLocationY"></param>
        /// <param name="roverDirection"></param>
        /// <returns></returns>
        private static bool GetPositionDirection(IFormatCheckService formatCheckService, IRoverMovementService roverMovementService, int plateauAreaX, int plateauAreaY, out int roverLocationX, out int roverLocationY, out string roverDirection)
        {
            Console.WriteLine("Enter the rover position and direction: ");

            string positionDirection = Console.ReadLine()?.Trim();

            if (!formatCheckService.PositionDirection(positionDirection))
            {
                Console.WriteLine("Please write the rover position and direction correctly. For example: 5 5 N, N: North, E: East, S: South, W: West");
                roverLocationX = 0;
                roverLocationY = 0;
                roverDirection = null;
                return true;
            }

            if (positionDirection == null)
            {
                roverLocationX = 0;
                roverLocationY = 0;
                roverDirection = null;
                return true;
            }

            string[] positionDirectionArray = positionDirection.Split(" ");

            roverLocationX = int.Parse(positionDirectionArray[0]);
            roverLocationY = int.Parse(positionDirectionArray[1]);
            roverDirection = positionDirectionArray[2];

            if (!roverMovementService.IsRoverInThePlateau(roverLocationX, plateauAreaX, roverLocationY, plateauAreaY)) 
                return false;
            
            Console.WriteLine("The rover is not in the plateau after instructions!!");
            return true;
        }

        /// <summary>
        /// Get the area of the plateau on Mars from user.
        /// </summary>
        /// <param name="formatCheckService"></param>
        /// <param name="plateauAreaX"></param>
        /// <param name="plateauAreaY"></param>
        /// <returns></returns>
        private static bool GetDimensions(IFormatCheckService formatCheckService, out int plateauAreaX, out int plateauAreaY)
        {
            Console.WriteLine("Enter the dimensions: ");
            string dimension = Console.ReadLine()?.Trim();

            plateauAreaX = 0;
            plateauAreaY = 0;
            
            if (!formatCheckService.Dimension(dimension))
            {
                Console.WriteLine("Please write the dimension of the plateau on Mars. For example: 5 5");
                return true;
            }

            if (dimension == null) 
                return false;
            
            string[] dimensionArray = dimension.Split(" ");

            plateauAreaX = int.Parse(dimensionArray[0]);
            plateauAreaY = int.Parse(dimensionArray[1]);

            return false;
        }
    }
}