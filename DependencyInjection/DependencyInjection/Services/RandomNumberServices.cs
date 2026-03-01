using DependencyInjection.Controllers;

namespace DependencyInjection.Services
{
    public class RandomNumberServices : IRandom
    {
        private readonly int _randomNumber;
        public int GetNumber()
        {
            return _randomNumber;
        }
        public RandomNumberServices()
        {
            _randomNumber = Random.Shared.Next(1, 101);
        }
    }
}
