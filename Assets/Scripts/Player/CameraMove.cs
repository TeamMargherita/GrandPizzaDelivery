using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    void Update()
    {
        transform.position = player.transform.position + (Vector3.forward*-20);
    }
}
