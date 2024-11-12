using System.Drawing;





namespace Mazescape
{
    internal class Maze
    {
        private static readonly ApplicationSettings _appSettings = new();

        internal char[,] mazeLayout = new char[0, 0];
        internal Point startLocation = new(0, 0);
        internal Point endLocation = new(0, 0);

        internal bool pathfindingWasSuccessful = false;

        private static readonly Random numberGenerator = new();

        

        public Maze(int mazeHeight, int mazeWidth)
        {
            mazeLayout = new char[mazeHeight, mazeWidth];
        }

        internal void Initalize()
        {
            for (int row = 0; row < mazeLayout.GetLength(0); row++)
            {
                for (int column = 0; column < mazeLayout.GetLength(1); column++)
                {
                    mazeLayout[row, column] = ' ';
                }
            }

            startLocation = new(0, 0);
            endLocation = new(0, 0);
        }

        internal void PlaceObstacles(int countOfObstacles)
        {
            int mazeWidth = mazeLayout.GetLength(1);
            int mazeHeigth = mazeLayout.GetLength(0);

            for (int i = 0; i < countOfObstacles; i++)
            {
            LabelCoordinatesGeneration:
                int xCoordinate = numberGenerator.Next(0, mazeWidth);
                int yCoordinate = numberGenerator.Next(0, mazeHeigth);

                if (mazeLayout[yCoordinate, xCoordinate].Equals(_appSettings.layoutObstacle))
                {
                    goto LabelCoordinatesGeneration;
                }

                mazeLayout[yCoordinate, xCoordinate] = _appSettings.layoutObstacle;
            }
        }

        internal void PlaceStartAndEndLocation()
        {
        LabelGeneratingStartLocation:

            int xCoordinate = numberGenerator.Next(0, mazeLayout.GetLength(1));
            int yCoordinate = numberGenerator.Next(0, mazeLayout.GetLength(0));

            if (mazeLayout[yCoordinate, xCoordinate].Equals(_appSettings.layoutObstacle))
            {
                goto LabelGeneratingStartLocation;
            }

            mazeLayout[yCoordinate, xCoordinate] = _appSettings.layoutEscapee;
            startLocation = new Point(xCoordinate, yCoordinate);



        LabelGeneratingEndLocation:

            xCoordinate = numberGenerator.Next(0, mazeLayout.GetLength(1));
            yCoordinate = numberGenerator.Next(0, mazeLayout.GetLength(0));

            if (mazeLayout[yCoordinate, xCoordinate].Equals(_appSettings.layoutObstacle))
            {
                goto LabelGeneratingEndLocation;
            }

            mazeLayout[yCoordinate, xCoordinate] = _appSettings.layoutDestination;
            endLocation = new Point(xCoordinate, yCoordinate);
        }

        internal void Display()
        {
            int mazeWidth = mazeLayout.GetLength(1);
            int mazeHeigth = mazeLayout.GetLength(0);

            

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("          ┳┳┓      ┓           ");
            Console.WriteLine("          ┃┃┃┏┓┓┏┓ ┃ ┏┓┓┏┏┓┓┏╋•");
            Console.WriteLine("          ┛ ┗┗┻┗┗  ┗┛┗┻┗┫┗┛┗┻┗•");
            Console.WriteLine("                        ┛      ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("        ────────────────────────────");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine($"         ┌{new string('─', mazeWidth)}┐");

            for (int row = 0; row < mazeHeigth; row++)
            {
                Console.Write("         │");

                for (int column = 0; column < mazeWidth; column++)
                {
                    char currentChar = mazeLayout[row, column];

                    Console.ForegroundColor = ConsoleColor.White;

                    if (currentChar.Equals(_appSettings.layoutCorrectPath))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }

                    if (currentChar.Equals(_appSettings.layoutFailedPath))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }

                    if (currentChar.Equals(_appSettings.layoutEscapee) || currentChar.Equals(_appSettings.layoutDestination))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }

                    Console.Write(mazeLayout[row, column]);

                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.WriteLine("│");
            }

            Console.WriteLine($"         └{new string('─', mazeWidth)}┘");
        }
    }
}