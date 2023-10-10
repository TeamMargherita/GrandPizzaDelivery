using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField]
    private int hp = 200;
    public float Braking = 0.99f;
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
            value = Mathf.Clamp(value, -MaxSpeed, MaxSpeed);
            speed = value;
        }
    }
}
