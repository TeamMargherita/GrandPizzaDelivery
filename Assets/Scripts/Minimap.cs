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
        // 플레이어와 목적지 위치를 MiniMap의 스케일에 맞게 조정하여 아이콘의 위치를 업데이트합니다.
        playerIcon.anchoredPosition = new Vector2(player.position.x, player.position.y) * 5;
        destinationIcon.anchoredPosition = new Vector2(destination.position.x, destination.position.y) * 5;
    }
}
