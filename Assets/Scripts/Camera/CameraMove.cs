using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    public bool playerDead;
    [SerializeField] private GameObject PlayerDeadPanel;
    private void Awake()
    {
        playerDead = false;
    }
    public void SlowMotion()
    {
        if (playerDead)
        {
            PlayerDeadPanel.SetActive(true);
            transform.position = player.transform.position + (Vector3.forward * -20);
            Camera.main.orthographicSize = 1;
        }
        else
        {
            transform.position = player.transform.position + (Vector3.forward * -20);
            Camera.main.orthographicSize = 3 + Math.Abs(player.GetComponent<PlayerMove>().Speed / 3);
        }
    }
    void Update()
    {
        SlowMotion();
    }
}
