using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using UnityEngine;

    public class Bullet : MonoBehaviour
    {
        private float _damage;
        private Transform _target;
        private float _timer;
        private IDamagable _iDamage;
        
        Vector3 tempVector;
        void Update()
        {
            _timer += Time.deltaTime;
            MoveToTarget();
        }

        private void MoveToTarget()
        {
            
            if (_target == null)
            {
                Destroy(gameObject);
                return;
            }
            tempVector = _target.position;
            
            transform.position = Vector3.MoveTowards(transform.position, tempVector, _timer);
            transform.LookAt(tempVector);
            if (_timer >= 1)
            {
              
                if (_iDamage != null)
                {
                    _iDamage.GetHit(_damage);
                }
                Destroy(gameObject);
            }
        }

        public void Initialize(IDamagable unitDamagable, float damage)
        {
            _damage = damage;
            if (unitDamagable == null)
            {
                Destroy(gameObject);
                return;
            }

            if (unitDamagable.IsDead) return;
            _target = unitDamagable.damagableTransform;
            tempVector = unitDamagable.damagableTransform.position;
            _iDamage = unitDamagable;
        }
    }
