using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

    public class BattleFriendly : State
    {
        private NavMeshAgent _agent;

        public override void DoState()
        {
            StateMachine.Unit.FindEnemy(UnitBattleManager.Instance.EnemyUnitList);
            if (StateMachine.Unit.EnemyUnit == null) return;
            
            _agent.stoppingDistance = StateMachine.Unit.AttackRange;
            StateMachine.BaseMovement.Move(StateMachine.Unit.EnemyUnit.damagableTransform.position);
            TriggersReset();
            //StateMachine.animator.SetTrigger(StateMachine.Unit.CheckDistance() ? "Attack" : "Walk");
            if (StateMachine.Unit.CheckDistance())
            {
                StateMachine.animator.SetTrigger("Attack");
            }
            else
            {
                if (Vector3.Distance(StateMachine.Unit.transform.position,StateMachine.Unit.EnemyUnit.damagableTransform.position)<=StateMachine.Unit.AttackRange)
                {
                    StateMachine.animator.SetTrigger("Idle");
                }
                else
                {
                    StateMachine.animator.SetTrigger("Walk");
                }
            }
            
            if (Vector3.Distance(StateMachine.transform.position, StateMachine.Unit.EnemyUnit.damagableTransform.position) <=
                StateMachine.Unit.DetectRange)
            {
                StateMachine.transform.LookAt(StateMachine.Unit.EnemyUnit.damagableTransform);
            }
        }

        public BattleFriendly(UnitStateMachine stateMachine, NavMeshAgent agent) : base(stateMachine)
        {
            _agent = agent;
        }
    }