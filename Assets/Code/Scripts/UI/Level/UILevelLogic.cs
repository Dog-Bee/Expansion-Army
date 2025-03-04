using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

    public class UILevelLogic : MonoBehaviour
    {
        public static Action LevelUpEvent;
        public static Action GetFinishedEvent;

        [SerializeField] private TextMeshProUGUI levelCountText;
        [SerializeField] private Slider levelSlider;
        [SerializeField] private TextMeshProUGUI enemyCountText;

        private LevelCount _levelCount;
        private SavableValue<int> _saveExpValue;

        private void OnEnable()
        {
            GetFinishedEvent += OnLevelUpdate;
            UnitBattleManager.EnemyKilledEvent += OnEnemyKilled;
        }

        private void OnDisable()
        {
            GetFinishedEvent -= OnLevelUpdate;
            UnitBattleManager.EnemyKilledEvent -= OnEnemyKilled;
        }

        private void Start()
        {
            _levelCount = new LevelCount();
            LevelUpEvent.Invoke();
            OnLevelUpdate();
           
        }
        
        private void OnLevelUpdate()
        {
            StartCoroutine(LevelUpdate());
        }
        private void OnEnemyKilled()
        {
            levelSlider.value = UnitBattleManager.Instance.EnemyUnitList.Count;
            enemyCountText.text = levelSlider.value + "/" + levelSlider.maxValue;
        }
      

        IEnumerator LevelUpdate()
        {
            yield return new WaitForSeconds(.1f);
            levelCountText.text = _levelCount.GetLevel(LevelType.BattleLevel).ToString();
            levelSlider.maxValue = UnitBattleManager.Instance.EnemyUnitList.Count;
            levelSlider.value = levelSlider.maxValue;
            enemyCountText.text = levelSlider.value + "/" + levelSlider.maxValue;
            LevelUpEvent?.Invoke();
        }
    }
