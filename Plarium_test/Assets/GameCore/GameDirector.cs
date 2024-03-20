using Plarium.Assets.GameCore.Events;
using VContainer.Unity;

namespace GameCore
{
    public class GameDirector : IStartable, ITickable
    {
        private readonly EventBus _eventBus;
        
        public GameDirector(EventBus bus)
        {
            _eventBus = bus;
        }
        
        public void Start()
        {
            
        
        }

        public void Tick()
        {
            
        }
    }
}