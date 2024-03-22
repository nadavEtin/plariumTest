using Plarium.Assets.GameCore.Events;
using TMPro;
using UnityEngine;
using VContainer;

namespace Plarium.GameCore.UI
{
    [RequireComponent(typeof(Canvas))]
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField;

        private EventBus _eventBus;
        
        [Inject]
        private void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                if(_inputField.text != string.Empty)
                {
                    _eventBus.Publish(GameplayEvent.NewPlayerInput, new PlayerInputEventParams(_inputField.text));
                    //clear the input field
                    _inputField.text = "";
                }                
            }
        }
    }
}
