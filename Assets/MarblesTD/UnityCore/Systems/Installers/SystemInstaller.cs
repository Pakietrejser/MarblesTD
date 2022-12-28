﻿using MarblesTD.Core.ScenarioSystems;
using MarblesTD.UnityCore.Systems.ScenarioSystems;
using UnityEngine;
using Zenject;

namespace MarblesTD.UnityCore.Systems.Installers
{
    public class SystemInstaller : MonoInstaller
    {
        [SerializeField] MarbleControllerView marbleControllerView;
        [SerializeField] TimeControllerView timeControllerView;

        public override void InstallBindings()
        {
            Container.Bind<MarbleController.IView>().FromInstance(marbleControllerView);
            Container.Bind<TimeController.IView>().FromInstance(timeControllerView);

            Container.Bind<MarbleController>().AsSingle().NonLazy();
            Container.Bind<TimeController>().AsSingle().NonLazy();
        }
    }
}