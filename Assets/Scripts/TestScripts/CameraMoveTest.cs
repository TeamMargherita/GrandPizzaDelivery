using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 한석호 작성

public class CameraMoveTest : MonoBehaviour
{
    private Vector2 vec;

    // Update is called once per frame
    /// <summary>
    /// 카메라 테스트
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.None))
        {
            Debug.Log("작동4");
        }
        this.gameObject.transform.Translate(new Vector3(Input.GetAxis("Horizontal") * 0.3f, Input.GetAxis("Vertical") * 0.3f));
    }
}
