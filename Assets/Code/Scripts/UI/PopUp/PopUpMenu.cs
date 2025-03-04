using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class PopUpMenu : PersistentSingleton<PopUpMenu>
    {
        public static Action PopUpOpenEvent;
        public static Action PopUpCloseEvent;

        [SerializeField] private PopUpSO popUpSo;

        private List<GameObject> _popUpList = new();

        public void OpenPopUp(GameObject popUp)
        {
            _popUpList.Add(Instantiate(popUp,transform));
            PopUpOpenEvent?.Invoke();
        }

        public void ClosePopUp(GameObject popUpGameObject)
        {
            GameObject temp = _popUpList.Find(popUp => popUp == popUpGameObject);
            _popUpList.Remove(temp);
            if (_popUpList.Count == 0)
            {
                PopUpCloseEvent?.Invoke();
            }
        }
    }
