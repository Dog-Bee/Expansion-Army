using UnityEngine;


    public class Melee : Unit
    {
        protected new void Awake()
        {
            base.Awake();
        }
        
        protected override void Attack()
        {
            if (EnemyUnit != null)
            {
                if (EnemyUnit.IsDead) return;
                if(Vector3.Distance(transform.position,EnemyUnit.damagableTransform.position)<=attackRange)
                    EnemyUnit.GetHit(damage);
            }
        }
    }
