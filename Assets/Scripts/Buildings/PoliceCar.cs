using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCar : MonoBehaviour
{
    private float dis;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // 바라보는 방향으로 직진합니다.
    private void Straight(int dis)
    {
        if (this.dis == 0f)
        {
            this.dis = dis;
        }

        this.dis -= 0.1f;
        transform.position += transform.right;

    }
    // 90도 회전합니다.
    private void Turn(int isRight)
    {
        //isRight = -1면 로테 z값을 -90 뺌 (true)
        //isRight = -2면 로테 z값을 90 더함 (false)
        if (isRight == -1)
        {

        }
        else if (isRight == -2)
        {

        }
    }
    //float q = 0;
    //float speed = 1f;
    // Update is called once per frame
    void FixedUpdate()
    {
    //    q += 0.1f;
    //    transform.position += transform.right * speed * Time.deltaTime;
    //    this.transform.Rotate(new Vector3(0,0,1f));
    
    }
}
