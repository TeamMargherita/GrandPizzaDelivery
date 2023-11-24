using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    public Transform player; // 플레이어(Transform)를 연결합니다.
    public class _destination
    {
        public int index;
        public Transform transform;
        public _destination(Transform t, int i)
        {
            index = i;
            transform = t;
        }
    }
    public List<_destination> Destination = new List<_destination>(); // 목적지(Transform)를 연결합니다.
    public List<RectTransform> destinationIcon = new List<RectTransform>();
    public RectTransform PlayerIcon;
    [SerializeField] private Transform PizzaHouse;
    [SerializeField] private RectTransform PizzaHouseIcon;


    public void CreateDestination(Request SDR, int index)
    {
        Destination.Add(new _destination(SDR.AddressS.IHouse.GetLocation(), index));
        ResetDestinationIcon();
    }
    /// <summary>
    /// 목적지를 삭제한다.
    /// </summary>
    /// <param name="destination">삭제할 집 Transform타입</param>
    public void DeleteDestination(int destination)
    {
        destinationIcon[Destination.Count - 1].gameObject.SetActive(false);
        Destination.RemoveAt(destination);
        ResetDestinationIcon();
    }

    private void ResetDestinationIcon()
    {
        for(int i = 0; i < destinationIcon.Count; i++)
        {
            if(i < SendDeliveryRequest.RequestList.Count)
            {
                if (SendDeliveryRequest.RequestList[i].Accept)
                {
                    destinationIcon[i].gameObject.SetActive(true);
                }
                else
                {
                    destinationIcon[i].gameObject.SetActive(false);
                }
            }
            /*if(i < Destination.Count)
            {
                destinationIcon[i].gameObject.SetActive(true);
            }
            else
                destinationIcon[i].gameObject.SetActive(false);*/
        }
    }
    private void StoreIconUpdate()
    {
        Vector2 change = (PizzaHouse.position - player.position) * 18;
        if (change.x < -135)
        {
            change.x = -135;
        }
        if (change.x > 135)
        {
            change.x = 135;
        }
        if (change.y < -135)
        {
            change.y = -135;
        }
        if (change.y > 135)
        {
            change.y = 135;
        }
        if (change.x <= -135 || change.x >= 135 || change.y <= -135 || change.y >= 135)
        {
            PizzaHouseIcon.GetComponent<Image>().color = Color.white;
        }
        else
        {
            PizzaHouseIcon.GetComponent<Image>().color = Color.clear;
        }
        PizzaHouseIcon.anchoredPosition = change;
    }
    void Update()
    {
        StoreIconUpdate();
        PlayerIcon.rotation = player.rotation;
        if (Destination.Count <= 0)
            return;
        for(int i = 0; i < Destination.Count; i++)
        {
            Vector2 change = (Destination[i].transform.position - player.position) * 18;
            if (change.x < -135)
            {
                change.x = -135;
            }
            if (change.x > 135)
            {
                change.x = 135;
            }
            if (change.y < -135)
            {
                change.y = -135;
            }
            if (change.y > 135)
            {
                change.y = 135;
            }
            if(change.x <= -135 || change.x >= 135 || change.y <= -135 || change.y >= 135)
            {
                destinationIcon[Destination[i].index].GetComponent<Image>().color = Color.red;
            }
            else
            {
                destinationIcon[Destination[i].index].GetComponent<Image>().color = Color.clear;
            }
            destinationIcon[Destination[i].index].anchoredPosition = change;
        }
    }
}
