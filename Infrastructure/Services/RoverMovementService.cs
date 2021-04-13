using System.Collections.Generic;
using System.Linq;
using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class RoverMovementService : IRoverMovementService
    {
        /// <summary>
        /// Calculate direction of the rover.
        /// For example: roverDirection: 'N', route: 'R' => newRoverDirection will be 'E'
        /// </summary>
        /// <param name="roverDirection"></param>
        /// <param name="route"></param>
        /// <returns></returns>
        public string CalculateDirection(string roverDirection, char route)
        {
            const string allDirections = "WNES"; //West,North,East,South

            string responseDirection = "";

            for (int i = 0; i < 4; i++)
            {
                if (roverDirection == allDirections[i].ToString())
                {
                    responseDirection = route switch
                    {
                        'L' when i == 0 => allDirections[3].ToString(),
                        'L' => allDirections[i - 1].ToString(),
                        'R' when i == 3 => allDirections[0].ToString(),
                        'R' => allDirections[i + 1].ToString(),
                        _ => responseDirection
                    };
                }
            }

            return responseDirection;
        }

        /// <summary>
        /// Drives the rover in the given direction.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public (int, int) MoveRover(int x, int y, string direction)
        {
            switch (direction)
            {
                case "N":
                    y += 1;
                    break;
                case "E":
                    x += 1;
                    break;
                case "S":
                    y -= 1;
                    break;
                case "W":
                    x -= 1;
                    break;
            }

            return (x, y);
        }

        /// <summary>
        /// Checks Rover positions
        /// If more than one rover is in the same position, change the instructions!
        /// </summary>
        /// <param name="roverList"></param>
        /// <param name="roverLocationX"></param>
        /// <param name="roverLocationY"></param>
        /// <returns></returns>
        public bool CheckRoverPositions(List<Rover> roverList, int roverLocationX, int roverLocationY)
        {
            return roverList.Any(item => item.X == roverLocationX && item.Y == roverLocationY);
        }

        /// <summary>
        /// Checks rover is in the plateau after the instructions.
        /// </summary>
        /// <param name="roverLocationX"></param>
        /// <param name="plateauAreaX"></param>
        /// <param name="roverLocationY"></param>
        /// <param name="plateauAreaY"></param>
        /// <returns></returns>
        public bool IsRoverInThePlateau(int roverLocationX, int plateauAreaX, int roverLocationY, int plateauAreaY)
        {
            return roverLocationX > plateauAreaX || roverLocationX < 0 || roverLocationY > plateauAreaY || roverLocationY < 0;
        }
    }
}