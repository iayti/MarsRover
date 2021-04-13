using System.Text.RegularExpressions;
using Application.Interfaces;

namespace Infrastructure.Services
{
    public class FormatCheckService : IFormatCheckService
    {
        /// <summary>
        /// Only accept L, M, R characters
        /// </summary>
        /// <param name="roverRoute"></param>
        /// <returns></returns>
        public bool RoverRoute(string roverRoute)
        {
            if (roverRoute == null)
                return false;
            
            Regex rx = new Regex(@"^[LMR]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            return rx.IsMatch(roverRoute);
        }

        /// <summary>
        /// Only accept {Any positive number} {Any positive number} {N,E,S,W}
        /// </summary>
        /// <param name="positionDirection"></param>
        /// <returns></returns>
        public bool PositionDirection(string positionDirection)
        {
            if (positionDirection == null)
                return false;
            
            Regex rx = new Regex(@"^\d+ \d+ [NESW]$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            return rx.IsMatch(positionDirection);
        }

        /// <summary>
        /// Only accept {Any positive number} {Any positive number}
        /// </summary>
        /// <param name="dimension"></param>
        /// <returns></returns>
        public bool Dimension(string dimension)
        {
            if (dimension == null)
                return false;
            
            Regex rx = new Regex(@"^\d+ \d+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            return rx.IsMatch(dimension);
        }
    }
}