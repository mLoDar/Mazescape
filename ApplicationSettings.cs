namespace Mazescape
{
    internal class ApplicationSettings
    {
        internal readonly int maxMazeLayoutHeight = 40;
        internal readonly int maxMazeLayoutWidth = 60;
        internal readonly int maxPercentOfObstaclesInLayout = 80;

        internal readonly char layoutObstacleCharacter = '█';
        internal readonly char layoutCorrectPath = '▓';
        internal readonly char layoutFailedPath = '░';
        internal readonly char layoutStartPoint = 'o';
        internal readonly char layoutDestination = 'x';
    }
}