namespace Mazescape
{
    internal class ApplicationSettings
    {
        internal readonly int maxMazeLayoutHeight = 40;
        internal readonly int maxMazeLayoutWidth = 60;
        internal readonly int maxPercentOfObstaclesInLayout = 80;

        internal readonly char layoutObstacle = '□';
        internal readonly char layoutCorrectPath = '▓';
        internal readonly char layoutFailedPath = '░';
        internal readonly char layoutEscapee = '•';
        internal readonly char layoutDestination = 'X';
        internal readonly char layoutEmpty = ' ';

        internal readonly int pathFindingRefreshRateInMilliseconds = 10;
    }
}