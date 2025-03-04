using System;

    public class DeadState : State
    {
        private Action _dieAction;
        public override void DoState()
        {
            //StateMachine.OnDeath();
            TriggersReset();
            _dieAction.Invoke();
        }

        public DeadState(UnitStateMachine stateMachine,Action dieAction) : base(stateMachine)
        {
            _dieAction = dieAction;
        }
        
    }
