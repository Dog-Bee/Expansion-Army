using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEditor;
using UnityEngine;

    public class Fountain : Building
    {
        [NonReorderable]
        [SerializeField] private List<ResourceHolder> _resourceHolders;
        [SerializeField] private ResourceObject goldResource;
        [SerializeField] private int resourceCount;
        [SerializeField] private int dropChance;

        private bool isInteracting;
        private float timer = 1f;

        private new void Start()
        {
            base.Start();
            if (isUnlocked)
            {
                pricePanel.SetPrice(_resourceHolders);
            }
        }

        private void Update()
        {
            timer -= Time.deltaTime;
            if (isInteracting && timer<=0)
            {
                timer = 1;
                Casino();
            }
        }
        
        private void Casino()
        {
            if (CurrencyManager.Instance.IsEnoughCurrency(ResourceNameEnum.Gold, _resourceHolders[0].ResourceCount))
            {
                filler.StartInteract(()=>
                {
                    CurrencyManager.Instance.SpendCurrency(ResourceNameEnum.Gold,  _resourceHolders[0].ResourceCount);
                    if (Random.Range(0,100)<=dropChance)
                    {
                        for (int i = 0; i < resourceCount; i++)
                        {
                            Instantiate(goldResource, buildSpawnPlace.transform.position, Quaternion.identity).Initialize(1);
                        }
                    }
                });
            }
        }

        public override void Interact()
        {
            base.Interact();
            if (!isUnlocked)
            {
                StartCoroutine(InteractCoroutine());
            }
            else
            {
                isInteracting = true;
            }
        }

        public override void StopInteract()
        {
            base.StopInteract();
            isInteracting = false;
        }


        IEnumerator InteractCoroutine()
        {
            yield return new WaitForSeconds(1.1f);
            if (isUnlocked)
            {
                pricePanel.SetPrice(_resourceHolders);
                Interact();
            }
            
        }
    }