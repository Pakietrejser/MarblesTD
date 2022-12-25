using MarblesTD.UnityCore.Systems.Game;
using MarblesTD.UnityCore.Systems.Game.Saving;
using UnityEngine;
using Zenject;

namespace MarblesTD.UnityCore.Systems.Installers
{
    public class GameSystemsInstaller : MonoInstaller
    {
        [SerializeField] SaveWindow saveWindow;
        [SerializeField] MainMenu mainMenu;
        [SerializeField] GameSettings gameSettings;
        
        public override void InstallBindings()
        {
            Container.Bind<SaveWindow>().FromInstance(saveWindow);
            Container.Bind<MainMenu>().FromInstance(mainMenu);
            Container.Bind<GameSettings>().FromInstance(gameSettings);
        }
    }
}