using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum LevelType
    {
        BattleLevel,
        //ExpLevel
    }
    
    public class LevelCount
    {
        private int _levelNumber;
        private SavableValue<int> _saveLevel;

        public void LevelUp(LevelType levelType)
        {
            _saveLevel = new SavableValue<int>(levelType.ToString(), 1);
            _saveLevel.Value += 1;
        }

        public int GetLevel(LevelType levelType)
        {
            _saveLevel = new SavableValue<int>(levelType.ToString(), 1);
            return _saveLevel.Value;
        }

    }
