using System.Collections.Generic;
using UnityEngine;

    public class BossSpawn : Spawner
    {
        [SerializeField] private List<ResourceHolder> resourceList;

        protected new void OnEnable()
        {
            base.OnEnable();
        }

        protected new void OnDisable()
        {
            base.OnDisable();
        }

        protected new void Start()
        {
            base.Start();
            if (isUnlocked)
            {
                pricePanel.SetPrice(resourceList);
            }
            
        }


        public override void Interact()
        {
            base.Interact();
            if (!isUnlocked)
            {
                UnlockSpawn();
            }
            if (isUnlocked)
            {
                if (GameManager.Instance.State != GameState.Game)
                {
                    if (CheckPrice(resourceList)&& spawnPlace.GetSpawnPlace())
                    {
                        filler.StartInteract(() =>
                        {
                            SpendResources(resourceList);
                                CheckSpawn();
                            
                        });
                    }
                    
                }
            }

            
        }
    }