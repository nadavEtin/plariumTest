using Plarium.Assets.PlayerInput;
using System.Collections.Generic;

namespace Plarium.Assets.Shapes
{
    public interface IShapesManager
    {
        void NewCommand(CommandTypes type, List<string> inputs);
    }
}