using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

    public class TavernLogic : Building
    {
        [SerializeField] private GameObject popUp;

        /*private void Start()
        {
            /*if (!isUnlocked)
            {
                pricePanel.SetPrice(upgradeStat.UnlockPrice);
            }#1#
        }*/

        public override void Interact()
        {
            base.Interact();
            if (isUnlocked)
            {
                filler.StartInteract(() =>
                {
                    PopUpMenu.Instance.OpenPopUp(popUp);
                });
            }
        }
        
    }
