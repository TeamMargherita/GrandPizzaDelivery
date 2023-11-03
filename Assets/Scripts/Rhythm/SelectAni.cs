using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAni : MonoBehaviour
{
    [SerializeField] private GameObject[] Arrows;
    [SerializeField] private Animator Disk;


    public void AniStart()
    {
        foreach (var arrow in Arrows)
            arrow.SetActive(true);
        Disk.speed = 1;
    }

    public void AniPause()
    {
        foreach (var arrow in Arrows)
            arrow.SetActive(false);
        Disk.speed = 0;
    }
}
