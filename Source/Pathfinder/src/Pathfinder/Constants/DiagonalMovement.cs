
namespace Pathfinder.Constants
{
    public enum DiagonalMovement : byte
    {
        Never = 0,
        OnlyWhenNoObstacles = 1,
        IfAtMostOneObstacle = 2,
        Always = 3
        
    }
}
