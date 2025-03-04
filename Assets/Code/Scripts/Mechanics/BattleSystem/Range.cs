using Unity.Mathematics;
using UnityEngine;


    public class Range : Unit
    {
        [SerializeField] private Bullet bulletObject;

        protected new void Awake()
        {
            base.Awake();
        }
        protected override void Attack()
        {
            if (EnemyUnit != null)
            {
                if (Vector3.Distance(transform.position, EnemyUnit.damagableTransform.position) <= attackRange)
                {
                    Instantiate(bulletObject,transform.position,quaternion.identity).Initialize(EnemyUnit,damage);
                }
            }
        }
    }