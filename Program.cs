using System.Diagnostics.Metrics;
using System.Text;





namespace Mazescape
{
    internal class Program
    {
        private static readonly ApplicationSettings _appSettings = new();



        static void Main()
        {
        LabelMethodBeginning:

            Console.Title = "Mazescape";
            Console.OutputEncoding = Encoding.UTF8;

            Console.Clear();
            Console.SetCursorPosition(0, 4);
            Console.ForegroundColor = ConsoleColor.Green;



            Console.WriteLine("           ┳┳┓                ");
            Console.WriteLine("           ┃┃┃┏┓┓┏┓┏┏┏┓┏┓┏┓   ");
            Console.WriteLine("           ┛ ┗┗┻┗┗ ┛┗┗┻┣┛┗    ");
            Console.Write("                       ┛     ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("│");
            Console.WriteLine("        ┌────────────────────┘");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();



            int mazeHeight = ReadInputAsInteger("Adjust the height for the maze:", 10, _appSettings.maxMazeLayoutHeight);
            int mazeWidth = ReadInputAsInteger("Adjust the width for the maze:", 30, _appSettings.maxMazeLayoutWidth);

            int maxCountOfObstacles = mazeHeight * mazeWidth * _appSettings.maxPercentOfObstaclesInLayout / 100;
            int obstacleCount = ReadInputAsInteger("Define the amount of obstacles:", maxCountOfObstacles / 3, maxCountOfObstacles);
            


            MazeLayout mazeLayout = new(mazeHeight, mazeWidth);

            MazeLayout.Initalize();
            MazeLayout.PlaceObstacles(obstacleCount);
            MazeLayout.PlaceStartAndEndLocation();



            Console.Clear();
            Console.SetCursorPosition(0, 4);
            Console.ForegroundColor = ConsoleColor.White;

            MazeLayout.Display();



            goto LabelMethodBeginning;
        }

        private static int ReadInputAsInteger(string inputPrompt, int presetInput, int maxInput)
        {
            Console.CursorVisible = false;
            
            int chosenInput = presetInput;
            bool inputAdjusted = false;

            while (inputAdjusted == false)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"        │ {inputPrompt} ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{chosenInput}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("        └─────");

                char pressedKey = (char)Console.ReadKey(true).Key;
                
                switch (pressedKey)
                {
                    case (char)ConsoleKey.UpArrow:
                        if (chosenInput + 1 <= maxInput)
                        {
                            chosenInput = chosenInput + 1;
                        }
                        Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 2);
                        Console.WriteLine(new string(' ', Console.BufferWidth));
                        Console.WriteLine(new string(' ', Console.BufferWidth));
                        Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 2);
                        break;

                    case (char)ConsoleKey.DownArrow:
                        if (chosenInput - 1 >= 5)
                        {
                            chosenInput = chosenInput - 1;
                        }
                        Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 2);
                        Console.WriteLine(new string(' ', Console.BufferWidth));
                        Console.WriteLine(new string(' ', Console.BufferWidth));
                        Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 2);
                        break;

                    case (char)ConsoleKey.Enter:
                        inputAdjusted = true;
                        Console.WriteLine();
                        break;

                    case (char)ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;

                    default:
                        Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 2);
                        Console.WriteLine(new string(' ', Console.BufferWidth));
                        Console.WriteLine(new string(' ', Console.BufferWidth));
                        Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 2);
                        break;
                }
            }

            Console.CursorVisible = true;

            return chosenInput;
        }
    }
}