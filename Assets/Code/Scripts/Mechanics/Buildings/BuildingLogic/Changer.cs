using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

    public class Changer : Building
    {
        [NonReorderable]
        [SerializeField] private List<ResourceHolder> resourceToChange;
        [SerializeField] private ResourceDropSO resourceToGet;

        private bool isInteracting;
        private float timer = 1f;

        private new void Start()
        {
            base.Start();
            if (isUnlocked)
            {
                pricePanel.SetPrice(resourceToChange,isUnlocked);
            }
        }
        private void Update()
        {
            timer -= Time.deltaTime;
            if (isInteracting && timer<=0)
            {
                timer = 1;
                Change();
            }
        }

        private void Change()
        {
            if (CurrencyManager.Instance.IsEnoughCurrency(resourceToChange[0].ResourceData.ResourceNameEnum, resourceToChange[0].ResourceCount))
            {
                filler.StartInteract(() =>
                {
                    CurrencyManager.Instance.SpendCurrency(resourceToChange[0].ResourceData.ResourceNameEnum,resourceToChange[0].ResourceCount);
                    resourceToGet.GetResource(pricePanel.transform.position);
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
            if (isUnlocked)
            {
                pricePanel.SetPrice(resourceToChange,isUnlocked);
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
            Interact();
        }
    }
