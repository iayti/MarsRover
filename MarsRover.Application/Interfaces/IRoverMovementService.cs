using System.Collections.Generic;
using MarsRover.Domain.Entities;

namespace MarsRover.Application.Interfaces
{
    public interface IRoverMovementService
    {
        string CalculateDirection(string roverDirection, char route);

        (int, int) MoveRover(int x, int y, string direction);

        bool CheckRoverPositions(List<Rover> roverList, int roverLocationX, int roverLocationY);

        bool IsRoverInThePlateau(int roverLocationX, int plateauAreaX, int roverLocationY, int plateauAreaY);
    }
}