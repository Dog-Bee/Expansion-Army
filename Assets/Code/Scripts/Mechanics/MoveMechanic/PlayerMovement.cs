using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class PlayerMovement : BaseMovement
    {
        [SerializeField] private StatUpgradeSO statSpeedSo;
        [SerializeField]private float moveSpeed;
        

        private void Awake()
        {
            OnPopUpClose();
        }

        private void OnEnable()
        {
            PopUpMenu.PopUpCloseEvent += OnPopUpClose;
        }

        private void OnDisable()
        {
            PopUpMenu.PopUpCloseEvent -= OnPopUpClose;
        }

        public override void Move(Vector3 destination)
        {
            /*transform.LookAt(transform.position+destination);*/
            transform.Translate(destination*moveSpeed*Time.deltaTime,Space.World);
        }

        private void OnPopUpClose()
        {
            SavableValue<int> speedLevel = new SavableValue<int>("SpeedLevel");
            moveSpeed = statSpeedSo.Upgrades[speedLevel.Value].UpgradeValue;
        }
    }
