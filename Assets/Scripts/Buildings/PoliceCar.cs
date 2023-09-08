using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PolicePathNS;

public class PoliceCar : MonoBehaviour, IPoliceCar
{
    private List<PolicePath> policePathList = new List<PolicePath>();

    private Transform trans;

    private Vector3 temRotate;
    private Vector3 temPosition;
    private float speed;
    private float dis;
    private float rotate;
    private int index;
    private bool nextBehaviour;
    private bool isLock = false;
    // Start is called before the first frame update
    void Awake()
    {
        trans = this.transform;
        InitValue();
    }

    private void InitValue()
    {
        speed = 3;
        dis = 0;
        rotate = 0;
        index = 0;
    }

    public void InitPoliceCarPath(List<PolicePath> policePathList)
    {
        this.policePathList.Clear();
        for (int i = 0; i < policePathList.Count; i++)
        {
            this.policePathList.Add(policePathList[i]);
        }
    }

    private void PoliceCarBehaviour(int choice, float value)
    {
        switch(choice)
        {
            case 1:
                Straight(value);
                break;
            case 2:
                Turn(value);
                TurnStraight(value);
                break;
        }
    }
    private void TurnStraight(float value)
    {
        trans.position += transform.right * (Mathf.PI / (2 * value * (-1)));
    }
    // 바라보는 방향으로 직진합니다.
    private void Straight(float dis)
    {
        if (!isLock)
        {
            temPosition = trans.position + (transform.right * dis);
            //Debug.Log(temPosition.x);
            isLock = true;
        }
        
        if (Vector3.SqrMagnitude(trans.position - temPosition)
            <= Vector3.SqrMagnitude((trans.position + transform.right * speed * Time.deltaTime) - temPosition))
        {
            trans.position = temPosition;
            //Debug.Log(trans.position);
            nextBehaviour = true;
            isLock = false;
        }
        else
        {
            trans.position += transform.right * speed * Time.deltaTime;
        }
    }
    // 회전합니다.
    private void Turn(float rotate)
    {
        if (this.rotate == 0f)
        {
            this.rotate = rotate;
            temRotate = trans.eulerAngles;
        }

        if (rotate > 0)
        {
            this.rotate += -1f;
            trans.Rotate(new Vector3(0, 0, 1f));
        }
        else if (rotate < 0)
        {
            this.rotate += 1f;
            trans.Rotate(new Vector3(0, 0, -1f));
        }

        if (this.rotate == 0f)
        {
            trans.eulerAngles = temRotate + new Vector3(0,0,rotate);
            nextBehaviour = true;
        }
    }
    //float q = 0;
    //float speed = 1f;
    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(policePathList.Count);
        if (policePathList.Count != 0)
        {
            Debug.Log($"{index}");
            PoliceCarBehaviour(policePathList[index].Behaviour, policePathList[index].Value);
        }

        if (nextBehaviour)
        {
            if (policePathList.Count - 1 == index)
            {
                index = 0;
            }
            else
            {
                index++;
            }
            nextBehaviour = false;
        }
    //    q += 0.1f;
    //    transform.position += transform.right * speed * Time.deltaTime;
    //    this.transform.Rotate(new Vector3(0,0,1f));
    }
}
