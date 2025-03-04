using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(order = 1,fileName = "UpgradesSO", menuName = "ScriptableObject/UpgradesSO")]
    public class UpgradesSO : ScriptableObject
    {
        [SerializeField] private List<GameObject> upgradesGO;

        public List<GameObject> UpgradeSO=>upgradesGO;

    }

