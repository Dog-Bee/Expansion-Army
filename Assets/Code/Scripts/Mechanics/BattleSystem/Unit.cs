using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

    public abstract class Unit : MonoBehaviour,IDamagable
    {
        [SerializeField] protected float hp;
        [SerializeField] protected float healthRegen;
        [SerializeField] protected float damage;
        [SerializeField] protected float attackRange;
        [SerializeField] protected float detectRange;
        [SerializeField] protected float attackDelay;
        [SerializeField] protected float healDelay;
        [SerializeField] protected ParticleSystem hitParticle;
        [SerializeField] protected ResourceDropSO resourceDropSo;

        public Transform damagableTransform => transform;
        public bool IsDead { get; set; }
        private IDamagable _enemyUnit;
        private float _hp;
        private float _timer;
        private float _healDelay;
        private float healTimer;
        
        public float DetectRange => detectRange;

        
        public ResourceDropSO ResourceDropSo => resourceDropSo;
        public float AttackRange => attackRange;
        public IDamagable EnemyUnit
        {
            get => _enemyUnit;
            set
            {
                _enemyUnit = value;
            }
        }

        public float HealthRegen
        {
            get => healthRegen;
            set => healthRegen = value;
        }

        public float MaxHP => hp;
        public float HP
        {
            get => _hp;
            set
            {
                hp = value;
                _hp = value;
            }
        }

        public float Damage
        {
            get => damage;
            set => damage = value;
        }

        protected void Awake()
        {
            _hp = hp;
            _timer = attackDelay;
            _healDelay = healDelay;
        }

        private void Update()
        {
            
        }


        public void HealthRegeneration()
        {
            _healDelay -=Time.deltaTime;
            _timer -=Time.deltaTime;
            if (_healDelay <= 0 && _timer <= 0)
            {
                _timer = 1;
                Heal();
            }
        }


        public void GetHit(float damage)
        {
            _hp -= damage;
            hitParticle.Stop();
            hitParticle.Play();
            if (_hp <= 0)
                IsDead = true;
            
            _hp = Mathf.Clamp(_hp, 0, hp);
            
        }

        public void Heal()
        {
            _hp += healthRegen;
            _hp = Mathf.Clamp(_hp, 0, hp);
        }

        public void FindEnemy(List<Unit> enemyList)
        {
            Unit tempUnit = null;
            float distance = Single.MaxValue;
            foreach (var enemy in enemyList.Where(enemy=> enemy!=null))
            {
                float tempDistance = Vector3.Distance(transform.position, enemy.transform.position);
                if(tempDistance>distance) continue;
                tempUnit = enemy;
                distance = tempDistance;
            }

            EnemyUnit = tempUnit;
        }

        public bool CheckDistance()
        {
            _timer -= Time.fixedDeltaTime;
            if (!(Vector3.Distance(transform.position, _enemyUnit.damagableTransform.position) <= attackRange) ||
                !(_timer <= 0))
            {
                return false;
            }

            _healDelay = healDelay;
            _timer = attackDelay;
            return true;
            //Attack();

        }
        

        protected abstract void Attack();





    }
