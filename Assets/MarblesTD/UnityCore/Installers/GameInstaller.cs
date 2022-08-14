using UnityEngine;
using Zenject;

namespace MarblesTD.UnityCore.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // Container.BindMemoryPool<>()
        }
    }

    public class TestMono : MonoBehaviour
    {
        public void Populate(int test)
        {
            
        }

        public void Clear()
        {
            
        }
    }
    
    // public class Pool : MonoMemoryPool<>
}