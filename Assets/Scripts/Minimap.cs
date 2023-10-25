using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    float width = 1920 / 280;
    float height = 1080 / 280;
    void Update()
    {
        PlayerIcon.rotation = player.rotation;
        if (Destination.Count <= 0)
            return;
        for(int i = 0; i < Destination.Count; i++)
        {
            Vector2 change = (Destination[i].position - player.position) * 18;
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
                destinationIcon[i].GetComponent<Image>().color = Color.red;
            }
            else
            {
                destinationIcon[i].GetComponent<Image>().color = Color.clear;
            }
            destinationIcon[i].anchoredPosition = change;
        }
    }
}
