using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Barricade : Melee
    {
        private void Start()
        {
            UnitBattleManager.Instance.AddEnemy(this);
        }

        private void Update()
        {
            CheckHealth();
        }

        private void CheckHealth()
        {
            if (HP <= 0)
            {
                resourceDropSo.GetResource(transform.position);
                UnitBattleManager.Instance.RemoveEnemy(this);
                Destroy(gameObject);
            }
        }
    }

