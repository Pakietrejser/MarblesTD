using System;
using Newtonsoft.Json;
  
namespace MarblesTD.UnityCore.Systems.Saving
{
    [Serializable]
    public class SaveData
    {
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.Auto});
        }
        
        public SaveData LoadFromJson(string json)
        {
            return JsonConvert.DeserializeObject<SaveData>(json, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto});
        }
    }
}