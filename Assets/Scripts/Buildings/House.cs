using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 한석호 작성

public class House : MonoBehaviour, IAddress
{
    private int houseNumber;

    public void InitAddress(int number)
    {
        houseNumber = number;
        //Debug.Log($"HouseNumber + {houseNumber}");
    }
    public int GetAddress()
    {
        return houseNumber;
    }

}
