using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    private int hp = Mathf.Clamp(200, 1, 200);
    private float braking;
    public float Speed;
    public float MaxSpeed;
    public float acceleration = 1;

    public int HP
    {
        get { return hp; }
        set { hp = value; }
    }


}
