using System;
using System.IO;
using Newtonsoft.Json;

namespace MarblesTD.UnityCore.Systems.Saving
{
    public class SerializationManager
    {
        public SaveData SerializedSave;
        string CurrentSaveName { get; set; }
        
        void SaveData(SaveData saveData)
        {
            // we don't save anything atm
        }

        void LoadData(SaveData saveData)
        {
            // we don't save anything atm
        }
        
        public void SaveJsonData(string fileName)
        {
            if (string.IsNullOrEmpty(CurrentSaveName)) throw new Exception("save empty!");
            
            var saveData = new SaveData();
            SaveData(saveData);
            string saveDataString  = JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.Auto});
            if (!FileManager.WriteToFile($"{fileName}.json", saveDataString))
            {
                CurrentSaveName = fileName;
            }
        }

        public bool LoadJsonData(string fileName)
        {
            if (!FileManager.LoadFromFile($"{fileName}.json", out string json))
            {
                return false;
            }
            
            var saveData = JsonConvert.DeserializeObject<SaveData>(json, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto});
            LoadData(saveData);
            CurrentSaveName = fileName;
            return true;
        }

        public void RemoveJsonData(string fileName)
        {
            string filePath = FileManager.GetFullPath(fileName);
            filePath = $"{Path.GetFullPath(filePath)}.json";
            
            if (File.Exists(filePath))
            { 
                File.Delete(filePath);
                return;
            }

            foreach (string file in Directory.GetFiles(FileManager.GetPartialPath()))
            {
                if (!file.Contains(fileName)) continue;
                File.Delete(file);
                return;
            }

            throw new Exception($"Couldnt remove a file {fileName} from {FileManager.GetPartialPath()}");
        }
    }
}