# Case Study Mars Rover

According to the TDD methodology, I had to prepare tests first and then add the business codes, but since I first understood the flow in the project and then turned to possible scenarios, I added the tests later. You can check my commit history for the solution steps.

#### Solution steps of the project:
* Create a console app and wrote an algorithm that works on happy path.
* Refactored the one class app.
* Adding application loops.
* Refactored the project for Unit Test applicable.
* Adding Unit Tests.

#### Run 
Open CLI in the project folder and run the below comment.
```powershell
PS MarsRover> dotnet run --project MarsRover.ConsoleApp
```
#### Console App Instructions
Enter the dimension: 'Integer Integer' => 5 5  
Enter the rover position and direction: 'Integer Integer Direction' => 1 2 N  N: North, E: East, S: South, W: West  
Enter the rover instructions: 'LMLMMLLMM' => L: Left, R: Right, M: Move
add: adding new rover on the plateau  
go: result of the locations and directions of the rovers  
exit: closed the programme.  

## Technologies
* ASP.NET Core 5
* C# 9.0
* [FluentValidation](https://fluentvalidation.net/)
* [XUnit](https://xunit.net/), [FluentAssertions](https://fluentassertions.com/), [Moq](https://github.com/moq)

## Unit Tests
1. Have dimensions true format? 5 5
2. Has the rover position and direction true format? 1 2 N
3. Is the rover position in the plateau after the position and direction installing?
4. Has the rover instructions true format?  LMLMLMLMM 
5. Is the rover's location still on the plateau after the instructions?
6. Does the newly added rover have the same location as another rover?

### Example of the Console Application: 

Enter the dimensions:  
5 C 5  
Please write the dimension of the plateau on Mars. For example: 5 5  
Enter the dimensions:  
5  5  
Please write the dimension of the plateau on Mars. For example: 5 5  
Enter the dimensions:  
5 5 C  
Please write the dimension of the plateau on Mars. For example: 5 5  
Enter the dimensions:  
5 5  
Enter the rover position and direction:  
1 2 C  
Please write the rover position and direction correctly. For example: 5 5 N, N: North, E: East, S: South, W: West  
Enter the rover position and direction:  
1 6 N  
The rover is not in the plateau after instructions!!  
Enter the rover position and direction:  
1 2 N  
Enter the rover instructions:  
LMLMLMLMMMMMM  
The rover is not in the plateau after instructions!!  
Enter the rover position and direction:  
1 2 N  
Enter the rover instructions:  
LMLMLMLMM  
Rover added to the plateau on Mars!  
If you want to add one more rover, please write 'add'. If It is enough, please write 'go'.  
add  
Enter the rover position and direction:  
1 2 N  
Enter the rover instructions:  
LMLMLMLMM  
Please change the instructions. Newly added rover in same location as another rover!!  
Enter the rover position and direction:  
3 3 E  
Enter the rover instructions:  
MMRMMRMRRM  
Rover added to the plateau on Mars!  
If you want to add one more rover, please write 'add'. If It is enough, please write 'go'.  
go  
Results of the locations and directions of the Rovers:  
1 3 N  
5 1 E  
If you want to continue the program, press any key or write 'exit' for closing programme  
exit  
Program closed!  

 


