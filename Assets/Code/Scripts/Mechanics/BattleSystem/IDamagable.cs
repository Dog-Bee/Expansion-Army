using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public interface IDamagable
    {
        public Transform damagableTransform { get; }
        public bool IsDead { get; set; }
        public void GetHit(float damage);
    }
