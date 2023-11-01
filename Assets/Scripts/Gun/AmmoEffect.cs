using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoEffect : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(Vector2.up * 30 * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
