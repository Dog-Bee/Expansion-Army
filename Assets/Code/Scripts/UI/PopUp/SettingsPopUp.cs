using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MoreMountains.NiceVibrations;
using UnityEngine;
using UnityEngine.UI;

    public class SettingsPopUp : MonoBehaviour
    {
        [SerializeField] private Transform scaleObj;
        
        [Header("Sprites")]
        [SerializeField] private Sprite offButton;
        [SerializeField] private Sprite onButton;

        [Header("Buttons")]
        [SerializeField] private Button musicButton;
        [SerializeField] private Button soundButton;
        [SerializeField] private Button vibroButton;
        [SerializeField] private Button closeButton;
 

        private bool _musicStatus=true;
        private bool _soundStatus=true;
        private bool _vibroStatus=true;

        private SavableValue<bool> _saveVibro;
        private SavableValue<bool> _saveSound;
        private SavableValue<bool> _saveMusic;


        private void Awake()
        {
            _saveVibro = new SavableValue<bool>("Vibro", true);
            _saveSound = new SavableValue<bool>("Sound", true);
            _saveMusic = new SavableValue<bool>("Music", true);

            _vibroStatus = _saveVibro.Value;
            _soundStatus = _saveSound.Value;
            _musicStatus = _saveMusic.Value;
        }


        private void Start()
        {
            musicButton.image.sprite = _musicStatus ? onButton : offButton;
            soundButton.image.sprite = _soundStatus ? onButton : offButton;
            vibroButton.image.sprite = _vibroStatus ? onButton : offButton;
            
            MMVibrationManager.SetHapticsActive(_vibroStatus);

            scaleObj.DOScale(Vector3.one, .5f);
            

            InitializeButtons();
        }

        

        private void InitializeButtons()
        {
            closeButton.onClick.AddListener(() =>
            {
                scaleObj.DOScale(Vector3.zero, .5f).OnComplete(() =>
                {
                    PopUpMenu.Instance.ClosePopUp(gameObject);
                    Destroy(gameObject);
                });
            });
            musicButton.onClick.AddListener(() =>
            {
                _musicStatus = !_musicStatus;
                musicButton.image.sprite = _musicStatus ? onButton : offButton;
            });
            soundButton.onClick.AddListener(() =>
            {
                _soundStatus = !_soundStatus;
                soundButton.image.sprite = _soundStatus ? onButton : offButton;
            });
            vibroButton.onClick.AddListener(() =>
            {
                _vibroStatus = !_vibroStatus;
                vibroButton.image.sprite = _vibroStatus ? onButton : offButton;
               MMVibrationManager.SetHapticsActive(_vibroStatus);
            });
        }
    }
