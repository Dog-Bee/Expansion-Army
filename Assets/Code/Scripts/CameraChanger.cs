using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

    public class CameraChanger : MonoBehaviour
    {
        [SerializeField] private Button cam1;
        [SerializeField] private Button cam2;
        [SerializeField] private Button cam3;
        [SerializeField] private Button cam4;

        [SerializeField] private CinemachineVirtualCamera camera1;
        [SerializeField] private CinemachineVirtualCamera camera2;
        [SerializeField] private CinemachineVirtualCamera camera3;
        [SerializeField] private CinemachineVirtualCamera camera4;
        private void Start()
        {
            AssignButtons();
        }

        private void AssignButtons()
        {
            cam1.onClick.AddListener(() =>
            {
                camera1.gameObject.SetActive(true);
                camera2.gameObject.SetActive(false);
                camera3.gameObject.SetActive(false);
                camera4.gameObject.SetActive(false);
                Camera.main.orthographic = false;
            });
            cam2.onClick.AddListener(() =>
            {
                camera1.gameObject.SetActive(false);
                camera2.gameObject.SetActive(true);
                camera3.gameObject.SetActive(false);
                camera4.gameObject.SetActive(false);
                Camera.main.orthographic = true;
            });
            cam3.onClick.AddListener(() =>
            {
                camera1.gameObject.SetActive(false);
                camera2.gameObject.SetActive(false);
                camera3.gameObject.SetActive(true);
                camera4.gameObject.SetActive(false);
                Camera.main.orthographic = true;
            });
            cam4.onClick.AddListener(() =>
            {
                camera1.gameObject.SetActive(false);
                camera2.gameObject.SetActive(false);
                camera3.gameObject.SetActive(false);
                camera4.gameObject.SetActive(true);
                Camera.main.orthographic = false;
            });
        }
    }
