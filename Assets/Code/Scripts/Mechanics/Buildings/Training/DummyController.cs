using System.Collections;
using DG.Tweening;
using UnityEngine;

    public class DummyController : MonoBehaviour,IDamagable
    {
        [SerializeField] private float hp;
        [SerializeField] private float respawnDelay;
        [SerializeField] private ResourceDropSO resourceDropSO;

        private float _hp;
        private Animator _animator;
        public Transform damagableTransform => transform;
        public bool IsDead { get; set; }

        private void Awake()
        {
            _hp = hp;
            _animator = GetComponent<Animator>();
        }

        public void GetHit(float damage)
        {
            _hp -= damage;
            _hp = Mathf.Clamp(_hp, 0, hp);
            _animator.SetTrigger(_hp<=0? "Death" : "Hit");
            if (_hp <= 0)
            {
                IsDead = true;
                _animator.SetTrigger("Death");
                resourceDropSO.GetResource(transform.position);
                transform.DOMove(transform.position + Vector3.down * 5, .5f);
                StartCoroutine(RespawnDummy());
            }
            else
            {
                _animator.SetTrigger("Hit");
            }

        }

        IEnumerator RespawnDummy()
        {
            yield return new WaitForSeconds(respawnDelay);
            _hp = hp;
            _animator.SetTrigger("Respawn");
            IsDead = false;
            transform.DOMove(transform.position - Vector3.down * 5, .5f);


        }
    }
