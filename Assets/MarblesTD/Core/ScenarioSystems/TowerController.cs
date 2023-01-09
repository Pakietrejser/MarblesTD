using MarblesTD.Core.Common.Automatons;

namespace MarblesTD.Core.ScenarioSystems
{
    public class TowerController : IUpdateState
    {
        public void Enter()
        {
            throw new System.NotImplementedException();
        }

        public void Exit()
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