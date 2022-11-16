namespace MarblesTD.Core.Common.Systems
{
    public abstract class System
    {
        public virtual void OnEnter() {}
        public virtual void OnExit() {}
        public virtual void OnPause() {}
        public virtual void OnResume() {}
        public virtual void OnTimeMultiplierChanged(float timeMultiplier = 1f) {}
    }
}