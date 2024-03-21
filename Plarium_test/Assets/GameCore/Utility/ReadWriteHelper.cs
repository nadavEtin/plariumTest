using System.IO;
using System;
using UnityEngine;

namespace Plarium.Assets.GameCore.Utility
{
    public static class ReadWriteHelper
    {
        private static string _filePath = string.Concat(Application.streamingAssetsPath, "PlariumTestLog.txt");

        public static void WriteToFile(object objectText)
        {
            string text = (string)objectText;
            
            try
            {
                //check if the log exists to avoid overwriting it
                if (File.Exists(_filePath))
                    File.AppendAllText(_filePath, string.Concat("\n", text));   //new line added for readability
                else
                    File.WriteAllText(_filePath, text);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
