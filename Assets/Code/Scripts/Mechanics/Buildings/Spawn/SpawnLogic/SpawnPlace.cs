using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

    public class SpawnPlace : MonoBehaviour
    {
        [SerializeField] private List<SpawnPlaceHolder> spawnPlaces;
        [SerializeField] private List<GameObject> imagesList;
        [SerializeField] private StatUpgradeSO spawnPlaceStat;
        [SerializeField] private string mobName;


        private bool _isUnlocked;

        public bool IsUnlocked
        {
            get => _isUnlocked;
            set
            {
                _isUnlocked = value;
                OnUpgrade();
            }
        }

        private void OnEnable()
        {
            PopUpUpgradeHeadBuilding.OnHeadBuildingUpgrade += OnUpgrade;
        }

        private void OnDisable()
        {
            PopUpUpgradeHeadBuilding.OnHeadBuildingUpgrade -= OnUpgrade;
        }
        
        public SpawnPlaceHolder GetSpawnPlace()
        {
            return spawnPlaces.FirstOrDefault(place => place.UnitOnPlace == null && place.gameObject.activeSelf);
        }
        
        public void OnUpgrade()
        {
           if (!_isUnlocked) return;
            if (spawnPlaceStat == null)
            {
                spawnPlaces[0].gameObject.SetActive(true);
                imagesList[0].SetActive(false);
                return;
            }
            SavableValue<int> tempLevel = new SavableValue<int>(mobName+"Level");
            for (int i = 0; i <= tempLevel.Value; i++)
            {
                imagesList[i].SetActive(false);
            }
            for (int i = 0; i < spawnPlaceStat.Upgrades[tempLevel.Value].UpgradeValue; i++)
            {
                if (i < spawnPlaces.Count)
                {
                    spawnPlaces[i].gameObject.SetActive(true);
                }
            }
        }
    }