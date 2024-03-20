using Plarium.Assets.GameCore.Events;

namespace Plarium.Assets.PlayerInput
{
    public class InputProcessing
    {
        //create command syntax: "create <"triangle"/"square"/"circle"> <object id>"
        private const string createCommandRegex = @"\bcreate\b\s+(?:triangle|square|circle)\s+\S+";
        //scale command syntax: "scale <object id> <positive int/float>"
        private const string scaleCommandRegex = @"\bscale\b\s+\S+\s+(\d+(\.\d+)?)(?=\s|$)";
        //move command syntax: "move <object id> <float/vector 2(x, y)>"
        private const string moveCommandRegex = @"\bmove\b\s+\S+\s+([-+]?\d+(\.\d+)?)\b(,\s*([-+]?\d+(\.\d+)?))(?=\s|$)";
        //destroy command syntax: "destroy <object id>"
        private const string destroyCommandRegex = @"\bdestroy\b\s+\S+";
        //change command color syntax: "change color <object id> <"white"/"black"/"red"/"green"/"blue">
        private const string changecolorCommandRegex = @"\bchange\b\s+color\s+\S+\s+(white|black|red|green|blue)\b";


        public void NewPlayerInput(BaseEventParams eventParams)
        {
            var newInput = (PlayerInputEventParams)eventParams;
            StringValidation(newInput.PlayerInput);
        }

        private void StringValidation(string input)
        {

        }
    }
}
