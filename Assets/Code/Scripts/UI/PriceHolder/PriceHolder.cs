using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

    public class PriceHolder : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private Image resourceImage;

        public void Initialize(ResourceHolder resourceHolder)
        {
            priceText.text = resourceHolder.ResourceCount.ToString();
            resourceImage.sprite = resourceHolder.ResourceData.ResourceSprite;
        }
    }
