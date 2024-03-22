using UnityEngine;

namespace Plarium.Assets.GameCore.ScriptableObjects
{
    public interface IAssetRefs
    {
        GameObject CirclePrefab { get; }
        GameObject TrianglePrefab { get; }
        GameObject SquarePrefab { get; }
    }
}