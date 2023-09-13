using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerStat
{
    private Vector3 angle = new Vector3(0, 0, 300);
    void Start()
    {

    }
    private float time;
    void Update()
    {
        time += Time.deltaTime;
        if (Input.GetKey(KeyCode.W))
        {
            Speed += acceleration * Time.deltaTime;
        }else if (Input.GetKey(KeyCode.S))
        {
            Speed -= acceleration * Time.deltaTime;
        }else if(Input.GetKey(KeyCode.Space))
        {
            if(time > 0.01f)
            {
                Speed *= Braking;
                time = 0;
            }
        }
        else
        {
            if (time > 0.02f)
            {
                Speed = Speed * 0.99f;
                time = 0;
            }
        }
        float angleRatio = Speed / MaxSpeed;
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Rotate(angle * angleRatio * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Rotate(-angle * angleRatio * Time.deltaTime);
        }
        this.transform.Translate(Vector3.up * Speed * Time.deltaTime);
    }
}
