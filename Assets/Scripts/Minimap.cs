using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform player; // 플레이어(Transform)를 연결합니다.
    public Transform destination; // 목적지(Transform)를 연결합니다.
    public RectTransform playerIcon; // PlayerIcon GameObject를 연결합니다.
    public RectTransform destinationIcon; // DestinationIcon GameObject를 연결합니다.

    void Update()
    {
        Vector2 distince = destination.position - player.position;
        Vector2 change = new Vector2(player.position.x, player.position.y) + distince * 10;
        // 플레이어와 목적지 위치를 MiniMap의 스케일에 맞게 조정하여 아이콘의 위치를 업데이트합니다.
        //playerIcon.anchoredPosition = new Vector2(player.position.x, player.position.y) * 6;
        if (change.x < -200)
        {
            destinationIcon.anchoredPosition = new Vector2(-200, change.y);
        }else if (change.x > 200)
        {
            destinationIcon.anchoredPosition = new Vector2(200, change.y);
        }else if(change.y < -200)
        {
            destinationIcon.anchoredPosition = new Vector2(change.x, -200);
        }else if (change.y > 200)
        {
            destinationIcon.anchoredPosition = new Vector2(change.x, 200);
        }else
        {
            destinationIcon.anchoredPosition = change;
        }
    }
}
