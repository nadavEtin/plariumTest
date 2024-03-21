using Plarium.Assets.GameCore.Events;
using Plarium.Assets.GameCore.ScriptableObjects;
using Plarium.Assets.PlayerInput;
using TMPro;
using UnityEngine;
using VContainer;

namespace Plarium.GameCore.UI
{
    [RequireComponent(typeof(Canvas))]
    public class UIManager : MonoBehaviour, IUIManager
    {
        [SerializeField] private TMP_InputField _inputField;

        private EventBus _eventBus;
        private IKeywords _keywords;
        //private IInputProcessing _inputprocessing;
        
        [Inject]
        private void Construct(IKeywords keywords, IInputProcessing inputProcessing, EventBus eventBus)
        {
            _keywords = keywords;
            _eventBus = eventBus;
            //_inputprocessing = inputProcessing;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                if(_inputField.text != string.Empty)
                {
                    //TODO: delete
                    ProcessInput(_inputField.text);


                    //_inputprocessing.NewPlayerInput(_inputField.text);
                    //clear the input field
                    _inputField.text = "";
                }                
            }
        }

        private void ProcessInput(string userInput)
        {
            _eventBus.Publish(GameplayEvent.NewPlayerInput, new PlayerInputEventParams(userInput));
        }
    }
}
