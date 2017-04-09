namespace Pathfinder
{
    public enum HeuristicEnum
    {
        Manhattan,
        Euclidean,
        Octile,
        Chebyshev
    }
    public enum FinderEnum
    {
        AStar,
        BestFirstSearch,
        IDAStar,
        Dijkstra,
        GA,
    }
    public enum MapGeneratorEnum
    {
        File,
        Static,
        Random,
        WithPattern,
    }
    public enum DirectionMovement
    {
        None,
        Up,
        Down,
        Left,
        Right,
        UpRight,
        UpLeft,
        DownRight,
        DownLeft
    }
    public enum DiagonalMovement : byte
    {
        Never = 0,
        OnlyWhenNoObstacles = 1,
        IfAtMostOneObstacle = 2,
        Always = 3
    }
}