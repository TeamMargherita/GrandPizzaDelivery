using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform player; // 플레이어(Transform)를 연결합니다.
    public List<Transform> Destination = new List<Transform>(); // 목적지(Transform)를 연결합니다.
    public List<RectTransform> destinationIcon = new List<RectTransform>();
    public RectTransform PlayerIcon;
    //public SendDeliveryRequest SDR;
    

    public void CreateDestination(Request SDR)
    {
        Destination.Add(SDR.AddressS.IHouse.GetLocation());
        ResetDestinationIcon();
    }
    /// <summary>
    /// 목적지를 삭제한다.
    /// </summary>
    /// <param name="destination">삭제할 집 Transform타입</param>
    public void DeleteDestination(Transform destination)
    {
        destinationIcon[Destination.Count - 1].gameObject.SetActive(false);
        Destination.Remove(destination);
    }

    private void ResetDestinationIcon()
    {
        for(int i = 0; i < destinationIcon.Count; i++)
        {
            if(i < Destination.Count)
                destinationIcon[i].gameObject.SetActive(true);
            else
                destinationIcon[i].gameObject.SetActive(false);
        }
    }
    void Update()
    {
        //CreateDestination();
        if (Destination.Count <= 0)
            return;
        for(int i = 0; i < Destination.Count; i++)
        {
            //float distince = Vector2.Distance(new Vector2(Destination[i].position.x, Destination[i].position.y), new Vector2(player.position.x, player.position.y));
            Vector2 change = new Vector2((Destination[i].position.x - player.position.x) * 10, (Destination[i].position.y - player.position.y) * 10);
            // 플레이어와 목적지 위치를 MiniMap의 스케일에 맞게 조정하여 아이콘의 위치를 업데이트합니다.
            //playerIcon.anchoredPosition = new Vector2(player.position.x, player.position.y) * 6;
            if (change.x < -190)
                change.x = -190;
            if (change.x > 190)
                change.x = 190;
            if (change.y < -190)
                change.y = -190;
            if (change.y > 190)
                change.y = 190;

            /*if (change.y < -190 && change.y <= 190 && change.y >= -190)
            {
                destinationIcon[i].anchoredPosition = new Vector2(-190, change.y);
            }
            else if (change.x > 190 && change.y <= 190 && change.y >= -190)
            {
                destinationIcon[i].anchoredPosition = new Vector2(190, change.y);
            }
            else if (change.y < -190 && change.x <= 190 && change.x >= -190)
            {
                destinationIcon[i].anchoredPosition = new Vector2(change.x, -190);
            }
            else if (change.y > 190 && change.x <= 190 && change.x >= -190)
            {
                destinationIcon[i].anchoredPosition = new Vector2(change.x, 190);
            }*/
            destinationIcon[i].anchoredPosition = change;
        }
        
    }
}
