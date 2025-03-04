using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyMovement))] [RequireComponent(typeof(Animator))]
public class Enemy : UnitStateMachine
{
    private Vector3 _startPosition;
    private NavMeshAgent agent;

    protected new void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        _startPosition = transform.position;
        _currentState = new PreBattleEnemy(this, _startPosition, agent);
        UnitBattleManager.Instance.AddEnemy(unit);

    }

    protected override void ChangeState()
    {
        float enemyDistance = Single.MaxValue;
        if (unit.EnemyUnit != null)
        {
            if (!unit.EnemyUnit.IsDead)
                enemyDistance = Vector3.Distance(transform.position, unit.EnemyUnit.damagableTransform.position);
        }

        if (unit.HP <= 0)
        {
            if (_currentState != null)
                _currentState = new DeadState(this, OnDeath);
            return;
        }

        _currentState = enemyDistance <= unit.DetectRange
            ? new BattleEnemy(this, agent)
            : new PreBattleEnemy(this, _startPosition, agent);
    }

    protected new void OnDeath()
    {
        base.OnDeath();
        _currentState = null;
        UnitBattleManager.Instance.RemoveEnemy(unit);
        unit.enabled = false;
        Destroy(gameObject, 1f);
    }

}