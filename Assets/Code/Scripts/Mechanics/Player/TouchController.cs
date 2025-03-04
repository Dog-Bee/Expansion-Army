using System;
using DG.Tweening;
using UnityEngine;

    public class TouchController : MonoBehaviour
    {
        [SerializeField] private Joystick joystick;
        private bool _isTouch;
        
        private Vector3 _computedDestination;

        private Vector2 _startPosition;
        private Vector2 _tempPosition;

        public Vector3 ComputedDestination => _computedDestination;

        private bool _isPaused;

        private void OnEnable()
        {
            PopUpMenu.PopUpOpenEvent += () =>
            {
                _isPaused = true;
                //TouchState();
                CheckDestination();

            };
            PopUpMenu.PopUpCloseEvent += () => _isPaused = false;
        }

        private void OnDisable()
        {
            PopUpMenu.PopUpOpenEvent -= () =>{
                _isPaused = true;
                CheckDestination();
                //TouchState();
            };
            PopUpMenu.PopUpCloseEvent -= () => _isPaused = false;
        }

        private void Update()
        {
            
            if (_isPaused) return;
            CheckDestination();
            //TouchState();

        }

        private void CheckDestination()
        {
            //Vector2 tempVector = (_tempPosition - _startPosition).normalized;
            Vector2 tempVector = joystick.Direction;
            float angle = Camera.main.transform.rotation.eulerAngles.x;
            tempVector = (Quaternion.Euler(0,0,-angle) * tempVector).normalized;
         

            _computedDestination = new Vector3(tempVector.x, 0, tempVector.y);
        }

        /*private void TouchState()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _startPosition = Input.mousePosition;
                _isTouch = true;
            }

            if (Input.GetMouseButtonUp(0)||_isPaused)
            {
                _isTouch = false;
                _startPosition = Vector2.zero;
                _tempPosition = Vector2.zero;
            }
            
            if(_isTouch)
                _tempPosition = Input.mousePosition;
        }*/
    }
