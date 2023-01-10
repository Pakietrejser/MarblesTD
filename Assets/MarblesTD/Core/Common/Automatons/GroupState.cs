using System.Collections.Generic;

namespace MarblesTD.Core.Common.Automatons
{
    public class GroupState : IState
    {
        public bool IsActive { get; private set; }
        readonly List<IState> _states;

        public GroupState(List<IState> states)
        {
            _states = states;
        }
        
        public void EnterState()
        {
            if (IsActive) return;
            IsActive = true;
            _states.ForEach(state => state.EnterState());
        }

        public void UpdateState(float timeDelta)
        {
            foreach (var state in _states)
            {
                if (state is IUpdateState updateState)
                {
                    updateState.UpdateState(timeDelta);
                }
            }
        }

        public void ExitState()
        {
            if (!IsActive) return;
            IsActive = false;
            _states.ForEach(state => state.ExitState());
        }
    }
}