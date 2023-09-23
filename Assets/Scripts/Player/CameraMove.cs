using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    void Update()
    {
        transform.position = player.transform.position + (Vector3.forward  * -20);
        Camera.main.orthographicSize = 3 + Math.Abs(player.GetComponent<PlayerMove>().Speed / 3);
    }
}
