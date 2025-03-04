using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class BattleTrigger : MonoBehaviour,IInteractable
    {
        private TestFiller _filler;

        private void Awake()
        {
            _filler = GetComponent<TestFiller>();
        }

        public void Interact()
        {
            if (UnitBattleManager.Instance.FriendlyUnitList.Count <= 1|| UnitBattleManager.Instance.EnemyUnitList.Count<=1  || GameManager.Instance.State == GameState.Game) return;
            _filler.StartInteract(()=>GameManager.Instance.ChangeState(GameState.Game));
        }

        public void StopInteract()
        {
            _filler.StopInteract();
        }
    }
