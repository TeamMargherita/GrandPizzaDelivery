using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 한석호 작성
public class PoliceSmokeEffect : MonoBehaviour
{
    private SpriteRenderer sprR;
    private Transform trans;
    private float colorA = 1f;
    // Start is called before the first frame update
    void OnEnable()
    {
        // 캐싱
        sprR = this.gameObject.GetComponent<SpriteRenderer>();
        trans = this.gameObject.transform;
        // 초기화
        sprR.color = new Color(1, 1, 1, 1);
        colorA = 1f;
        trans.localScale = Vector3.one;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (colorA > 0)
        {
            // 알파값을 서서히 낮춤
            colorA -= 2 / 255f;
            sprR.color = new Color(1, 1, 1, colorA);
            // 연기 이펙트 위치와 크기를 랜덤으로 이동시키고 줄이거나 키움
            trans.position += new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100)).normalized * Time.deltaTime * 3f;
            trans.localScale += new Vector3(Random.Range(-10, 100), Random.Range(-10, 100), 0).normalized * 0.1f;
        }
        else
        {
            // 알파값이 0 아래로 내려가면 알파값을 0으로 바꾸고, 게임 오브젝트를 꺼준다.
            colorA = 0f;
            this.gameObject.SetActive(false);
        }
    }
}
