using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


    public class PopUpUpgrade : PopUpLogic
    {
        
        [SerializeField] private Button upgradeDamageButton;
        [SerializeField] private Button upgradeHealthButton;
        [SerializeField] private Button upgradeSpeedButton;
        [SerializeField] private Button upgradeRegenButton;

        [Header("ButtonPrice")]
        [SerializeField] private TextMeshProUGUI damagePriceText;
        [SerializeField] private TextMeshProUGUI healthPriceText;
        [SerializeField] private TextMeshProUGUI speedPriceText;
        [SerializeField] private TextMeshProUGUI regenPriceText;
        
        [Header("UpgradeSO")] 
        [SerializeField] private StatUpgradeSO damageUpgrade;
        [SerializeField] private StatUpgradeSO healthUpgrade;
        [SerializeField] private StatUpgradeSO speedUpgrade;
        [SerializeField] private StatUpgradeSO regenUpgrade;

        [Header("UpgradeTexts")]
        [SerializeField] private TextMeshProUGUI currentAttackLevel;
        [SerializeField] private TextMeshProUGUI attackLevel;

        [SerializeField] private TextMeshProUGUI currentHealthLevel;
        [SerializeField] private TextMeshProUGUI healthLevel;

        [SerializeField] private TextMeshProUGUI currentSpeedLevel;
        [SerializeField] private TextMeshProUGUI speedLevel;
        
        [SerializeField] private TextMeshProUGUI currentRegenLevel;
        [SerializeField] private TextMeshProUGUI regenLevel;

        private bool isUpdate;

        private void Awake()
        {
            Initialize();
        }

       protected new void Start()
       {
           base.Start();
            AssignButton();
        }

        protected new void AssignButton()
        {
            base.AssignButton();
            
            closeButton.onClick.AddListener(() =>
            {
                Player.UpdateStatsEvent?.Invoke(isUpdate);
            });
            
            upgradeDamageButton.onClick.AddListener(() =>
            {
                SavableValue<int> damageLevel = new SavableValue<int>("DamageLevel");
                CurrencyManager.Instance.SpendCurrency(ResourceNameEnum.Gold,(uint)damageUpgrade.Upgrades[damageLevel.Value+1].Cost);
                damageLevel.Value++;
                InitializeDamageButton();
                isUpdate = true;
            });
            upgradeHealthButton.onClick.AddListener(() =>
            {
                SavableValue<int> saveHealthLevel = new SavableValue<int>("HealthLevel");
                CurrencyManager.Instance.SpendCurrency(ResourceNameEnum.Gold,(uint)healthUpgrade.Upgrades[saveHealthLevel.Value+1].Cost);
                saveHealthLevel.Value++;
                InitializeHealthButton();
                isUpdate = true;

            });
            upgradeSpeedButton.onClick.AddListener(() =>
            {
                SavableValue<int> speedLevel = new SavableValue<int>("SpeedLevel");
                CurrencyManager.Instance.SpendCurrency(ResourceNameEnum.Gold,(uint)speedUpgrade.Upgrades[speedLevel.Value+1].Cost);
                speedLevel.Value++;
                InitializeSpeedButton();
                isUpdate = true;

            });
            upgradeRegenButton.onClick.AddListener(() =>
            {
                SavableValue<int> saveRegenLevel = new SavableValue<int>("HealthRegenLevel");
                CurrencyManager.Instance.SpendCurrency(ResourceNameEnum.Gold,(uint)regenUpgrade.Upgrades[saveRegenLevel.Value+1].Cost);
                saveRegenLevel.Value++;
                isUpdate = true;
                InitializeRegenButton();

            });
           

        }

        protected new void Initialize()
        {
            base.Initialize();
            InitializeDamageButton();
            InitializeHealthButton();
            InitializeSpeedButton();
            InitializeRegenButton();
        }

        private void InitializeDamageButton()
        {
            if (upgradeDamageButton == null) return;
            SavableValue<int> damageLevel = new SavableValue<int>("DamageLevel");


            currentAttackLevel.text = "<color=#7998CC>"+damageUpgrade.Upgrades[damageLevel.Value].UpgradeValue + " <sprite=0> ";
            attackLevel.text = "Level "+(damageLevel.Value + 1);

            if (damageLevel.Value>=damageUpgrade.Upgrades.Count-1)
            {
                currentAttackLevel.text += "<color=#3DAA43>MAX";
                Instantiate(inactiveButton, upgradeDamageButton.transform.parent);
                upgradeDamageButton.gameObject.SetActive(false);
                Destroy(upgradeDamageButton);
                return;
            }
            else
            {
                currentAttackLevel.text +="<color=#3DAA43>"+damageUpgrade.Upgrades[damageLevel.Value + 1].UpgradeValue;
                damagePriceText.text =damageUpgrade.Upgrades[damageLevel.Value + 1].Cost.ToString();
            }

            if (CurrencyManager.Instance.IsEnoughCurrency(ResourceNameEnum.Gold, (uint)damageUpgrade.Upgrades[damageLevel.Value + 1].Cost))
            {
                upgradeDamageButton.interactable = true;
                upgradeDamageButton.image.sprite = activeSprite;
            }
            else
            {
                upgradeDamageButton.interactable = false;
                upgradeDamageButton.image.sprite = inactiveSprite;
            }
        }
        
        private void InitializeRegenButton()
        {
            if (upgradeRegenButton == null) return;
            SavableValue<int> saveRegenLevel = new SavableValue<int>("HealthRegenLevel");


            currentRegenLevel.text = "<color=#7998CC>"+regenUpgrade.Upgrades[saveRegenLevel.Value].UpgradeValue + " <sprite=0> ";
            regenLevel.text = "Level "+(saveRegenLevel.Value + 1);

            if (saveRegenLevel.Value>=regenUpgrade.Upgrades.Count-1)
            {
                currentRegenLevel.text += "<color=#3DAA43>MAX";
                Instantiate(inactiveButton, upgradeRegenButton.transform.parent);
                upgradeRegenButton.gameObject.SetActive(false);
                Destroy(upgradeRegenButton);
                return;
            }
            else
            {
                currentRegenLevel.text +="<color=#3DAA43>"+regenUpgrade.Upgrades[saveRegenLevel.Value + 1].UpgradeValue;
                regenPriceText.text = regenUpgrade.Upgrades[saveRegenLevel.Value + 1].Cost.ToString();
            }

            if (CurrencyManager.Instance.IsEnoughCurrency(ResourceNameEnum.Gold, (uint)regenUpgrade.Upgrades[saveRegenLevel.Value + 1].Cost))
            {
                upgradeRegenButton.interactable = true;
                upgradeRegenButton.image.sprite = activeSprite;
            }
            else
            {
                upgradeRegenButton.interactable = false;
                upgradeRegenButton.image.sprite = inactiveSprite;
            }
        }
        
        
        private void InitializeHealthButton()
        {
            if (upgradeHealthButton == null) return;
            SavableValue<int> saveHealthLevel = new SavableValue<int>("HealthLevel");


            currentHealthLevel.text ="<color=#7998CC>"+ healthUpgrade.Upgrades[saveHealthLevel.Value].UpgradeValue + " <sprite=0> ";
            healthLevel.text = "Level "+(saveHealthLevel.Value + 1);

            if (saveHealthLevel.Value>=healthUpgrade.Upgrades.Count-1)
            {
                currentHealthLevel.text += "<<color=#3DAA43>>MAX";
                Instantiate(inactiveButton, upgradeHealthButton.transform.parent);
                upgradeHealthButton.gameObject.SetActive(false);
                Destroy(upgradeHealthButton);
                return;
            }
            else
            {
                currentHealthLevel.text +="<color=#3DAA43>"+ healthUpgrade.Upgrades[saveHealthLevel.Value + 1].UpgradeValue;
                healthPriceText.text = healthUpgrade.Upgrades[saveHealthLevel.Value + 1].Cost.ToString();
            }

            if (CurrencyManager.Instance.IsEnoughCurrency(ResourceNameEnum.Gold, (uint)healthUpgrade.Upgrades[saveHealthLevel.Value + 1].Cost))
            {
                upgradeHealthButton.interactable = true;
                upgradeHealthButton.image.sprite = activeSprite;
            }
            else
            {
                upgradeHealthButton.interactable = false;
                upgradeHealthButton.image.sprite = inactiveSprite;
            }
        }
        private void InitializeSpeedButton()
        {
            if(upgradeSpeedButton == null) return;
            SavableValue<int> speedLevel = new SavableValue<int>("SpeedLevel");



            currentSpeedLevel.text = "<color=#7998CC>"+speedUpgrade.Upgrades[speedLevel.Value].UpgradeValue + " <sprite=0> ";
            this.speedLevel.text ="Level "+ (speedLevel.Value + 1);

            if (speedLevel.Value>=speedUpgrade.Upgrades.Count-1)
            {
                currentSpeedLevel.text += "<color=#3DAA43>MAX";
                Instantiate(inactiveButton, upgradeSpeedButton.transform.parent);
                upgradeSpeedButton.gameObject.SetActive(false);
                Destroy(upgradeSpeedButton);
                return;
            }
            else
            {
                currentSpeedLevel.text +="<color=#3DAA43>"+speedUpgrade.Upgrades[speedLevel.Value + 1].UpgradeValue;
                speedPriceText.text = speedUpgrade.Upgrades[speedLevel.Value + 1].Cost.ToString();
            }

            if (CurrencyManager.Instance.IsEnoughCurrency(ResourceNameEnum.Gold, (uint)speedUpgrade.Upgrades[speedLevel.Value + 1].Cost))
            {
                upgradeSpeedButton.interactable = true;
                upgradeSpeedButton.image.sprite = activeSprite;
            }
            else
            {
                upgradeSpeedButton.interactable = false;
                upgradeSpeedButton.image.sprite = inactiveSprite;
            }
        }
    }
