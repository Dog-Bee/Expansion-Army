using System.Collections.Generic;
using UnityEngine;

  [CreateAssetMenu(order = 1,fileName = "LevelSO", menuName = "ScriptableObject/LevelSO")]
    public class LevelSO : ScriptableObject
    {
        [SerializeField] private List<GameObject> levelList;


        public GameObject GetLevel()
        {
            LevelCount levelCount = new LevelCount();
            return levelCount.GetLevel(LevelType.BattleLevel)-1>=levelList.Count ? levelList[Random.Range(0, levelList.Count)] : levelList[levelCount.GetLevel(LevelType.BattleLevel)-1];
        }
    }
