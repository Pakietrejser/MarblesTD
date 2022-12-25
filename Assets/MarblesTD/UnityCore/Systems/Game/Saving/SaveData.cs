using UnityEngine;

namespace MarblesTD.UnityCore.Systems.Game.Saving
{
    public struct SaveData
    {
        public float MasterVolume;
        public float MusicVolume;
        public float SfxVolume;
        public bool MuteWhileInBackground;
        public FullScreenMode WindowMode;
        public Resolution Resolution;
        public int Framerate;
        public bool VsyncEnabled;
        public bool ScreenShakeEnabled;
        public bool UploadGameplayDataEnabled;
    }
}