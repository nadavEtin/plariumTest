using System.Collections.Generic;

namespace Plarium.Assets.GameCore.ScriptableObjects
{
    public interface IKeywords
    {
        List<string> Colors { get; }
        List<string> Commands { get; }
        List<string> Shapes { get; }
    }
}