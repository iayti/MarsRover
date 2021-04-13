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
                Console.WriteLine("Enter the dimensions: ");
                //Get dimension of the plateau on Mars
                string dimension = Console.ReadLine()?.Trim();

                if (!DimensionFormatCheck(dimension))
                {
                    Console.WriteLine("Please write the dimension of the plateau on Mars. For example: 5 5");
                    continue;
                }

                int plateauAreaX = 0;
                int plateauAreaY = 0;

                if (dimension != null)
                {
                    string[] dimensionArray = dimension.Split(" ");

                    plateauAreaX = int.Parse(dimensionArray[0]);
                    plateauAreaY = int.Parse(dimensionArray[1]);
                }


                var roverList = new List<Rover>();

                while (true)
                {
                    Console.WriteLine("Enter the rover position and direction: ");

                    string positionDirection = Console.ReadLine()?.Trim();

                    if (!PositionDirectionFormatCheck(positionDirection))
                    {
                        Console.WriteLine("Please write the rover position and direction correctly. For example: 5 5 N, N: North, E: East, S: South, W: West");
                        continue;
                    }

                    if (positionDirection != null)
                    {
                        string[] positionDirectionArray = positionDirection.Split(" ");

                        int roverLocationX = int.Parse(positionDirectionArray[0]);
                        int roverLocationY = int.Parse(positionDirectionArray[1]);
                        string roverDirection = positionDirectionArray[2];

                        //Rover rover = new Rover(roverLocationX, roverLocationY, roverDirection);

                        string roverRoute = Console.ReadLine()?.Trim();

                        if (roverLocationX > plateauAreaX || roverLocationX < 0 || roverLocationY > plateauAreaY || roverLocationY < 0)
                        {
                            Console.WriteLine("The rover is not in the plateau");
                            continue;
                        }

                        if (roverRoute == null)
                            continue;

                        foreach (var route in roverRoute)
                        {
                            switch (route)
                            {
                                case 'L' or 'R':
                                    roverDirection = CalculateDirection(roverDirection, route);
                                    break;
                                case 'M':
                                    var result = MoveRover(roverLocationX, roverLocationY, roverDirection);
                                    roverLocationX = result.Item1;
                                    roverLocationY = result.Item2;
                                    break;
                            }
                        }

                        Rover rover = new Rover(roverLocationX, roverLocationY, roverDirection);
                        roverList.Add(rover);

                        Add_Rover:
                        Console.WriteLine("Rover added to the plateau on Mars!");
                        Console.WriteLine("If you want to add one more rover, please write 'add'");
                        Console.WriteLine("If there is enough rover, please write 'go'");

                        string state = Console.ReadLine()?.Trim();

                        if (!string.IsNullOrEmpty(state))
                        {
                            if (state == "add")
                                continue;
                            if (state == "go")
                                break;

                            goto Add_Rover;
                        }
                    }
                }

                foreach (var rover in roverList)
                {
                    Console.WriteLine(rover.LocationDirection);
                }

                Console.WriteLine("If you want to exit the program, please write 'exit'");
                Console.WriteLine("If you want to continue the program, please write any key.");

                string programState = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(programState))
                    continue;
                if (programState == "exit")
                    break;
            }

            Console.WriteLine("Program closed!");
            Console.ReadLine();
        }

        /// <summary>
        /// Drives the rover in the given direction.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static (int, int) MoveRover(int x, int y, string direction)
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