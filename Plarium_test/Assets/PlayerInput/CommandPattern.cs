using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Plarium.Assets.PlayerInput
{
    public abstract class CommandPattern
    {
        public abstract void ExecuteCommand(List<string> commands);
    }

    public class Create : CommandPattern
    {
        public override void ExecuteCommand(List<string> commands)
        {
            Dictionary<string, GameObject> dic = new Dictionary<string, GameObject>();
            for (int i = 0; i < commands.Count; i++)
            {
                var words = Regex.Split(commands[i], @"\s+");
                switch (words[1])
                {
                    case "square":
                        //go = squarePrefab;
                        break;
                    case "triangle":
                        //go = trianglePrefab;
                        break;

                    case "circle":
                        //go = circlePrefab;
                        break;
                    default:

                        break;
                }
                //dic.Add(words[2], new GameObject());
            }
            //return dic;
        }
    }

    public class Move : CommandPattern
    {
        public override void ExecuteCommand(List<string> commands)
        {
            
        }
    }

    public class Scale : CommandPattern
    {
        public override void ExecuteCommand(List<string> commands)
        {
            
        }
    }

    public class Destroy : CommandPattern
    {
        public override void ExecuteCommand(List<string> commands)
        {
            
        }
    }

    public class ChangeColor : CommandPattern
    {
        public override void ExecuteCommand(List<string> commands)
        {
            
        }
    }
}
