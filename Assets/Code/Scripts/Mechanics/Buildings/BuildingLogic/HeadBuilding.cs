using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class HeadBuilding : Building
    {
        
        [SerializeField] private GameObject popUp; 
        
        protected new void OnEnable()
        {
            base.OnEnable();
        }

        protected new void OnDisable()
        {
            base.OnDisable();
        }
        
        public override void Interact()
        {
            base.Interact();

            if (!isUnlocked) return;
            if (isUnlocked)
            {
                filler.StartInteract(() =>
                {
                    PopUpMenu.Instance.OpenPopUp(popUp);
                });
            }
        }
        
    }
