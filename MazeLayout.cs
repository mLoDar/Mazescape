using System.Drawing;





namespace Mazescape
{
    internal class MazeLayout
    {
        private static readonly ApplicationSettings _appSettings = new();

        private static char[,] _mazeLayout = new char[0, 0];
        private static Point startLocation = new(0, 0);
        private static Point endLocation = new(0, 0);

        private static readonly Random numberGenerator = new();

        

        public MazeLayout(int mazeHeight, int mazeWidth)
        {
            _mazeLayout = new char[mazeHeight, mazeWidth];
        }

        internal static void Initalize()
        {
            for (int row = 0; row < _mazeLayout.GetLength(0); row++)
            {
                for (int column = 0; column < _mazeLayout.GetLength(1); column++)
                {
                    _mazeLayout[row, column] = ' ';
                }
            }

            startLocation = new(0, 0);
            endLocation = new(0, 0);
        }

        internal static void PlaceObstacles(int countOfObstacles)
        {
            int mazeWidth = _mazeLayout.GetLength(1);
            int mazeHeigth = _mazeLayout.GetLength(0);

            for (int i = 0; i < countOfObstacles; i++)
            {
            LabelCoordinatesGeneration:
                int xCoordinate = numberGenerator.Next(0, mazeWidth);
                int yCoordinate = numberGenerator.Next(0, mazeHeigth);

                if (_mazeLayout[yCoordinate, xCoordinate].Equals(_appSettings.layoutObstacleCharacter))
                {
                    goto LabelCoordinatesGeneration;
                }

                _mazeLayout[yCoordinate, xCoordinate] = _appSettings.layoutObstacleCharacter;
            }
        }

        internal static void PlaceStartAndEndLocation()
        {
        LabelGeneratingStartLocation:

            int xCoordinate = numberGenerator.Next(0, _mazeLayout.GetLength(1));
            int yCoordinate = numberGenerator.Next(0, _mazeLayout.GetLength(0));

            if (_mazeLayout[yCoordinate, xCoordinate].Equals(_appSettings.layoutObstacleCharacter))
            {
                goto LabelGeneratingStartLocation;
            }

            _mazeLayout[yCoordinate, xCoordinate] = _appSettings.layoutStartPoint;
            startLocation = new Point(xCoordinate, yCoordinate);



        LabelGeneratingEndLocation:

            xCoordinate = numberGenerator.Next(0, _mazeLayout.GetLength(1));
            yCoordinate = numberGenerator.Next(0, _mazeLayout.GetLength(0));

            if (_mazeLayout[yCoordinate, xCoordinate].Equals(_appSettings.layoutObstacleCharacter))
            {
                goto LabelGeneratingEndLocation;
            }

            _mazeLayout[yCoordinate, xCoordinate] = _appSettings.layoutDestination;
            endLocation = new Point(xCoordinate, yCoordinate);
        }

        internal static void Display()
        {
            int mazeWidth = _mazeLayout.GetLength(1);
            int mazeHeigth = _mazeLayout.GetLength(0);

            

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
                    char currentChar = _mazeLayout[row, column];

                    Console.ForegroundColor = ConsoleColor.White;

                    if (currentChar.Equals(_appSettings.layoutCorrectPath))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }

                    if (currentChar.Equals(_appSettings.layoutFailedPath))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }

                    if (currentChar.Equals(_appSettings.layoutStartPoint) || currentChar.Equals(_appSettings.layoutDestination))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }

                    Console.Write(_mazeLayout[row, column]);
                }

                Console.WriteLine("│");
            }

            Console.WriteLine($"         └{new string('─', mazeWidth)}┘");
        }
    }
}