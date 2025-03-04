using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum PopUpType
    {
        CharacterUpgrade,
        Shop,
        Settings
    }

    [Serializable]
    public struct PopUp
    {
        public PopUpType PopUpType;
        public GameObject PopUpObject;
    }

    [CreateAssetMenu(order = 1, fileName = "PopUpSO", menuName = "ScriptableObject/PopUpSO")]
    public class PopUpSO : ScriptableObject
    {
        [NonReorderable]
        [SerializeField] private List<PopUp> popUpList;
        
        public List<PopUp> PopUpList => popUpList;
    }