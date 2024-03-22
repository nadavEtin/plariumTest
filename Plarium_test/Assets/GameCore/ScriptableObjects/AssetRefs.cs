using UnityEngine;

namespace Plarium.Assets.GameCore.ScriptableObjects
{
    [CreateAssetMenu(fileName = "AssetRefs", menuName = "Scriptable Objects/Asset References")]
    public class AssetRefs : ScriptableObject, IAssetRefs
    {
        [SerializeField] private GameObject _circle, _triangle, _square;

        public GameObject CirclePrefab => _circle;
        public GameObject TrianglePrefab => _triangle;
        public GameObject SquarePrefab => _square;
    }
}