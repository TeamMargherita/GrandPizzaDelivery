using System;
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

            float angleRatio;
            if(Speed > 0)
            {
                angleRatio = Speed / (Speed + (MaxSpeed / 2));
            }
            else
            {
                angleRatio = -(Speed / (Speed + (-MaxSpeed / 2)));
            }

            if (Input.GetKey(KeyCode.A))
            {
                this.transform.Rotate(angle * angleRatio * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D))
            {
                this.transform.Rotate(-angle * angleRatio * Time.deltaTime);
            }
            this.GetComponent<Rigidbody2D>().velocity = transform.rotation * new Vector2(0, Speed);
        }
        else
        {
            Speed = 0;
            this.GetComponent<Rigidbody2D>().velocity = transform.rotation * new Vector2(0, Speed);
        }
    }

    IEnumerator banana(float time)
    {
        float count = 0;
        while (true)
        {
            count += Time.deltaTime;
            this.transform.Rotate(angle * 10 * Time.deltaTime);
            if (count > time)
                break;
            yield return null;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Banana"))
        {
            StartCoroutine(banana(2));
        }
    }
}
