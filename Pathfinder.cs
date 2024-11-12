using System.Drawing;





namespace Mazescape
{
    internal class Pathfinder
    {
        private static Maze _currentMaze;

        private static readonly ApplicationSettings _appSettings = new();



        internal Maze Search(Maze currentMaze)
        {
            _currentMaze = currentMaze;

            int startX = _currentMaze.startLocation.X;
            int startY = _currentMaze.startLocation.Y;

            bool aPathWasFound = false;

            if (startY + 1 < _currentMaze.mazeLayout.GetLength(1) && ArrayHasChar(_currentMaze.mazeLayout, startX, startY + 1, _appSettings.layoutObstacle))
            {
                aPathWasFound = MakeMove(startX, startY + 1, startX, startY);
            }
            if (startX + 1 < _currentMaze.mazeLayout.GetLength(0) && ArrayHasChar(_currentMaze.mazeLayout, startX + 1, startY, _appSettings.layoutObstacle) && !aPathWasFound)
            {
                aPathWasFound = MakeMove(startX + 1, startY, startX, startY);
            }
            if (startY - 1 >= 0 && !ArrayHasChar(_currentMaze.mazeLayout, startX, startY - 1, _appSettings.layoutObstacle) && !aPathWasFound)
            {
                aPathWasFound = MakeMove(startX, startY - 1, startX, startY);
            }
            if (startX - 1 >= 0 && !ArrayHasChar(_currentMaze.mazeLayout, startX - 1, startY, _appSettings.layoutObstacle) && !aPathWasFound)
            {
                aPathWasFound = MakeMove(startX - 1, startY, startX, startY);
            }

            _currentMaze.pathfindingWasSuccessful = aPathWasFound;

            return _currentMaze;
        }

        private static bool MakeMove(int currentX, int currentY, int oldX, int oldY)
        {
            Thread.Sleep(_appSettings.pathFindingRefreshRateInMilliseconds);

            if (new Point(currentX, currentY).Equals(_currentMaze.endLocation))
            {
                _currentMaze.mazeLayout[oldY, oldX] = _appSettings.layoutCorrectPath;

                Console.SetCursorPosition(0, 4);
                _currentMaze.Display();

                return true;
            }
            else if (_currentMaze.mazeLayout[currentY, currentX] == _appSettings.layoutEmpty)
            {
                _currentMaze.mazeLayout[currentY, currentX] = _appSettings.layoutCorrectPath;

                Console.SetCursorPosition(0, 4);
                _currentMaze.Display();

                bool canMoveUp = currentY + 1 < _currentMaze.mazeLayout.GetLength(0) && !ArrayHasChar(_currentMaze.mazeLayout, currentX, currentY + 1, _appSettings.layoutObstacle) && oldY != currentY + 1;
                bool canMoveRight = currentX + 1 < _currentMaze.mazeLayout.GetLength(1) && !ArrayHasChar(_currentMaze.mazeLayout, currentX + 1, currentY, _appSettings.layoutObstacle) && oldX != currentX + 1;
                bool canMoveDown = currentY - 1 >= 0 && !ArrayHasChar(_currentMaze.mazeLayout, currentX, currentY - 1, _appSettings.layoutObstacle) && oldY != currentY - 1;
                bool canMoveLeft = currentX - 1 >= 0 && !ArrayHasChar(_currentMaze.mazeLayout, currentX - 1, currentY, _appSettings.layoutObstacle) && oldX != currentX - 1;

                if (canMoveUp && MakeMove(currentX, currentY + 1, currentX, currentY))
                {
                    return true;
                }
                if (canMoveRight && MakeMove(currentX + 1, currentY, currentX, currentY))
                {
                    return true;
                }
                if (canMoveDown && MakeMove(currentX, currentY - 1, currentX, currentY))
                {
                    return true;
                }
                if (canMoveLeft && MakeMove(currentX - 1, currentY, currentX, currentY))
                {
                    return true;
                }

                _currentMaze.mazeLayout[currentY, currentX] = _appSettings.layoutFailedPath;
            }

            return false;
        }

        private static bool ArrayHasChar(char[,] inputArray, int xCoordinate, int yCoordinate, char charaterToLookFor)
        {
            if (inputArray[yCoordinate, xCoordinate].Equals(charaterToLookFor))
            {
                return true;
            }

            return false;
        }
    }
}