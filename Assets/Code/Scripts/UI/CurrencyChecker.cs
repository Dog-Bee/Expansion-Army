using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

    public class CurrencyChecker : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI coinText;
        [SerializeField] private TextMeshProUGUI brainText;
        [SerializeField] private TextMeshProUGUI boneText;
        [SerializeField] private TextMeshProUGUI dustText;

        private void OnEnable()
        {
            CurrencyManager.MoneyChangedEvent += OnMoneyChanged;
        }

        private void OnDisable()
        {
            CurrencyManager.MoneyChangedEvent -= OnMoneyChanged;
        }

        private void OnMoneyChanged()
        {
            coinText.text = CurrencyManager.Instance.GetCurrency(ResourceNameEnum.Gold).ToString();
            brainText.text = CurrencyManager.Instance.GetCurrency(ResourceNameEnum.Brain).ToString();
            boneText.text = CurrencyManager.Instance.GetCurrency(ResourceNameEnum.Bone).ToString();
            dustText.text = CurrencyManager.Instance.GetCurrency(ResourceNameEnum.Dust).ToString();
        }
    }
