using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


    [Serializable]
    public sealed class SavableValue<T>
    {
        public event Action OnChanged = () => { };
        
        private readonly string playerPrefsPath;
        private T value;


        public T Value
        {
            get => value;
            set
            {
                PrevValue = this.value;
                this.value = value;
                SaveToPrefs();
                OnChanged.Invoke();
            }
        }

        public T PrevValue { get; private set; }

        public SavableValue(string playerPrefsPath, T defaultValue = default(T))
        {
            if (string.IsNullOrEmpty(playerPrefsPath))
            {
                throw new Exception("Empty playerPrefsPath in savableValue");
            }

            this.playerPrefsPath = playerPrefsPath;

            value = defaultValue;
            PrevValue = defaultValue;

            LoadFromPrefs();
        }

        private void LoadFromPrefs()
        {
            if (!PlayerPrefs.HasKey(playerPrefsPath))
            {
                SaveToPrefs();
                return;
            }

            var stringToDeserialize = PlayerPrefs.GetString(playerPrefsPath, "");

            value = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(stringToDeserialize);
        }

        public void SaveToPrefs()
        {
            var stringToSave = Newtonsoft.Json.JsonConvert.SerializeObject(value);
            
            PlayerPrefs.SetString(playerPrefsPath, stringToSave);
        }
    }