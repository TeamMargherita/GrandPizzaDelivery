using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveTest : MonoBehaviour
{
    private Vector2 vec;

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Translate(new Vector3(Input.GetAxis("Horizontal") * 0.3f, Input.GetAxis("Vertical") * 0.3f));
    }
}
