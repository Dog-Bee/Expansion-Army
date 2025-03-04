using System;
using System.Collections.Generic;

	public class CurrencyManager : PersistentSingleton<CurrencyManager>
	{
		public static Action MoneyChangedEvent;

		private SavableValue<uint> _currency;
		private Dictionary<ResourceNameEnum, SavableValue<uint>> resourceDict = new ();

		protected override void Awake()
		{
			base.Awake();
		}
		
		private void Start()
		{
			foreach (ResourceNameEnum name in Enum.GetValues(typeof(ResourceNameEnum)))
			{
				resourceDict.Add(name,
					name == ResourceNameEnum.Gold
						? new SavableValue<uint>(name.ToString(), 50)
						: new SavableValue<uint>(name.ToString()));
			}
			MoneyChangedEvent?.Invoke();
		}

		public bool IsEnoughCurrency(ResourceNameEnum nameEnum ,uint amount)
		{
			return resourceDict[nameEnum].Value >= amount;
		}

		public void AddCurrency(ResourceNameEnum nameEnum,uint amount)
		{
			resourceDict[nameEnum].Value += amount;
			MoneyChangedEvent?.Invoke();
		}

		public uint GetCurrency(ResourceNameEnum nameEnum)
		{
			
			return resourceDict[nameEnum].Value;
		}

		public void SpendCurrency(ResourceNameEnum nameEnum,uint amount)
		{
			if (resourceDict[nameEnum].Value < amount) return;
			resourceDict[nameEnum].Value -= amount;
			MoneyChangedEvent?.Invoke();
		}
		
		private void InvokeAction()
		{
			MoneyChangedEvent.Invoke();
		}
	}
