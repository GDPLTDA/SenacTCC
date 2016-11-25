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
        Standard,
    }

    public enum ViewerEnum
    {
        Console,
        OpenGL
    }

    public enum AppModeEnum
    {
        SingleRun,
        Dynamic,
        BatchMode
    }
}
