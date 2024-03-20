using Plarium.Assets.GameCore.ScriptableObjects;
using TMPro;
using UnityEngine;
using VContainer;

namespace Plarium.GameCore.UI
{
    public class InfoBtn : MonoBehaviour
    {
        [SerializeField] private GameObject _textBackground;
        [SerializeField] private TMP_Text _commandsText, _colorsText, _shapesText;

        private IKeywords _keywordsRef;
        private bool _infoVisible = false;

        [Inject]
        private void Contruct(IKeywords keywords)
        {
            _keywordsRef = keywords;
            _textBackground.SetActive(false);
        }

        public void OnBtnClick()
        {
            if(_infoVisible)
            {
                _textBackground.SetActive(false);
            }
            else
            {
                _textBackground.SetActive(true);
                _commandsText.text = $"Commands: \n{string.Join(", ", _keywordsRef.Commands)}";
                _shapesText.text = $"Shapes: \n{string.Join(", ", _keywordsRef.Shapes)}";
                _colorsText.text = $"Colors: \n{string.Join(", ", _keywordsRef.Colors)}";
            }

            _infoVisible = !_infoVisible;
        }
    }
}
