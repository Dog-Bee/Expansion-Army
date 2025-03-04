using System;
using System.Collections;
using MoreMountains.NiceVibrations;
using UnityEngine;
using UnityEngine.UI;

    public class TestFiller : MonoBehaviour
    {
        private Action _fillAction;
        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponentInChildren<Slider>();
        }

        public void StartInteract(Action fillAction)
        {
            if (!_slider.gameObject.activeSelf) return;
            
            _fillAction = fillAction;
            StartCoroutine(Fill());
        }

        public void StopInteract()
        {
            StopAllCoroutines();
            _slider.value = 0;
        }

        IEnumerator Fill()
        {
            while (_slider.value<_slider.maxValue)
            {
                _slider.value += Time.deltaTime;
                yield return null;
            }
            _fillAction?.Invoke();
            MMVibrationManager.Haptic(HapticTypes.MediumImpact);
            _slider.value = 0;
        }
    }