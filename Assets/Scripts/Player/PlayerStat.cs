using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    private int hp = 200;
    public float Braking = 0.99f;
    [SerializeField]
    private float speed;
    public float MaxSpeed;
    public float acceleration = 10;

    public int HP
    {
        get { return hp; }
        set {
            if (value <= 0)
                Debug.Log("플레이어 사망");
            else if (value > 0)
                Debug.Log("플레이어 생존");
            hp = value;
        }
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
