using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ExplainTextSetPosition : MonoBehaviour
{
    void Update()
    {
        transform.position = new Vector3(Input.mousePosition.x + 10, Input.mousePosition.y - 30, Input.mousePosition.z);
    }
}
