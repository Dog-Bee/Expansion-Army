using System.Threading.Tasks;
using UnityEngine;
using UrFairy;

    public class BattlePlayer : State
    {
        private TouchController _touchController;

        public override void DoState()
        {
            if (StateMachine.Unit.EnemyUnit!=null)
            {
                if (StateMachine.Unit.EnemyUnit.IsDead)
                {
                    StateMachine.Unit.FindEnemy(UnitBattleManager.Instance.EnemyUnitList);
                }
                else
                {
                    if (!StateMachine.Unit.EnemyUnit.damagableTransform.CompareTag("Damagable"))
                    {
                        
                        StateMachine.Unit.FindEnemy(UnitBattleManager.Instance.EnemyUnitList);
                    }
                }
            }
            else
            {
                StateMachine.Unit.FindEnemy(UnitBattleManager.Instance.EnemyUnitList);
            }

            StateMachine.BaseMovement.Move(_touchController.ComputedDestination);
            TriggersReset();
            StateMachine.animator.SetTrigger(_touchController.ComputedDestination == Vector3.zero ? "Idle" : "Walk");
            if (StateMachine.Unit.EnemyUnit == null)
            {
                if (_touchController.ComputedDestination != Vector3.zero)
                    StateMachine.transform.rotation = Quaternion.LookRotation(_touchController.ComputedDestination);
                return;
            }
            if (StateMachine.Unit.CheckDistance())
                StateMachine.animator.SetTrigger("Attack");
            if (Vector3.Distance(StateMachine.transform.position,
                    StateMachine.Unit.EnemyUnit.damagableTransform.transform.position) <=
                StateMachine.Unit.DetectRange && StateMachine.Unit.EnemyUnit != null)
            {
                StateMachine.transform.LookAt(StateMachine.Unit.EnemyUnit.damagableTransform.transform);
            }
            else
            {
                StateMachine.Unit.HealthRegeneration();
                if (_touchController.ComputedDestination != Vector3.zero)
                    StateMachine.transform.rotation = Quaternion.LookRotation(_touchController.ComputedDestination);
            }
        }

        public BattlePlayer(UnitStateMachine stateMachine, TouchController touchController) : base(stateMachine)
        {
            _touchController = touchController;
        }

       
    }