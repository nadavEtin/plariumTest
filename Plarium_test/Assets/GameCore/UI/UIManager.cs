using Plarium.Assets.GameCore.ScriptableObjects;
using TMPro;
using UnityEngine;
using VContainer;

namespace Plarium.GameCore.UI
{
    [RequireComponent(typeof(Canvas))]
    public class UIManager : MonoBehaviour, IUIManager
    {
        [SerializeField] private TMP_InputField _inputField;

        //private IAssetRefs _assetRefs;
        private IKeywords _keywords;
        
        [Inject]
        private void Construct(IKeywords keywords)
        {
            _keywords = keywords;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                ProcessInput(_inputField.text);
                //clear the input field
                _inputField.text = "";
            }
        }

        private void ProcessInput(string userInput)
        {
            

        }
    }
}
