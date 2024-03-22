using Plarium.Assets.GameCore.Events;
using Plarium.Assets.GameCore.Utility;
using Plarium.Assets.PlayerInput;
using Plarium.Assets.Shapes;
using System;
using System.Threading;
using VContainer.Unity;

namespace Plarium.Assets.GameCore
{
    public class GameDirector : IStartable, IDisposable
    {
        private EventBus _eventBus;
        private InputProcessing _inputProcessing;

        public GameDirector(EventBus bus, IShapesManager shapesManager)
        {
            _eventBus = bus;
            _inputProcessing = new InputProcessing(_eventBus, shapesManager);
        }

        public void Start()
        {
            _eventBus.Subscribe(GameplayEvent.NewPlayerInput, NewPlayerInput);
        }

        private void NewPlayerInput(BaseEventParams eventParams)
        {
            var playerInput = (PlayerInputEventParams)eventParams;
            WriteInputToLog(playerInput.PlayerInput);
        }

        private void WriteInputToLog(string input)
        {
            Thread thread = new Thread(ReadWriteHelper.WriteToFile);
            thread.Start(input);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe(GameplayEvent.NewPlayerInput, NewPlayerInput);
        }
    }
}