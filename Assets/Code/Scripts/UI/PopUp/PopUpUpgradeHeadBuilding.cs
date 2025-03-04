using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

    public class PopUpUpgradeHeadBuilding : PopUpLogic
    {
        public static Action OnHeadBuildingUpgrade;
        
        [SerializeField] private Button upgradeZombieButton;
        [SerializeField] private Button upgradeSkeletonButton;
        [SerializeField] private Button upgradeGhostButton;


        [Header("ButtonPrice")]
        [SerializeField] private TextMeshProUGUI zombiePriceText;
        [SerializeField] private TextMeshProUGUI skeletonPriceText;
        [SerializeField] private TextMeshProUGUI ghostPriceText;
        
        [Header("UpgradeSO")] 
        [SerializeField] private StatUpgradeSO zombieUpgrade;
        [SerializeField] private StatUpgradeSO skeletonUpgrade;
        [SerializeField] private StatUpgradeSO ghostUpgrade;

        [Header("UpgradeTexts")]
        [SerializeField] private TextMeshProUGUI currentZombieLevel;
        [SerializeField] private TextMeshProUGUI zombieLevelText;

        [SerializeField] private TextMeshProUGUI currentSkeletonLevel;
        [SerializeField] private TextMeshProUGUI skeletonLevelText;

        [SerializeField] private TextMeshProUGUI currentGhostLevel;
        [SerializeField] private TextMeshProUGUI ghostLevelText;

        private void Awake()
        {
            Initialize();
        }

        void Start()
        {
            transform.DOScale(Vector3.one, .5f);
            //OnHeadBuildingUpgrade?.Invoke();
            AssignButton();
        }

        protected new void AssignButton()
        {
            base.AssignButton();
            
            closeButton.onClick.AddListener(() =>
            {
                OnHeadBuildingUpgrade?.Invoke();
            });
            upgradeZombieButton.onClick.AddListener(() =>
            {
                SavableValue<int> zombieLevel = new SavableValue<int>("ZombieLevel");
                CurrencyManager.Instance.SpendCurrency(ResourceNameEnum.Gold,(uint)zombieUpgrade.Upgrades[zombieLevel.Value].Cost);
                zombieLevel.Value++;
                InitializeZombieButton();
            });
            upgradeSkeletonButton.onClick.AddListener(() =>
            {
                SavableValue<int> skeletonLevel = new SavableValue<int>("SkeletonLevel");
                CurrencyManager.Instance.SpendCurrency(ResourceNameEnum.Gold,(uint)zombieUpgrade.Upgrades[skeletonLevel.Value].Cost);
                skeletonLevel.Value++;
                InitializeSkeletonButton();

            });
            upgradeGhostButton.onClick.AddListener(() =>
            {
                SavableValue<int> ghostLevel = new SavableValue<int>("GhostLevel");
                CurrencyManager.Instance.SpendCurrency(ResourceNameEnum.Gold,(uint)zombieUpgrade.Upgrades[ghostLevel.Value].Cost);
                ghostLevel.Value++;
                InitializeGhostButton();

            });
           

        }

        protected new void Initialize()
        {
            base.Initialize();
            InitializeZombieButton();
            InitializeSkeletonButton();
            InitializeGhostButton();
        }

        private void InitializeZombieButton()
        {
            if (upgradeZombieButton == null) return;
            SavableValue<int> zombieLevel = new SavableValue<int>("ZombieLevel");


            currentZombieLevel.text = "<color=#7998CC>"+zombieUpgrade.Upgrades[zombieLevel.Value].UpgradeValue + " <sprite=0> ";
            zombieLevelText.text = "Level "+ (zombieLevel.Value +1);

            if (zombieLevel.Value>=zombieUpgrade.Upgrades.Count-1)
            {
                currentZombieLevel.text += "<color=#3DAA43>MAX";
                Instantiate(inactiveButton, upgradeZombieButton.transform.parent);
                upgradeZombieButton.gameObject.SetActive(false);
                Destroy(upgradeZombieButton);
                return;
            }
            else
            {
                currentZombieLevel.text +="<color=#3DAA43>"+zombieUpgrade.Upgrades[zombieLevel.Value + 1].UpgradeValue;
                zombiePriceText.text = zombieUpgrade.Upgrades[zombieLevel.Value + 1].Cost.ToString();
            }

            if (CurrencyManager.Instance.IsEnoughCurrency(ResourceNameEnum.Gold, (uint)zombieUpgrade.Upgrades[zombieLevel.Value + 1].Cost))
            {
                upgradeZombieButton.interactable = true;
                upgradeZombieButton.image.sprite = activeSprite;
            }
            else
            {
                upgradeZombieButton.interactable = false;
                upgradeZombieButton.image.sprite = inactiveSprite;
            }
        }
        
        private void InitializeSkeletonButton()
        {
            if (upgradeSkeletonButton == null) return;
            SavableValue<int> SkeletonLevel = new SavableValue<int>("SkeletonLevel");


            currentSkeletonLevel.text ="<color=#7998CC>"+skeletonUpgrade.Upgrades[SkeletonLevel.Value].UpgradeValue + " <sprite=0> ";
            skeletonLevelText.text ="Level "+  (SkeletonLevel.Value + 1);

            if (SkeletonLevel.Value>=skeletonUpgrade.Upgrades.Count-1)
            {
                currentSkeletonLevel.text += "<<color=#3DAA43>>MAX";
                Instantiate(inactiveButton, upgradeSkeletonButton.transform.parent);
                upgradeSkeletonButton.gameObject.SetActive(false);
                Destroy(upgradeSkeletonButton);
                return;
            }
            else
            {
                currentSkeletonLevel.text +="<color=#3DAA43>"+ skeletonUpgrade.Upgrades[SkeletonLevel.Value + 1].UpgradeValue;
                skeletonPriceText.text = skeletonUpgrade.Upgrades[SkeletonLevel.Value + 1].Cost.ToString();
            }

            if (CurrencyManager.Instance.IsEnoughCurrency(ResourceNameEnum.Gold, (uint)skeletonUpgrade.Upgrades[SkeletonLevel.Value + 1].Cost))
            {
                upgradeSkeletonButton.interactable = true;
                upgradeSkeletonButton.image.sprite = activeSprite;
            }
            else
            {
                upgradeSkeletonButton.interactable = false;
                upgradeSkeletonButton.image.sprite = inactiveSprite;
            }
        }
        private void InitializeGhostButton()
        {
            if(upgradeGhostButton == null) return;
            SavableValue<int> ghostLevel = new SavableValue<int>("GhostLevel");



            currentGhostLevel.text = "<color=#7998CC>"+ghostUpgrade.Upgrades[ghostLevel.Value].UpgradeValue + " <sprite=0> ";
            ghostLevelText.text ="Level "+  (ghostLevel.Value + 1);

            if (ghostLevel.Value>=ghostUpgrade.Upgrades.Count-1)
            {
                currentGhostLevel.text += "<color=#3DAA43>MAX";
                Instantiate(inactiveButton, upgradeGhostButton.transform.parent);
                upgradeGhostButton.gameObject.SetActive(false);
                Destroy(upgradeGhostButton);
                return;
            }
            else
            {
                currentGhostLevel.text +="<color=#3DAA43>"+ghostUpgrade.Upgrades[ghostLevel.Value + 1].UpgradeValue;
                ghostPriceText.text = ghostUpgrade.Upgrades[ghostLevel.Value + 1].Cost.ToString();
            }

            if (CurrencyManager.Instance.IsEnoughCurrency(ResourceNameEnum.Gold, (uint)ghostUpgrade.Upgrades[ghostLevel.Value + 1].Cost))
            {
                upgradeGhostButton.interactable = true;
                upgradeGhostButton.image.sprite = activeSprite;
            }
            else
            {
                upgradeGhostButton.interactable = false;
                upgradeGhostButton.image.sprite = inactiveSprite;
            }
        }
    }
