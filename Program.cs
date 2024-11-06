﻿using System.Text;





namespace Mazescape
{
    internal class Program
    {
        private static readonly ApplicationSettings _appSettings = new();

        private static char[,] _mazeLayout = new char[0, 0];



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

            int maxCountOfObstacles = mazeHeight * mazeWidth * _appSettings.maxPercentOfObstaclesInLayout / 100;
            int obstacleCount = ReadInputAsInteger("Define the amount of obstacles:", maxCountOfObstacles);
            


            _mazeLayout = InitializeMazeLayout(mazeHeight, mazeWidth);
            _mazeLayout = PlaceObstaclesInLayout(_mazeLayout, obstacleCount);



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

        private static char[,] InitializeMazeLayout(int layoutHeight, int layoutWidth)
        {
            char[,] initializedLayout = new char[layoutHeight, layoutWidth];

            for (int row = 0; row < initializedLayout.GetLength(0); row++)
            {
                for (int column = 0; column < initializedLayout.GetLength(1); column++)
                {
                    initializedLayout[row, column] = ' ';
                }
            }

            return initializedLayout;
        }

        private static char[,] PlaceObstaclesInLayout(char[,] mazeLayout, int countOfObstacles)
        {
            Random numberGenerator = new();

            int mazeLayoutWidth = mazeLayout.GetLength(1);
            int mazeLayoutHeight = mazeLayout.GetLength(0);
            
            for (int i = 0; i < countOfObstacles; i++)
            {
            LabelCoordinatesGeneration:
                int xCoordinate = numberGenerator.Next(0, mazeLayoutWidth);
                int yCoordinate = numberGenerator.Next(0, mazeLayoutHeight);

                if (mazeLayout[yCoordinate, xCoordinate].Equals(_appSettings.layoutObstacleCharacter))
                {
                    goto LabelCoordinatesGeneration;
                }

                mazeLayout[yCoordinate, xCoordinate] = _appSettings.layoutObstacleCharacter;
            }

            return mazeLayout;
        }
    }
}