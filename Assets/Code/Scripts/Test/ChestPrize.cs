using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class ChestPrize : MonoBehaviour,IInteractable
    {
        [SerializeField] private ResourceHolder prize;
        [SerializeField] private ParticleSystem chestParticle;

        private Animator _animator;
        private bool _isOpen;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            UnitBattleManager.EnemyKilledEvent += OnEnemyKilled;
        }

        private void OnDisable()
        {
            UnitBattleManager.EnemyKilledEvent -= OnEnemyKilled;
        }

        private void OnEnemyKilled()
        {
            if (UnitBattleManager.Instance.EnemyUnitList.Count <= 0)
            {
                _animator.SetTrigger("Intro");
            }
        }


        private void OpenChest()
        {
            StartCoroutine(ChestCoroutine());
        }

        IEnumerator ChestCoroutine()
        {
            _isOpen = true;
            _animator.SetTrigger("Open");
            chestParticle.Play();
            CurrencyManager.Instance.AddCurrency(prize.ResourceData.ResourceNameEnum,prize.ResourceCount);
            yield return new WaitForSeconds(2f);
            new LevelCount().LevelUp(LevelType.BattleLevel);
            BattleFieldManager.LevelFinishEvent?.Invoke();
            UILevelLogic.GetFinishedEvent?.Invoke();
            Destroy(gameObject);
        }

        public void Interact()
        {
            if (GameManager.Instance.State != GameState.Victory|| _isOpen) return;
            OpenChest();
            
        }

        public void StopInteract()
        {
            
        }
    }
