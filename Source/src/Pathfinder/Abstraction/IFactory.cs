

namespace Pathfinder.Abstraction
{
    public interface IFactory<T>
    {
         T GetImplementation();
         T GetImplementation(int option );
    }
    public interface IFactory : IFactory<object>
    {
    }
}