using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    //private Vector3 CameraPosition;
    //[SerializeField] private GunShooting gunShooting;
    
    void Update()
    {
        transform.position = player.transform.position + (Vector3.forward * -20);
        /*else
        {
            
            if(gunShooting.dir.x < 1 && gunShooting.dir.x > -1 && gunShooting.dir.y < 1 && gunShooting.dir.y > -1)
            {
                CameraPosition = player.transform.position + (gunShooting.dir) + (Vector3.forward * -20);
            }
            else
            {
                CameraPosition = player.transform.position + (gunShooting.dir.normalized) + (Vector3.forward * -20);
            }
            transform.position = CameraPosition;
        }*/
        Camera.main.orthographicSize = 3 + Math.Abs(player.GetComponent<PlayerMove>().Speed / 3);
    }
}
