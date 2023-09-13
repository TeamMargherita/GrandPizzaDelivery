using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    private int hp = Mathf.Clamp(200, 1, 200);
    public float Braking = 0.97f;
    private float speed;
    public float MaxSpeed;
    public float acceleration = 10;

    public int HP
    {
        get { return hp; }
        set { hp = value; }
    }
    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            if(MaxSpeed < value)
            {
                value = MaxSpeed;
            }else if(-MaxSpeed > value)
            {
                value = -MaxSpeed;
            }
            speed = value;
        }
    }
}
