using System;
using System.IO;
using UnityEngine;

namespace MarblesTD.UnityCore.Systems.Game.Saving
{
    public static class FileManager
    {
        public static string GetFullPath(string fileName) => Path.Combine($"{Application.persistentDataPath}", "saves" , fileName);
        public static string GetPartialPath() => Path.Combine($"{Application.persistentDataPath}", "saves");

        public static bool WriteToFile(string fileName, string data)
        {
            string fullPath = GetFullPath(fileName);
            
            if(!Directory.Exists(GetPartialPath()))
            {
                Directory.CreateDirectory(GetPartialPath());
            }

            try
            {
                using var streamWriter = new StreamWriter(fullPath);
                streamWriter.Write(data);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to read from {fullPath}, {e}");
                return false;
            }
        }

        public static bool LoadFromFile(string fileName, out string data)
        {
            string fullPath = GetFullPath(fileName);
            
            if(!Directory.Exists(GetPartialPath()))
            {
                Directory.CreateDirectory(GetPartialPath());
            }

            try
            {
                data = File.ReadAllText(fullPath);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to read from {fullPath}, {e}");
                data = string.Empty;
                return false;
            }
        }
    }
}