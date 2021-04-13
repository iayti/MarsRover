using System;

namespace Case_Study_Mars_Rover
{
    public static class Helpers
    {
        /// <summary>
        /// Decide programme Exit or Continue
        /// </summary>
        /// <returns></returns>
        public static bool ExitOrContinueProgram()
        {
            Console.WriteLine("If you want to continue the program, press any key or write 'exit' for closing programme");

            string programState = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(programState))
                return true;
            return programState != "exit";
        }
        
        /// <summary>
        /// Decide Add one more rover or Go to results
        /// </summary>
        /// <returns></returns>
        public static bool AddOrGoRover()
        {
            Add_Rover:
            Console.WriteLine("Rover added to the plateau on Mars!");
            Console.WriteLine("If you want to add one more rover, please write 'add'. If It is enough, please write 'go'.");

            string state = Console.ReadLine()?.Trim();

            if (!string.IsNullOrEmpty(state))
            {
                if (state == "add")
                    return true;
                if (state == "go")
                    return false;

                goto Add_Rover;
            }

            return false;
        }
    }
}