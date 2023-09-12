using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCarCollisionCheck : MonoBehaviour
{
    private IPoliceCarControl iPoliceCarControl;
    private List<IPoliceCarControl> otherIPoliceCarIsBehaviourList = new List<IPoliceCarControl>();
    //경찰차가 다른 경찰차끼리 충돌할 우려가 있는지 체크한다.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //충돌할 우려가 있다면 자동차의 행동을 제어한다.
        if (collision.gameObject.GetComponent<IPoliceCarControl>() != null)
        {
            otherIPoliceCarIsBehaviourList.Add(collision.gameObject.GetComponent<IPoliceCarControl>());
            CheckPriority();
        }
    }

    private void CheckPriority()
    {
        if (iPoliceCarControl == null) { return; }

        if (otherIPoliceCarIsBehaviourList.FindIndex(a => a.GetPoliceCarCode() > iPoliceCarControl.GetPoliceCarCode()) != -1)
        {
            iPoliceCarControl.SetIsBehaviour(false);
            Debug.Log("작동1");
        }
        else
        {
            iPoliceCarControl.SetIsBehaviour(true);
            Debug.Log("작동2");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IPoliceCarControl>() != null)
        {
            otherIPoliceCarIsBehaviourList.Remove(collision.gameObject.GetComponent<IPoliceCarControl>());
            Invoke("CheckPriority", 1f);
        }
    }

    public void SetIPoliceCarIsBehaviour(IPoliceCarControl iPoliceCarIsBehaviour)
    {
        this.iPoliceCarControl = iPoliceCarIsBehaviour;
    }
}
