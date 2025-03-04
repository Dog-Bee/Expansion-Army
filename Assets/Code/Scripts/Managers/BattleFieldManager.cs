using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

    public class BattleFieldManager : MonoBehaviour
    {
        public static Action LevelFinishEvent;
        [SerializeField] private LevelSO levelSO;
        [SerializeField] private ChestPrize gift;

        private GameObject _currentLevel;

        private void OnEnable()
        {
            GameManager.OnAfterStateChanged += OnGameStateChange;
            LevelFinishEvent += OnLevelFinishEvent;
        }

        private void OnDisable()
        {
            GameManager.OnAfterStateChanged += OnGameStateChange;
            LevelFinishEvent -= OnLevelFinishEvent;
        }

        private void Start()
        {
            SpawnLevel();
        }

        private void OnGameStateChange(GameState gameState)
        {
            /*if (gameState == GameState.Victory)
            {
                Instantiate(gift, _currentLevel.transform);
            }*/
        }

        private void SpawnLevel()
        {
            _currentLevel = Instantiate(levelSO.GetLevel(), transform);
        }

        private void OnLevelFinishEvent()
        {
            Destroy(_currentLevel);
            SpawnLevel();
            GameManager.Instance.ChangeState(GameState.MainMenu);
        }
    }
