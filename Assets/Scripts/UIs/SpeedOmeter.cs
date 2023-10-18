using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedOmeter : MonoBehaviour
{
    [SerializeField]
    GameObject SpeedPin;
    [SerializeField]
    PlayerMove playerMove;
    private Vector3 angle = new Vector3(0, 0, 0);
    private void Update()
    {
        RotateUpdate();
    }
    void RotateUpdate()
    {
        angle.z = Mathf.Clamp(90 - (playerMove.Speed * 4.5f), -90, 90); ;
        SpeedPin.transform.eulerAngles = angle;
    }
}
