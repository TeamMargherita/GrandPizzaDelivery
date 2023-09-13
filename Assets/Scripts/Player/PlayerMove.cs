using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerStat
{
    private Vector3 angle = new Vector3(0, 0, 300);

    private float time;
    public bool Stop = false;
    void Update()
    {
        if (!Stop)
        {
            time += Time.deltaTime;
            if (Input.GetKey(KeyCode.W))
            {
                if (Speed < 0)
                {
                    Speed *= Braking;
                }
                Speed += acceleration * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                if (Speed > 0)
                {
                    Speed *= Braking;
                }
                Speed -= acceleration * Time.deltaTime;
            }
            else
            {
                if (time > 0.01f)
                {
                    Speed *= Braking;
                    time = 0;
                }
            }

            float angleRatio = Speed / (Speed + (MaxSpeed / 2));
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
        else
        {
            Speed = 0;
        }
    }
}
