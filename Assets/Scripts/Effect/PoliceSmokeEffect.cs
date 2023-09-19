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
        sprR = this.gameObject.GetComponent<SpriteRenderer>();
        trans = this.gameObject.transform;
        sprR.color = new Color(1, 1, 1, 1);
        colorA = 1f;
        trans.localScale = Vector3.one;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (colorA > 0)
        {
            colorA -= 2 / 255f;
            sprR.color = new Color(1, 1, 1, colorA);

            trans.position += new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100)).normalized * Time.deltaTime * 3f;
            trans.localScale += new Vector3(Random.Range(-10, 100), Random.Range(-10, 100), 0).normalized * 0.1f;
        }
        else
        {
            colorA = 0f;
            this.gameObject.SetActive(false);
        }
    }
}
