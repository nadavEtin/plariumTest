using Plarium.Assets.GameCore.Events;
using Plarium.Assets.Shapes;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using VContainer;

namespace Plarium.Assets.PlayerInput
{
    public enum CommandTypes
    {
        Create,
        Destroy,
        Move,
        Scale,
        ChaneColor,
        Rotate
    }

    public class InputProcessing
    {
        //create command syntax: "create (triangle/square/circle) <object id>"
        private const string createCommandRegex = @"(?<!\S)\bcreate\b\s+(?:triangle|square|circle)\s+\S+";
        //scale command syntax: "scale <object id> <positive int/float>"
        private const string scaleCommandRegex = @"(?<!\S)\bscale\b\s+\S+\s+(\d+(\.\d+)?)(?=\s|$)";
        //move command syntax: "move <object id> <float/vector 2(x, y)>"
        private const string moveCommandRegex = @"(?<!\S)\bmove\b\s+\S+\s+([-+]?\d+(\.\d+)?)\b(,\s*([-+]?\d+(\.\d+)?))?(?=\s|$)";
        //rotate command syntax: "rotate <object id> <float/vector 3(x, y, z)>"
        private const string rotateCommandRegex = @"(?<!\S)\brotate\b\s+\S+\s+([-+]?\d+(\.\d+)?)(,\s*([-+]?\d+(\.\d+)?)){0,2}(?=\s|$)";
        //destroy command syntax: "destroy <object id>"
        private const string destroyCommandRegex = @"(?<!\S)\bdestroy\b\s+\S+";
        //change command color syntax: "change <object id> color <"white"/"black"/"red"/"green"/"blue">
        private const string changecolorCommandRegex = @"(?<!\S)\bchange\b\s+\S+\s+color\s+(white|black|red|green|blue)\b";

        private EventBus _eventBus;
        private Dictionary<CommandTypes, string> _commandsRegex;
        private IShapesManager _shapesManager;

        [Inject]
        public InputProcessing(EventBus eventBus, IShapesManager shapesManager)
        {
            _eventBus = eventBus;
            _shapesManager = shapesManager;
            _eventBus.Subscribe(GameplayEvent.NewPlayerInput, NewPlayerInput);
            _commandsRegex = new Dictionary<CommandTypes, string>
            {
                { CommandTypes.Create, createCommandRegex },
                { CommandTypes.Destroy, destroyCommandRegex },
                { CommandTypes.Move, moveCommandRegex },
                { CommandTypes.Scale, scaleCommandRegex },
                { CommandTypes.ChaneColor, changecolorCommandRegex },
                { CommandTypes.Rotate, rotateCommandRegex }
            };
        }

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
            bool validCommandFound = false;
            foreach (var commandTypePattern in _commandsRegex)
            {
                if (SearchForMatches(input, commandTypePattern.Value, out List<string> matchingStrings))
                {
                    validCommandFound = true;
                    //pass a list of all commands of the same type
                    _shapesManager.NewCommand(commandTypePattern.Key, matchingStrings);
                }
            }

            if (validCommandFound == false)
                Debug.Log("No valid command input found");
        }

        private bool SearchForMatches(string input, string regexPattern, out List<string> matchingStrings)
        {
            //use the regex to find valid commands
            matchingStrings = Regex.Matches(input, regexPattern, RegexOptions.IgnoreCase)
                .OfType<Match>()
                .Select(m => m.Groups[0].Value)
                .ToList();

            return matchingStrings.Count > 0;
        }

        private bool GeneralInputValidation(string text)
        {
            //limit string length to prevent errors and excessive slowdown
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
