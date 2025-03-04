using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


    public class PreBattleEnemy : State
    {
        private Vector3 _startPosition;
        private NavMeshAgent _agent;
        public override void DoState()
        {
            StateMachine.Unit.FindEnemy(UnitBattleManager.Instance.FriendlyUnitList);
            StateMachine.BaseMovement.Move(_startPosition);
            if (StateMachine.Unit.EnemyUnit == null) return;
            
           if (Vector3.Distance(StateMachine.transform.position, StateMachine.Unit.EnemyUnit.damagableTransform.transform.position) <=
               StateMachine.Unit.DetectRange)
           {
               StateMachine.transform.LookAt(StateMachine.Unit.EnemyUnit.damagableTransform.transform);
           }
           else
           {
               StateMachine.Unit.HealthRegeneration();
           }
           TriggersReset();
           StateMachine.animator.SetTrigger(Vector3.Distance(StateMachine.transform.position, _startPosition) <= .5f
               ? "Idle"
               : "Walk");

        }
        public PreBattleEnemy(UnitStateMachine stateMachine, Vector3 startPosition,NavMeshAgent agent):base (stateMachine)
        {
            _startPosition = startPosition;
            _agent = agent;
            _agent.stoppingDistance = 0;

        }
    }
