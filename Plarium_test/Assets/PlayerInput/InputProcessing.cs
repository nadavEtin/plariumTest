using Plarium.Assets.GameCore.Events;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Windows;
using VContainer;

namespace Plarium.Assets.PlayerInput
{
    public enum CommandTypes
    {
        Create,
        Destroy,
        Move,
        Scale,
        ChaneColor
    }

    public class InputProcessing : IInputProcessing
    {
        //create command syntax: "create (triangle/square/circle) <object id>"
        private const string createCommandRegex = @"\bcreate\b\s+(?:triangle|square|circle)\s+\S+";
        //scale command syntax: "scale <object id> <positive int/float>"
        private const string scaleCommandRegex = @"\bscale\b\s+\S+\s+(\d+(\.\d+)?)(?=\s|$)";
        //move command syntax: "move <object id> <float/vector 2(x, y)>"
        private const string moveCommandRegex = @"\bmove\b\s+\S+\s+([-+]?\d+(\.\d+)?)\b(,\s*([-+]?\d+(\.\d+)?))(?=\s|$)";
        //destroy command syntax: "destroy <object id>"
        private const string destroyCommandRegex = @"\bdestroy\b\s+\S+";
        //change command color syntax: "change color <object id> <"white"/"black"/"red"/"green"/"blue">
        private const string changecolorCommandRegex = @"\bchange\b\s+\S+\s+color\s+(white|black|red|green|blue)\b";

        private EventBus _eventBus;
        private Dictionary<CommandTypes, string> _commandsRegex;

        [Inject]
        public InputProcessing(EventBus eventBus)
        {
            _eventBus = eventBus;
            _eventBus.Subscribe(GameplayEvent.NewPlayerInput, NewPlayerInput);
            _commandsRegex = new Dictionary<CommandTypes, string>
            {
                { CommandTypes.Create, createCommandRegex },
                { CommandTypes.Destroy, destroyCommandRegex },
                { CommandTypes.Move, moveCommandRegex },
                { CommandTypes.Scale, scaleCommandRegex },
                { CommandTypes.ChaneColor, changecolorCommandRegex }
            };
        }

        /*public void NewPlayerInput(string input)
        {
            if(GeneralInputValidation(input))
            {

            }
        }*/

        public void NewPlayerInput(BaseEventParams eventParams)
        {
            var newInput = (PlayerInputEventParams)eventParams;

            //general input validation
            if (GeneralInputValidation(newInput.PlayerInput) == false)
                return;

            StringValidation(newInput.PlayerInput);
        }

        private void StringValidation(string input)
        {
            // = new List<string>();
            foreach (var regex in _commandsRegex)
            {
                if (SearchForMatches(input, regex.Value, out List<string> matchingStrings))
                {

                }
            }
            

        }

        private bool SearchForMatches(string input, string regexPattern, out List<string> matchingStrings)
        {
            matchingStrings = Regex.Matches(input, regexPattern, RegexOptions.IgnoreCase)
                .OfType<Match>()
                .Select(m => m.Groups[0].Value)
                .ToList();

            return matchingStrings.Count > 0;
        }

        private bool GeneralInputValidation(string text)
        {
            var len = text.Length;
            if (len < 4 || len > 150)
            {
                Debug.Log("The input must be between 5 and 150 characters");
                return false;
            }

            return true;
        }
    }
}
