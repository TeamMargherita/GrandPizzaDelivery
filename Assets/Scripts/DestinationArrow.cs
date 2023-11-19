using UnityEngine;

public class DestinationArrow : MonoBehaviour
{
    [SerializeField] private Minimap minimap;
    [SerializeField] private RectTransform[] Arrow;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    /// <summary>
    /// ����ī�޶� ������������ false ������ true�� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <param name="_camera">����ī�޶�</param>
    /// <param name="_transform">��ǥƮ������</param>
    /// <returns>True or False</returns>
    public bool IsTargetVisible(Camera _camera, Transform _transform)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(_camera);
        var point = _transform.position;
        foreach (var plane in planes)
        {
            if (plane.GetDistanceToPoint(point) < 0)
            {
                return false;
            }

        }
        return true;
    }

    void Update()
    {
        for (int i = 0; i < 5; i++)
        {
            if (!UIControl.isIn)
            {
                if (minimap.Destination.Count > i)
                {
                    if (!IsTargetVisible(Camera.main, minimap.Destination[i]))
                    {
                        Vector2 change = (minimap.Destination[i].position - player.transform.position) * 100;
                        if (change.x < -900)
                        {
                            change.x = -900;
                        }
                        if (change.x > 900)
                        {
                            change.x = 900;
                        }
                        if (change.y < -480)
                        {
                            change.y = -480;
                        }
                        if (change.y > 480)
                        {
                            change.y = 480;
                        }
                        if (change.x > -900 && change.x < 900 && change.y > -480 && change.y < 480)
                        {
                            Arrow[i].gameObject.SetActive(false);
                        }
                        else
                        {
                            Arrow[i].gameObject.SetActive(true);
                            Arrow[i].right = change;
                        }
                        Arrow[i].anchoredPosition = change;
                    }
                    else
                        Arrow[i].gameObject.SetActive(false);
                }
                else
                    Arrow[i].gameObject.SetActive(false);
            }
            else
            {
                Arrow[i].gameObject.SetActive(false);
            }
        }
    }
}
