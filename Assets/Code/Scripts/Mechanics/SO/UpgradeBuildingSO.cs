using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [Serializable]
    public class ListHolder
    {
        [NonReorderable]
        public List<ResourceHolder> ResourceHolders;

        public GameObject buildObject;
        public int level;
    }
    
    
    [CreateAssetMenu(order = 1,fileName = "UpgradeStatSo", menuName = "ScriptableObject/UpgradeStatSo")]
    public class UpgradeBuildingSO : ScriptableObject
    {
        [NonReorderable]
        [SerializeField] private List<ListHolder> resourceLevels;
        [NonReorderable]
        [SerializeField] private List<ResourceHolder> unlockPrice;
        [SerializeField] private int unlockLevel;

        public List<ResourceHolder> UnlockPrice => unlockPrice;
        public List<ListHolder> ResourceLevels => resourceLevels;
        public GameObject DefaultBuilding;
        public int UnlockLevel => unlockLevel;
    }

