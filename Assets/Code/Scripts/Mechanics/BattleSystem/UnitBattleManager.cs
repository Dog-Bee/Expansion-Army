using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

    public class UnitBattleManager : PersistentSingleton<UnitBattleManager>
    {
        public static Action EnemyKilledEvent;
        
        [SerializeField] private List<Unit> friendlyUnitList;
        [SerializeField] private List<Unit> enemyUnitList;
        public List<Unit> FriendlyUnitList => friendlyUnitList;
        public List<Unit> EnemyUnitList => enemyUnitList;


        public void AddEnemy(Unit enemy)
        {
            enemyUnitList.Add(enemy);
        }

        public void AddFriendly(Unit friendly)
        {
            friendlyUnitList.Add(friendly);
        }

        public void RemoveEnemy(Unit Enemy)
        {
            enemyUnitList.Remove(Enemy);
            EnemyKilledEvent?.Invoke();
            if (enemyUnitList.Count == 0)
            {
                GameManager.Instance.ChangeState(GameState.Victory);
            }
        }

        public void RemoveFriendly(Unit Friendly)
        {
            friendlyUnitList.Remove(Friendly);
            if (friendlyUnitList.Count(unit => unit != null) <= 1)
            {
                GameManager.Instance.ChangeState(GameState.MainMenu);
            }
        }
        
    }