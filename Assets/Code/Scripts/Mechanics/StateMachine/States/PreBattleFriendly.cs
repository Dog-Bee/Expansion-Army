using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

    public class PreBattleFriendly : State
    {
        private Vector3 _startPosition;
        private NavMeshAgent _agent;
        private float _timer = 1f;
        public override void DoState()
        {
            StateMachine.BaseMovement.Move(_startPosition);
            StateMachine.transform.LookAt(_startPosition);
            TriggersReset();
            StateMachine.animator.SetTrigger(Vector3.Distance(StateMachine.transform.position, _startPosition) <= .1f
                ? "Idle"
                : "Walk");
            _agent.stoppingDistance = 0;
            StateMachine.Unit.HealthRegeneration();
        }
        
        public PreBattleFriendly(UnitStateMachine stateMachine,Vector3 startPosition,NavMeshAgent agent):base (stateMachine)
        {
            _startPosition = startPosition;
            _agent = agent;
        }
        
    }
