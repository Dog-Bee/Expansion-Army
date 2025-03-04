using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [Serializable]
    public struct StatUpgradeStruct
    {
        public int Cost;
        public float UpgradeValue;
    }
    [CreateAssetMenu(order = 1,fileName = "StatUpgradeSO", menuName = "ScriptableObject/StatUpgradeSO")]
    public class StatUpgradeSO : ScriptableObject
    {
        [NonReorderable]
        [SerializeField] private List<StatUpgradeStruct> upgrades;

        public List<StatUpgradeStruct> Upgrades => upgrades;
    }
