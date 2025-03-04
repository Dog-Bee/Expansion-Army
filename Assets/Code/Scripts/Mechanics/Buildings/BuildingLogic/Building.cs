using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

    public class Building : MonoBehaviour,IInteractable
    {
        [SerializeField] protected UpgradeBuildingSO upgradeStat;
        [SerializeField] protected PricePanel pricePanel;
        [SerializeField] protected Transform buildSpawnPlace;
        [SerializeField] private ParticleSystem buildParticle;

        private BoxCollider _boxCollider;
        protected int tempLevel;
        protected bool isUnlocked;
        protected LevelCount _levelCount;
        protected TestFiller filler;
       private GameObject _tempBuilding;
       protected SavableValue<bool> saveUnlock;
       protected SavableValue<int> saveLevel;

        protected GameObject tempBuilding
        {
            get => _tempBuilding;
            set
            {
                if (_tempBuilding!=null)
                {
                    Destroy(_tempBuilding);
                }

                
                _tempBuilding = Instantiate(value,buildSpawnPlace.position + Vector3.down * 10,buildSpawnPlace.transform.rotation);
                Instantiate(buildParticle, buildSpawnPlace);
                Vector3 tempVector = _tempBuilding.transform.localScale;
                tempVector.y -= .2f;
                _tempBuilding.transform.localScale = tempVector;
                _tempBuilding.transform.parent = buildSpawnPlace;
                _tempBuilding.transform.DOMove(buildSpawnPlace.position, .5f).SetEase(Ease.OutBack);
                Vector3 targetVector = tempVector;
                targetVector.y += .2f;
                _tempBuilding.transform.DOScale(targetVector, 1f).SetEase(Ease.OutBounce);
               
            }
        }


        protected void Awake()
        {
            saveLevel = new SavableValue<int>(transform.name + "TempLevel");
            saveUnlock = new SavableValue<bool>(transform.name + "Unlock");
            isUnlocked = saveUnlock.Value;
            tempLevel = saveLevel.Value;
            _levelCount = new LevelCount();
            
            filler = GetComponentInChildren<TestFiller>();
            if (_levelCount.GetLevel(LevelType.BattleLevel) < upgradeStat.UnlockLevel)
            {
                pricePanel.SetLock(upgradeStat.UnlockLevel);
            }
            else
            {
                if (!isUnlocked)
                {
                    pricePanel.SetPrice(upgradeStat.UnlockPrice,false);
                }
            }
        }

        protected void OnEnable()
        {
            _boxCollider = GetComponent<BoxCollider>();
            _boxCollider.enabled = false;
            UILevelLogic.LevelUpEvent += OnLevelUp;
        }

        protected void OnDisable()
        {
            UILevelLogic.LevelUpEvent -= OnLevelUp;
        }

        protected void Start()
        {
            StartInitialize();
        }

        protected void StartInitialize()
        {
            if (isUnlocked)
            {
                if (tempLevel==0)
                {
                    _tempBuilding = Instantiate(upgradeStat.DefaultBuilding,buildSpawnPlace.position , buildSpawnPlace.transform.rotation);
                }
                else
                {
                    _tempBuilding = Instantiate(upgradeStat.ResourceLevels[tempLevel-1].buildObject,buildSpawnPlace.position, buildSpawnPlace.transform.rotation);
                }

                if (upgradeStat.ResourceLevels.Count == 0)
                {
                    pricePanel.SetPrice(null,isUnlocked);
                }
                else
                {
                    if (tempLevel >= upgradeStat.ResourceLevels.Count)
                    {
                        pricePanel.gameObject.SetActive(false);
                        _boxCollider.enabled = false;
                    }
                    else
                    {
                        pricePanel.SetPrice(upgradeStat.ResourceLevels[tempLevel].ResourceHolders);
                    }
                }
            }
        }

        protected void Unlock()
        {
            if (CheckPrice(upgradeStat.UnlockPrice))
            {
                SpendResources(upgradeStat.UnlockPrice);
                isUnlocked = true;
                tempBuilding = upgradeStat.DefaultBuilding;
                saveUnlock.Value = isUnlocked;
                if (upgradeStat.ResourceLevels.Count != 0)
                {
                    pricePanel.SetPrice(upgradeStat.ResourceLevels[tempLevel].ResourceHolders);
                }
                else
                {
                    pricePanel.SetPrice(null,isUnlocked);
                }
            }
        }

        protected void Upgrade()
        {
            if (tempLevel >= upgradeStat.ResourceLevels.Count)
            {
                pricePanel.gameObject.SetActive(false);
                _boxCollider.enabled = false;
                return;
            }
            upgradeStat.ResourceLevels[tempLevel].ResourceHolders.ForEach(resource =>
                CurrencyManager.Instance.SpendCurrency(resource.ResourceData.ResourceNameEnum, resource.ResourceCount));
            tempBuilding = upgradeStat.ResourceLevels[tempLevel].buildObject;
            tempLevel++;
            saveLevel.Value = tempLevel;
            if (tempLevel >= upgradeStat.ResourceLevels.Count)
            {
                pricePanel.gameObject.SetActive(false);
                _boxCollider.enabled = false;
                return;
            }
            pricePanel.SetPrice(upgradeStat.ResourceLevels[tempLevel].ResourceHolders);
           

        }

        protected bool CheckPrice(List<ResourceHolder> resourceList)
        {
            return resourceList.All(resource => CurrencyManager.Instance.IsEnoughCurrency(
                resource.ResourceData.ResourceNameEnum,
                resource.ResourceCount));
        }

        protected void SpendResources(List<ResourceHolder> resourceList)
        {
            resourceList.ForEach(resource =>
                CurrencyManager.Instance.SpendCurrency(resource.ResourceData.ResourceNameEnum, resource.ResourceCount));
        }

        protected void OnLevelUp()
        {
            if (new LevelCount().GetLevel(LevelType.BattleLevel) < upgradeStat.UnlockLevel) return;
            pricePanel.gameObject.SetActive(true);
            if (!isUnlocked)
            {
                pricePanel.SetPrice(upgradeStat.UnlockPrice,false);
            }
            _boxCollider.enabled = true;
        }

        public virtual void Interact()
        {
            if (!isUnlocked)
            {
                if (_levelCount.GetLevel(LevelType.BattleLevel) >= upgradeStat.UnlockLevel && CheckPrice(upgradeStat.UnlockPrice))
                {
                    filler.StartInteract(() =>
                    {
                        Unlock();
                    });
                }
            }
        }

        public virtual void StopInteract()
        {
            filler.StopInteract();
        }
    }