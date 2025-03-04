using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

    public class ResourceObject : MonoBehaviour,IInteractable
    {
        [SerializeField] private ResourceNameEnum _nameEnum;
        [SerializeField] private float xRange;
        [SerializeField] private float zRange;
        [SerializeField] private float duration;
        private uint _resourceCount;
        private BoxCollider _collider;

        private Sequence tempSequence;
        public void Initialize(uint resourceCount)
        {
            _collider = GetComponent<BoxCollider>();
            _resourceCount = 1;
            Vector3 endPosition = transform.position;
            endPosition.x += Random.Range(-xRange, xRange);
            endPosition.z += Random.Range(-zRange, zRange);
            tempSequence = transform.DOJump(endPosition, 2, 1, duration).OnComplete(()=>
            {
                _collider.enabled = true;
            });
            tempSequence.Play();
            
        }

        public void Interact()
        {
            DOTween.Kill(tempSequence);
            CurrencyManager.Instance.AddCurrency(_nameEnum,_resourceCount);
            Destroy(gameObject);
        }

        public void StopInteract()
        {
            
        }
    }