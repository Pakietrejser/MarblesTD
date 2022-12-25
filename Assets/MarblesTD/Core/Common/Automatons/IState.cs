namespace MarblesTD.Core.Common.Automatons
{
    public interface IState
    {
        void Enter();
        void Exit();
    }

    public interface IUpdateState : IState
    {
        void UpdateState(float timeDelta); 
    }
}