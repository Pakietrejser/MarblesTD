using System.Collections.Generic;
using MarblesTD.Core.Common.Automatons;

namespace MarblesTD.Core.ScenarioSystems
{
    public class TowerController : IUpdateState
    {
        Dictionary<string, bool> AvailableTowers = new Dictionary<string, bool>();

        public void EnterState()
        {
            throw new System.NotImplementedException();
        }

        public void ExitState()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateState(float timeDelta)
        {
            throw new System.NotImplementedException();
        }
        
        public interface IView
        {
            void CreateTower();
            void DestroyAllTowers();
        }
    }
}