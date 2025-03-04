
using UnityEngine;

    public class DefaultSpawner : Spawner
    {
        protected new void Start()
        {
            base.Start();
            spawnPlace.IsUnlocked = isUnlocked;
            spawnObject = unitUpgrades.UpgradeSO[tempLevel].GetComponent<Unit>();
            /*spawnerObject.SetActive(isUnlocked);
            pricePanel.SetPrice(isUnlocked?  upgradeStat.ResourceLevels[tempLevel].ResourceHolders : upgradeStat.UnlockPrice);*/
        }
        
        protected new void Update()
        {
            base.Update();
            if (GameManager.Instance.State!= GameState.Game)
                CheckSpawn();
        }
        public override void Interact()
        {
            base.Interact();
            if (!isUnlocked)
            {
                spawnObject = unitUpgrades.UpgradeSO[tempLevel].GetComponent<Unit>();
               UnlockSpawn();
            }
            else
            {
                if (_levelCount.GetLevel(LevelType.BattleLevel) >= upgradeStat.ResourceLevels[tempLevel].level && CheckPrice(upgradeStat.ResourceLevels[tempLevel].ResourceHolders))
                {
                    filler.StartInteract(() =>
                    {
                        Upgrade();
                        spawnObject = unitUpgrades.UpgradeSO[tempLevel].GetComponent<Unit>();
                    });
                }
            }
        }
        
    }