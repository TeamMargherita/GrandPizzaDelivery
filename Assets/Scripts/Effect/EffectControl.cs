using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 한석호 작성

public class EffectControl : MonoBehaviour, ISetTransform
{
    [SerializeField] private GameObject policeSmokeEffectObj;
    [SerializeField] private GameObject policeSmokeEffect;

    private List<GameObject> policeSmokeEffectList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }
    /// <summary>
    /// 오브젝트 풀링을 사용해서 연기를 생성
    /// </summary>
    /// <param name="trans"></param>
    public void SetTransform(Transform trans)
	{
        for (int i = 0; i < policeSmokeEffectList.Count; i++)
		{
            if (!policeSmokeEffectList[i].activeSelf)
			{
                policeSmokeEffectList[i].SetActive(true);
                policeSmokeEffectList[i].transform.position = trans.position
                    + new Vector3(Random.Range(-2, 3), Random.Range(-2, 3), 0).normalized * 0.2f
                    + trans.right * 0.5f;
                    

                return;
			}
		}

        GameObject obj = Instantiate(policeSmokeEffectObj);
        obj.transform.parent = policeSmokeEffect.transform;
        policeSmokeEffectList.Add(obj);
        obj.SetActive(true);
        obj.transform.position = trans.position
            + new Vector3(Random.Range(-2, 3),Random.Range(-2, 3),0).normalized * 0.2f
            + trans.right * 0.5f;

    }
}
