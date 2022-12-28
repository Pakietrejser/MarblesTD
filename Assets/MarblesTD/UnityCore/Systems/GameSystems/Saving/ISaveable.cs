namespace MarblesTD.UnityCore.Systems.GameSystems.Saving
{
    public interface ISaveable
    {
        void Save(SaveData saveData, bool freshSave);
        void Load(SaveData saveData);
    }
}