using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(order = 1, fileName = "GameLevelBalanceSO", menuName = "ScriptableObject/GameLevelBalanceSO")]
    public class GameLevelBalanceSO : ScriptableObject
    {
        [SerializeField] private List<int> expPointsList;

        public int GetCurrentMaxExp(int level)
        {
            return level >= expPointsList.Count ? expPointsList[^1] : expPointsList[level];
        }
    }