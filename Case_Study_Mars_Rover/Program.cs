using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Case_Study_Mars_Rover
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO: If the "exit" enters loop will be finished.

            while (true)
            {
                //Get dimension of the plateau on Mars
                string dimension = Console.ReadLine()?.Trim();

                if (!DimensionFormatCheck(dimension))
                    Console.WriteLine("Please write the dimension of the plateau on Mars. For example: 5 5");
                else
                {
                    int plateauAreaX = 0;
                    int plateauAreaY = 0;

                    if (dimension != null)
                    {
                        string[] dimensionArray = dimension.Split(" ");

                        plateauAreaX = int.Parse(dimensionArray[0]);
                        plateauAreaY = int.Parse(dimensionArray[1]);
                    }


                    var roverList = new List<Rover>();

                    #region While Loop

                    string positionDirection = Console.ReadLine()?.Trim();

                    if (!PositionDirectionFormatCheck(positionDirection))
                        Console.WriteLine("Please write the rover position and direction correctly. For example: 5 5 N, N: North, E: East, S: South, W: West");
                    else
                    {
                        if (positionDirection != null)
                        {
                            string[] positionDirectionArray = positionDirection.Split(" ");

                            int roverLocationX = int.Parse(positionDirectionArray[0]);
                            int roverLocationY = int.Parse(positionDirectionArray[1]);
                            string roverDirection = positionDirectionArray[2];

                            Rover rover = new Rover(roverLocationX, roverLocationY, roverDirection);

                            string roverRoute = Console.ReadLine()?.Trim();

                            if (roverLocationX > plateauAreaX || roverLocationX < 0 || roverLocationY > plateauAreaY || roverLocationY < 0)
                            {
                                //Try again break
                                Console.WriteLine("The rover is not in the plateau");
                            }

                            if (roverRoute != null)
                            {
                                foreach (var route in roverRoute)
                                {
                                    if (route == 'L' || route == 'R')
                                    {
                                        rover.Direction = CalculateDirection(rover.Direction, route);
                                    }
                                    else if (route == 'M')
                                    {
                                        MoveRover(rover);
                                    }
                                }
                                //rover can be created and added in the end of the line.
                                //
                                roverList.Add(rover);
                            }
                        }
                    }

                    #endregion
                    
                    //TODO: after the loop ends Console.WriteLine(Each.roverlist.LocationDirection)
                }
            }

        }

        /// <summary>
        /// Drives the rover in the given direction
        /// </summary>
        /// <param name="rover"></param>
        public static void MoveRover(Rover rover)
        {
            switch (rover.Direction)
            {
                case "N":
                    rover.Y += 1;
                    break;
                case "E":
                    rover.X += 1;
                    break;
                case "S":
                    rover.Y -= 1;
                    break;
                case "W":
                    rover.X -= 1;
                    break;
            }
        }

        /// <summary>
        /// Calculate direction of the rover.
        /// For example: roverDirection: 'N', route: 'R' => newRoverDirection will be 'E'
        /// </summary>
        /// <param name="roverDirection"></param>
        /// <param name="route"></param>
        /// <returns></returns>
        public static string CalculateDirection(string roverDirection, char route)
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
        /// Only accept {Any positive number} {Any positive number}
        /// </summary>
        /// <param name="dimension"></param>
        /// <returns></returns>
        public static bool DimensionFormatCheck(string dimension)
        {
            Regex rx = new Regex(@"^\d+ \d+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            return rx.IsMatch(dimension);
        }

        /// <summary>
        /// Only accept {Any positive number} {Any positive number} {N,E,S,W}
        /// </summary>
        /// <param name="positionDirection"></param>
        /// <returns></returns>
        public static bool PositionDirectionFormatCheck(string positionDirection)
        {
            Regex rx = new Regex(@"^\d+ \d+ [NESW]$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            return rx.IsMatch(positionDirection);
        }

        /// <summary>
        /// Only accept L, M, R characters
        /// </summary>
        /// <param name="roverRoute"></param>
        /// <returns></returns>
        public static bool RoverRouteFormatCheck(string roverRoute)
        {
            Regex rx = new Regex(@"^[LMR]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            return rx.IsMatch(roverRoute);
        }
    }
}