using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;


    [RequireComponent(typeof(EnemyMovement))]
    [RequireComponent(typeof(Animator))]
    public class Friendly : UnitStateMachine
    {
        private Vector3 _startPosition;
        private NavMeshAgent agent;
        
        private void OnEnable()
        {
            BattleFieldManager.LevelFinishEvent += OnLevelFinished;
        }

        private void OnDisable()
        {
            BattleFieldManager.LevelFinishEvent -= OnLevelFinished;
        }

        private void Start()
        {
            _currentState = new PreBattleFriendly(this, _startPosition, agent);
            UnitBattleManager.Instance.AddFriendly(unit);
        }
        
        protected override void ChangeState()
        {
            if (unit.HP <= 0)
            {
                _currentState = new DeadState(this,OnDeath);
                return;
            }

            if (unit.EnemyUnit == null)
            {
                _currentState = new PreBattleFriendly(this, _startPosition, agent);
            }

            if (GameManager.Instance.State == GameState.Game)
            {
                _currentState = new BattleFriendly(this, agent);
            }
        }

        public void Initialize(Vector3 startPosition)
        {
           agent = GetComponent<NavMeshAgent>();
           _startPosition = startPosition;
        }

        protected new void OnDeath()
        {
            base.OnDeath();
            UnitBattleManager.Instance.RemoveFriendly(unit);
            unit.enabled = false;
            Destroy(gameObject);
        }

        private void OnLevelFinished()
        {
            transform.position = _startPosition;
        }
    }