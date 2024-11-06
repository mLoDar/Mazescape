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
            Console.ForegroundColor = ConsoleColor.White;



            int mazeHeight = ReadInputAsInteger("Enter a height for the maze:", _appSettings.maxMazeLayoutHeight);
            int mazeWidth = ReadInputAsInteger("Enter a width for the maze:", _appSettings.maxMazeLayoutWidth);

            int maxCountOfObstacles = mazeHeight * mazeWidth / 100 * _appSettings.maxPercentOfObstaclesInLayout;
            int obstacleCount = ReadInputAsInteger("Define the amount of obstacles:", maxCountOfObstacles);

            Console.ForegroundColor = ConsoleColor.White;



            goto LabelMethodBeginning;
        }

        private static int ReadInputAsInteger(string inputPrompt, int maxInput)
        {
        LabelReadInput:
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"         {inputPrompt} ");

            Console.ForegroundColor = ConsoleColor.Cyan;
            string userInput = Console.ReadLine() ?? string.Empty;

            if (int.TryParse(userInput, out int output) == false)
            {
                Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 1);
                Console.WriteLine(new string(' ', Console.BufferWidth));
                Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 1);

                goto LabelReadInput;
            }

            if (output > maxInput || output < 5)
            {
                Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 1);
                Console.WriteLine(new string(' ', Console.BufferWidth));
                Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 1);

                goto LabelReadInput;
            }

            return output;
        }
    }
}