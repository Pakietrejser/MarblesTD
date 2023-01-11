using MarblesTD.Core.Common.Enums;
using UnityEngine;

namespace MarblesTD.UnityCore.Systems.GameSystems.Saving
{
    public class SaveData
    {
        public float MasterVolume;
        public float MusicVolume;
        public float SfxVolume;
        public bool MuteWhileInBackground;
        public FullScreenMode WindowMode;
        public Resolution Resolution;
        public int Framerate;
        public bool VsyncEnabled;

        public (ScenarioID, bool, bool, bool)[] ScenarioQuests;

        public bool[] TowerUnlocks;
    }
}