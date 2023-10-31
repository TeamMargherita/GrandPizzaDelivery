using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSmoke : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject, 1f);
    }

    private void Update()
    {
        //transform.Translate(new Vector2(0, -0.02f));
    }
}
