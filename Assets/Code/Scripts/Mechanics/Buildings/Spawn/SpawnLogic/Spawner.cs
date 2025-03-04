using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

    public class Spawner : Building
    {
        [SerializeField] protected Unit spawnObject;
        [SerializeField] protected SpawnPlace spawnPlace;
        [SerializeField] protected float spawnDelay;
        [SerializeField] protected UpgradesSO unitUpgrades;


        private float _timer;

        protected new void Awake()
        {
            base.Awake();
        }
        protected new void Start()
        {
            base.Start();
            _timer = spawnDelay;
        }

        protected void Update()
        {
            if (!isUnlocked) return;
            _timer -= Time.deltaTime;
        }

        protected void UnlockSpawn()
        {
            StartCoroutine(UnlockSpawnCoroutine());
        }

        protected void CheckSpawn()
        {
            if (_timer > 0) return;
            SpawnPlaceHolder tempSpawn = spawnPlace.GetSpawnPlace();
            if (tempSpawn == null)
            {
                _timer = spawnDelay;
                return;
            }

            Unit tempUnit = Instantiate(spawnObject, transform.position, Quaternion.identity);
            tempSpawn.UnitOnPlace = tempUnit.gameObject;
            tempUnit.GetComponent<Friendly>().Initialize(tempSpawn.transform.position);
            //UnitBattleManager.Instance.FriendlyUnitList.Add(tempUnit);
            _timer = spawnDelay;
        }

        IEnumerator UnlockSpawnCoroutine()
        {
            yield return new WaitForSeconds(1.1f);
            spawnPlace.IsUnlocked = isUnlocked;
        }
        
    }