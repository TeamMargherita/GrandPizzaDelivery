using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerStat
{
    private Vector3 angle = new Vector3(0, 300, 0);
    void Start()
    {

    }
    void Update()
    {
        Speed *= acceleration;
        if (Input.GetKey(KeyCode.W))
        {
            Speed += acceleration;
        }
        if (Input.GetKey(KeyCode.S))
        {
            Speed -= acceleration;
        }
        float angleRatio = Speed / MaxSpeed;
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Rotate(-angle * angleRatio * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Rotate(angle * angleRatio * Time.deltaTime);
        }
        this.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }
}
