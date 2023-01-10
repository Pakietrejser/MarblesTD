namespace MarblesTD.Core.Common.Automatons
{
    public interface IState
    {
        void EnterState();
        void ExitState();
    }

    public interface IUpdateState : IState
    {
        void UpdateState(float timeDelta); 
    }
}