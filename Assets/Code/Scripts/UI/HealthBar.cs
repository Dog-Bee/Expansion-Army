using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider hpBar;
        [SerializeField] private TextMeshProUGUI hpText;
        [SerializeField] private Unit _unit;

        private void Awake()
        {
            hpBar.maxValue = _unit.HP;
        }
        
        

        private void LateUpdate()
        {
            hpText.text = _unit.HP.ToString();
            hpBar.value = _unit.HP;
        }

        public void UpdateHealthBar()
        {
            hpBar.maxValue = _unit.HP;
            hpText.text = _unit.HP.ToString();
            hpBar.value = _unit.HP;
        }
        
    }
