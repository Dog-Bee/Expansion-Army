using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

    public class PopUpLogic : MonoBehaviour
    {
        [SerializeField] protected uint adCurrencyCount;

        [SerializeField] protected Button closeButton;
        [SerializeField] protected Button adGetMoneyButton;
        [SerializeField] protected Transform scaleObj;

        [Header("InactiveButton")] 
        [SerializeField] protected Sprite inactiveSprite;
        [SerializeField] protected Sprite activeSprite;

        [SerializeField] protected GameObject inactiveButton;

        protected void Start()
        {
            scaleObj.DOScale(Vector3.one, .5f);
        }

        protected void AssignButton()
        {
            closeButton.onClick.AddListener(() =>
            {
                scaleObj.DOScale(Vector3.zero, .5f).OnComplete(() =>
                {
                    PopUpMenu.Instance.ClosePopUp(gameObject);
                    Destroy(gameObject);
                });
            });
            
            adGetMoneyButton.onClick.AddListener(() =>
            {
                CurrencyManager.Instance.AddCurrency(ResourceNameEnum.Gold, adCurrencyCount);
                Initialize();
            });
        }

        protected void Initialize()
        {
            
        }
    }
