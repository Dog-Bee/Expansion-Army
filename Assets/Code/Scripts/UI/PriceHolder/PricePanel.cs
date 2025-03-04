using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

    public class PricePanel : MonoBehaviour
    {
        [SerializeField] private PriceHolder priceHolder;
        [SerializeField] private Transform priceParent;
        [SerializeField] private GameObject levelBlock;
        [SerializeField] private Image interactImage;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private Sprite buildSprite;
        [SerializeField] private Sprite interactSprite;

        private List<PriceHolder> _priceHolders = new();


        public void SetPrice(List<ResourceHolder> resourceHolders=null , bool isUnlocked = true)
        {
            interactImage.sprite = isUnlocked ? interactSprite : buildSprite;   
            levelBlock.SetActive(false);
            priceParent.gameObject.SetActive(true);
            interactImage.gameObject.SetActive(true);
            if (resourceHolders == null)
            {
                DeactivatePopUp();
                return;
            }
            _priceHolders.ForEach(price=>Destroy(price.gameObject));
            _priceHolders.Clear();
            foreach (var resource in resourceHolders)
            {
                PriceHolder tempPrice = Instantiate(priceHolder, priceParent);
                tempPrice.Initialize(resource);
                _priceHolders.Add(tempPrice);
            }
        }

        public void SetLock(int level)
        {
            print("SetLock");
            priceParent.gameObject.SetActive(false);
            interactImage.gameObject.SetActive(false);
            levelBlock.SetActive(true);
            levelText.text = level.ToString();
        }

        public void DeactivatePopUp()
        {
            _priceHolders.ForEach(price=>Destroy(price.gameObject));
            priceParent.gameObject.SetActive(false);
            /*Destroy(priceParent.gameObject);*/
            _priceHolders.Clear();
        }
    }
