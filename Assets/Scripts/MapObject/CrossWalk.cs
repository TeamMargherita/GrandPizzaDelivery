using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 한석호 작성
public class CrossWalk : MonoBehaviour, ICheckIsGreen
{
    [SerializeField] private Sprite redSpr;
    [SerializeField] private Sprite greenSpr;
    [SerializeField] private Sprite yellowSpr;

    private SpriteRenderer sprRenderer;
    private Coroutine lightCoroutine;
    private List<Police> policeList = new List<Police>();
    private bool isGreen = false;
    
    private void Awake()
    {
        sprRenderer = this.GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        lightCoroutine = StartCoroutine(LightChange());
    }
    /// <summary>
    /// 시간에 따른 신호등(횡단보도) 색 변화를 주는 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator LightChange()
    {
        // 횡단보도마다 시작 텀이 좀 다름
        int r = Random.Range(0, 9) * 90; 
        for (int i = 0; i < r; i++)
        {
            yield return Constant.OneTime;
        }

        while(true)
        {
            for(int i = 0; i < 1500; i++)
            {
                yield return Constant.OneTime; 
            }
            isGreen = true;
            sprRenderer.sprite = greenSpr;
            for (int i = 0; i < 450; i++)
            {
                yield return Constant.OneTime;
            }
            sprRenderer.sprite = yellowSpr; ;
            isGreen = false;
            for (int i = 0; i < 150; i++)
            {
                yield return Constant.OneTime;
            }
            sprRenderer.sprite = redSpr;
            // 횡단보도가 빨간불이 되면 경찰차를 다시 움직이게 함
            for (int i = 0; i < policeList.Count; i++)
            {
                policeList[i].PausePoliceCar(false);
            }
            policeList.Clear();

        }
    }
    /// <summary>
    /// 초록불인지를 판별해줌
    /// </summary>
    /// <returns></returns>
    public bool CheckIsGreen()
    {
        return isGreen;
    }
    /// <summary>
    /// 신호등에 따라 이동에 변화가 있는 콜라이더를 찾아서 움직임을 제어함
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 초록불이고 경찰차인 것들에만 해당
        if (collision.GetComponent<Police>() != null && isGreen)
        {
            // 경찰차를 일시정지 시킴
            collision.GetComponent<Police>().PausePoliceCar(true);
            // 일시정지한 경찰차를 나중에 해제하기 위해 리스트에 임시로 저장
            policeList.Add(collision.GetComponent<Police>());
        }
    }
}
