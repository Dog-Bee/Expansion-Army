using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

    public class BattleEnemy : State
    {
        private NavMeshAgent _agent;

        public override void DoState()
        {
            StateMachine.Unit.FindEnemy(UnitBattleManager.Instance.FriendlyUnitList);
            if (StateMachine.Unit.EnemyUnit == null) return;
            StateMachine.BaseMovement.Move(StateMachine.Unit.EnemyUnit.damagableTransform.position);
            TriggersReset();
            StateMachine.animator.SetTrigger(StateMachine.Unit.CheckDistance() ? "Attack" : "Walk");
            if (Vector3.Distance(StateMachine.transform.position, StateMachine.Unit.EnemyUnit.damagableTransform.position) <=
                StateMachine.Unit.DetectRange)
            {
                StateMachine.transform.LookAt(StateMachine.Unit.EnemyUnit.damagableTransform);
            }
        }

        public BattleEnemy(UnitStateMachine stateMachine, NavMeshAgent agent) : base(stateMachine)
        {
            _agent = agent;
            _agent.stoppingDistance = StateMachine.Unit.AttackRange;
        }
    }