using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class CameraFacer : MonoBehaviour
    {
        
        void Update()
        {
           // Vector3 tempRotation = Quaternion.LookRotation(Camera.main.transform.position).eulerAngles;
            
            /*transform.rotation = Quaternion.Euler(tempRotation);*/
            transform.rotation = Camera.main.transform.rotation;

        }
    }
