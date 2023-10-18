using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform player; // 플레이어(Transform)를 연결합니다.
    public List<Transform> Destination = new List<Transform>(); // 목적지(Transform)를 연결합니다.
    public List<RectTransform> destinationIcon = new List<RectTransform>();
    public RectTransform PlayerIcon;

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
        ResetDestinationIcon();
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
        if (Destination.Count <= 0)
            return;
        for(int i = 0; i < Destination.Count; i++)
        {
            Vector2 change = new Vector2((Destination[i].position.x - player.position.x) * 10, (Destination[i].position.y - player.position.y) * 10);
            if (change.x < -135)
                change.x = -135;
            if (change.x > 135)
                change.x = 135;
            if (change.y < -135)
                change.y = -135;
            if (change.y > 135)
                change.y = 135;
            destinationIcon[i].anchoredPosition = change;
        }
        
    }
}
