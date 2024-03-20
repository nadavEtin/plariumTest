using System.Collections.Generic;
using UnityEngine;

namespace Plarium.Assets.GameCore.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Keywords", menuName = "Scriptable Objects/Keywords")]
    public class Keywords : ScriptableObject, IKeywords
    {
        [SerializeField] private List<string> _commands;
        [SerializeField] private List<string> _shapes;
        [SerializeField] private List<string> _colors;

        public List<string> Commands { get => _commands; }
        public List<string> Shapes { get => _shapes; }
        public List<string> Colors { get => _colors; }
    }
}
