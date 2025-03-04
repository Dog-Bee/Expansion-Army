using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public abstract class State
    {
        protected UnitStateMachine StateMachine;
     
        public abstract void DoState();

        public State()
        {
            
        } 
        public State(UnitStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }
        
        protected void TriggersReset()
        {
            foreach (var param in StateMachine.animator.parameters)
            {
                StateMachine.animator.ResetTrigger(param.name);
            }
        }
    }
