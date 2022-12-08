using MarblesTD.Core.Entities.Marbles;
using Zenject;

namespace MarblesTD.UnityCore.Entities.Installers
{
    public class EntityInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindMemoryPool<Marble, Marble.Pool>().WithInitialSize(100).ExpandByDoubling();
        }
    }
}