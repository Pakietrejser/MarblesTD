using MarblesTD.Core.Common.Automatons;
using UnityEngine;

namespace MarblesTD.UnityCore.Systems.Map
{
    public class ScenarioSpawner : MonoBehaviour, IState
    {
        [SerializeField] ScenarioButton main;

        void Start()
        {
            main.SetAsActive();
        }

        public void Enter()
        {
            main.SetAsActive();
        }

        public void Exit()
        {
            
        }
    }
}