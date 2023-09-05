using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workers : MonoBehaviour
{
    public int career { get; }
    public int handy { get; }
    public int creativity { get; }
    public int agility { get; }
    public int pay { get; set; }

    public int totalAbility { get; set; }

    private void Awake()
    {
        totalAbility = handy + agility + creativity + career;
    }
}
