using MarblesTD.Core.Entities.Marbles;
using Zenject;

namespace MarblesTD.UnityCore.Installers
{
    public class EntityInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindFactory<Marble, Marble.Factory>();
        }
    }
}