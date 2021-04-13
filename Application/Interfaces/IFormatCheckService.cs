namespace Application.Interfaces
{
    public interface IFormatCheckService
    {
        bool RoverRoute(string roverRoute);
        bool PositionDirection(string positionDirection);
        bool Dimension(string dimension);
    }
}