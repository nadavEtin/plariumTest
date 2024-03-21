using Plarium.Assets.GameCore.Events;
using Plarium.Assets.GameCore.Utility;
using Plarium.Assets.PlayerInput;
using System;
using System.Threading;
using UnityEngine;
using VContainer.Unity;

namespace Plarium.Assets.GameCore
{
    public class GameDirector : IStartable, IDisposable
    {
        private EventBus _eventBus;
        private InputProcessing _inputProcessing;
        //private CancellationTokenSource _cancellationTokenSource;

        public GameDirector(EventBus bus/*, InputProcessing inputProcessing*/)
        {
            _eventBus = bus;
            _inputProcessing = new InputProcessing(_eventBus);
            //_cancellationTokenSource = new CancellationTokenSource();
            //_inputProcessing = inputProcessing;
        }

        public void Start()
        {
            _eventBus.Subscribe(GameplayEvent.NewPlayerInput, NewPlayerInput);
            //_eventBus.Subscribe(GameplayEvent.ApplicationQuit, ApplicationQuit);
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

        /*private void ApplicationQuit(BaseEventParams eventParams)
        {
            _cancellationTokenSource.Cancel();
        }*/

        public void Dispose()
        {
            _eventBus.Unsubscribe(GameplayEvent.NewPlayerInput, NewPlayerInput);
            //_eventBus.Unsubscribe(GameplayEvent.ApplicationQuit, ApplicationQuit);
        }
    }
}