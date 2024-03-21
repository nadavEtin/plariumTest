using Plarium.Assets.PlayerInput;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Plarium.Assets.Shapes
{
    public class ShapesManager : MonoBehaviour, IShapesManager
    {
        private Dictionary<CommandTypes, Action<List<string>>> _commandHandlers;
        private Dictionary<string, GameObject> _activeShapes;

        private void Start()
        {
            _activeShapes = new Dictionary<string, GameObject>();
            _commandHandlers = new Dictionary<CommandTypes, Action<List<string>>>
            {
                { CommandTypes.Create, CreateNewShapeObj },
                { CommandTypes.Destroy, DestroyShapeObj },
                { CommandTypes.Move, MoveShapeObj },
                { CommandTypes.Scale, ScaleShapeObj },
                { CommandTypes.ChaneColor, ChangeColorOfShapeObj }
            };
        }

        public void NewCommand(CommandTypes type, List<string> input)
        {
            _commandHandlers[type](input);
        }

        private void CreateNewShapeObj(List<string> input)
        {
            //each input is a list of 3 words: create *shape* *id*
            switch (input[1])
            {

                default:
                    break;
            }
        }

        private void DestroyShapeObj(List<string> input)
        {

        }

        private void MoveShapeObj(List<string> input)
        {

        }

        private void ScaleShapeObj(List<string> input)
        {

        }

        private void ChangeColorOfShapeObj(List<string> input)
        {

        }
    }
}
