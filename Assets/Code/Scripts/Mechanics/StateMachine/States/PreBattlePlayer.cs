using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class PreBattlePlayer : State
    {
        private TouchController _touchController;
        public override void DoState()
        {
            StateMachine.BaseMovement.Move(_touchController.ComputedDestination);
        }
        
        public PreBattlePlayer(UnitStateMachine stateMachine,TouchController touchController):base (stateMachine)
        {
            _touchController = touchController;
        }
    }
