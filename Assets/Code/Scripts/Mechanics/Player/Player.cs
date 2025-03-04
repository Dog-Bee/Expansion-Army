using System;
using System.Collections;
using System.Threading;
using UnityEngine;

    [RequireComponent(typeof(TouchController))]
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(Melee))]
    [RequireComponent(typeof(Animator))]

    public class Player : UnitStateMachine
    {
        public static Action<bool> UpdateStatsEvent; 
        
        
        [SerializeField] private StatUpgradeSO healthUpgrade;
        [SerializeField] private StatUpgradeSO damageUpgrade;
        [SerializeField] private StatUpgradeSO healthRegenUpgrade;
        [SerializeField] private HealthBar healthBar;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private ParticleSystem lvlUpParticle; 

        private TouchController _touchController;


        protected new void Awake()
        {
            base.Awake();
            _touchController = GetComponent<TouchController>();
        }

        private void OnEnable()
        {
            BattleFieldManager.LevelFinishEvent += OnLevelFinished;
            UpdateStatsEvent += UpdatePlayerStat;
        }

        private void OnDisable()
        {
            BattleFieldManager.LevelFinishEvent -= OnLevelFinished;
            UpdateStatsEvent -= UpdatePlayerStat;

        }

        private void Start()
        {
            UnitBattleManager.Instance.AddFriendly(unit);
            _currentState = new BattlePlayer(this, _touchController);
            UpdatePlayerStat(false);

        }

        protected override void ChangeState()
        {
            if (unit.HP <= 0)
            {
                if(_currentState!=null)
                _currentState =  new DeadState(this,OnDeath);
                
            }
            else
            {
                _currentState = new BattlePlayer(this, _touchController);
            }
        }
        


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Damagable"))
            {
                unit.EnemyUnit = other.GetComponent<IDamagable>();
                return;
            }
            IInteractable action = other.gameObject.GetComponent<IInteractable>();
            action.Interact();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Damagable"))
            {
                unit.FindEnemy(UnitBattleManager.Instance.EnemyUnitList);
                return;
            }
            IInteractable action = other.gameObject.GetComponent<IInteractable>();
            action.StopInteract();
        }

        protected new void OnDeath()
        {
            base.OnDeath();
            StartCoroutine(DeathCoroutine());
        }

        IEnumerator DeathCoroutine()
        {
            _currentState = null;
            yield return new WaitForSeconds(2f);
            transform.position = spawnPoint.position;
            yield return new WaitForSeconds(1f);
            UnitBattleManager.Instance.RemoveFriendly(unit);
            UpdatePlayerStat(false);
            yield return new WaitForSeconds(1f);
            _currentState = new BattlePlayer(this, _touchController);
            UnitBattleManager.Instance.AddFriendly(unit);
        }

        private void OnLevelFinished()
        {
            transform.position = spawnPoint.position;
        }

        private void UpdatePlayerStat(bool isUpdated)
        {
            if (isUpdated)
            {
                lvlUpParticle.Play();
            }
            
            
            SavableValue<int> damageLevel = new SavableValue<int>("DamageLevel");
            SavableValue<int> saveHealthLevel = new SavableValue<int>("HealthLevel");
            SavableValue<int> saveRegenLevel = new SavableValue<int>("HealthRegenLevel");
            
            unit.HP = healthUpgrade.Upgrades[saveHealthLevel.Value].UpgradeValue;
            unit.Damage = damageUpgrade.Upgrades[damageLevel.Value].UpgradeValue;
            unit.HealthRegen = healthRegenUpgrade.Upgrades[saveRegenLevel.Value].UpgradeValue;
            healthBar.UpdateHealthBar();

        }
    }