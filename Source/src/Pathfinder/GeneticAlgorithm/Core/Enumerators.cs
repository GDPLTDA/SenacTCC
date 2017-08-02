namespace Pathfinder
{
    public enum CrossoverEnum
    {
        Simple,
        OBX,
        PBX
    }
    public enum MutateEnum
    {
        EM,
        DIVM,
        DM,
        IM,
        IVM,
        SM,
        Bitwise
    }
    public enum SelectionEnum
    {
        Random,
        RouletteWheel
    }
    public enum FitnessEnum
    {
        Heuristic,
        CollisionDetection,
        CirclicValidation,
        CollisionDetectionAndCirclicValidation
    }
}