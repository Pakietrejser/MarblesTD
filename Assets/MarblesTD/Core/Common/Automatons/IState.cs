namespace MarblesTD.Core.Common.Automatons
{
    public interface IState
    {
        void Enter();
        void Update(float timeDelta);
        void Exit();
    }
}