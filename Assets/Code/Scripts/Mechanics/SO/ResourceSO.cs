using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceNameEnum
    {
        Bone,
        Brain,
        Gold,
        Dust
    }
    [Serializable]
    public struct ResourceHolder
    {
        public ResourceSO ResourceData;
        public uint ResourceCount;
    }

    [CreateAssetMenu(order = 1,fileName = "ResourceSO", menuName = "ScriptableObject/ResourceSO")]
    public class ResourceSO : ScriptableObject
    {
       [SerializeField] private ResourceNameEnum resourceNameEnum;
       [SerializeField] private ResourceObject resourceObj;
       [SerializeField] private Sprite resourceSprite;
        
        
        
        public ResourceNameEnum ResourceNameEnum=>resourceNameEnum;
        public ResourceObject ResourceObj=>resourceObj;
        public Sprite ResourceSprite=>resourceSprite;
    }
