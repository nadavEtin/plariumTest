using Plarium.Assets.GameCore.ScriptableObjects;
using Plarium.Assets.PlayerInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using VContainer;

namespace Plarium.Assets.Shapes
{
    public class ShapesManager : MonoBehaviour, IShapesManager
    {
        private Dictionary<CommandTypes, Action<List<string>>> _commandHandlers;
        private Dictionary<string, GameObject> _activeShapes;

        private IAssetRefs _assetRefs;
        private float _startingXPos = -5;
        public void NewCommand(CommandTypes type, List<string> input)
        {
            _commandHandlers[type](input);
        }

        [Inject]
        private void Construct(IAssetRefs assetRefs)
        {
            _assetRefs = assetRefs;
            _activeShapes = new Dictionary<string, GameObject>();
            _commandHandlers = new Dictionary<CommandTypes, Action<List<string>>>
            {
                { CommandTypes.Create, CreateNewShapeObj },
                { CommandTypes.Destroy, DestroyShapeObj },
                { CommandTypes.Move, MoveShapeObj },
                { CommandTypes.Scale, ScaleShapeObj },
                { CommandTypes.ChaneColor, ChangeColorOfShapeObj },
                { CommandTypes.Rotate, RotateShapeObj }
            };
        }

        private void CreateNewShapeObj(List<string> input)
        {
            for (int i = 0; i < input.Count; i++)
            {
                //separate each word in the input
                //each input is a list of 3 words: create *shape* *id*
                var words = input[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                GameObject shapeObj = null;

                //check for existing objects with this id
                if (_activeShapes.ContainsKey(words[2]))
                {
                    Debug.Log($"An object with this id: {words[2]} already exists");
                    return;
                }

                switch (words[1].ToLower())
                {
                    case "square":
                        shapeObj = Instantiate(_assetRefs.SquarePrefab, transform);
                        break;

                    case "triangle":
                        shapeObj = Instantiate(_assetRefs.TrianglePrefab, transform);
                        break;

                    case "circle":
                        shapeObj = Instantiate(_assetRefs.CirclePrefab, transform);
                        break;

                    default:
                        Debug.Log($"Error! unrecognized shape: {words[1]}");
                        break;
                }

                //save the gameObject and it's id
                if (shapeObj != null)
                {
                    _activeShapes.Add(words[2], shapeObj);
                    //display the id on the obj
                    if (shapeObj.TryGetComponent(out IShapeObject shapeScript))
                        shapeScript.Setup(words[2]);
                    PositionShapeObj(shapeObj);
                }
            }
        }

        private void PositionShapeObj(GameObject shape)
        {
            shape.transform.position = new Vector2(_startingXPos, 0);
            _startingXPos += 0.5f;
            _startingXPos = Mathf.Clamp(_startingXPos, -5, 5);
        }

        private void DestroyShapeObj(List<string> input)
        {
            for (int i = 0; i < input.Count; i++)
            {
                //separate each word in the input
                //each input is a list of 2 words: destroy *id*
                var words = input[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (_activeShapes.ContainsKey(words[1]))
                {
                    var objToDestroy = _activeShapes[words[1]];
                    _activeShapes.Remove(words[1]);
                    Destroy(objToDestroy);
                }
            }
        }

        private void MoveShapeObj(List<string> input)
        {
            for (int i = 0; i < input.Count; i++)
            {
                //each input is a list of 3 words: move *id* *float/vector2*
                var words = SplitInputIntoWords(input[i]);
                if (_activeShapes.ContainsKey(words[1]))    //find shape by its id
                {
                    //attempt to parse float value from the string command
                    var parsedFloats = ParseCommandInput(words, out bool success);
                    if (success)
                    {
                        var objTransform = _activeShapes[words[1]].transform;
                        objTransform.position = new Vector2(objTransform.position.x + parsedFloats[0], 
                            parsedFloats.Count == 2 ? objTransform.position.y + parsedFloats[1] : 0);
                    }                        
                }                
                else
                    Debug.Log($"Error: object id {words[1]} not found");
            }
        }        

        private void RotateShapeObj(List<string> input)
        {
            for (int i = 0; i < input.Count; i++)
            {
                //each input is a string with 3 words: rotate *id* *float/vector3*
                var words = SplitInputIntoWords(input[i]);
                if (_activeShapes.ContainsKey(words[1]))
                {
                    //attempt to parse float value from the string command
                    var parsedFloats = ParseCommandInput(words, out bool success);
                    if (success)
                    {
                        var objTransform = _activeShapes[words[1]].transform;
                        //apply rotation values according to the input given
                        objTransform.rotation = Quaternion.Euler(parsedFloats[0],
                            parsedFloats.Count > 1 ? parsedFloats[1] : 0,
                            parsedFloats.Count > 2 ? parsedFloats[2] : 0);
                    }
                }
                else
                    Debug.Log($"Error: object id {words[1]} not found");
            }
        }

        private void ScaleShapeObj(List<string> input)
        {
            for (int i = 0; i < input.Count; i++)
            {
                //each input input is a string with 3 words: scale *id* *float*
                var words = SplitInputIntoWords(input[i]);
                if (_activeShapes.ContainsKey(words[1]))
                {
                    //attempt to parse float value from the string command
                    var parsedFloats = ParseCommandInput(words, out bool success);
                    if (success)
                    {
                        var objTransform = _activeShapes[words[1]].transform;
                        //apply scale modifier
                        objTransform.localScale = objTransform.localScale * parsedFloats[0];
                    }
                }
                else
                    Debug.Log($"Error: object id {words[1]} not found");
            }
        }

        private void ChangeColorOfShapeObj(List<string> input)
        {
            for (int i = 0; i < input.Count; i++)
            {
                //each input input is a string with 3 words: change *id* color *white/black/red/green/blue*
                var words = SplitInputIntoWords(input[i]);
                if (_activeShapes.ContainsKey(words[1]))
                {
                    if (_activeShapes[words[1]].TryGetComponent(out IShapeObject shapeScript))
                        shapeScript.ChangeColor(words[3]);
                    else
                        Debug.Log("Error: missing IShapeObject script on the object");
                }
                else
                    Debug.Log($"Error: object id {words[1]} not found");
            }
        }

        private string[] SplitInputIntoWords(string input)
        {
            //separate input into individual words, if there's a vector save it as one word
            return Regex.Matches(input, @"([-+]?\d+(\.\d+)?(,\s*[-+]?\d+(\.\d+)?){0,2})|\S+")
                    .OfType<Match>()
                    .Select(m => m.Groups[0].Value)
                    .ToArray();
        }

        private List<float> ParseCommandInput(string[] inputWords, out bool success)
        {
            success = true;
            var floatList = new List<float>();

            //its a vector
            if (inputWords[2].Contains(','))
            {
                //separate the vector into individual floats
                var stringArr = inputWords[2].Split(',', StringSplitOptions.RemoveEmptyEntries);
                foreach (var str in stringArr)
                {
                    //attempt to parse each float
                    if (float.TryParse(str, out float result))
                        floatList.Add(result);
                    else
                    {
                        Debug.Log($"Error: {str} is not a valid float");
                        success = false;
                        break;
                    }
                }
            }
            else
            {
                //not a vector
                if (float.TryParse(inputWords[2], out float result))
                    floatList.Add(result);
                else
                {
                    success = false;
                    Debug.Log($"Error: {inputWords[2]} is not a valid float");
                }
            }

            return floatList;
        }
    }
}
