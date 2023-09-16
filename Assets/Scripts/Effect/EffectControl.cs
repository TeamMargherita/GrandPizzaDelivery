using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectControl : MonoBehaviour, IPoliceSmokeEffect
{
    [SerializeField] private GameObject policeSmokeEffectObj;
    [SerializeField] private GameObject policeSmokeEffect;

    private List<GameObject> policeSmokeEffectList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void InsPoliceSmokeEfectObj(Vector3 pos)
	{
        for (int i = 0; i < policeSmokeEffectList.Count; i++)
		{
            if (!policeSmokeEffectList[i].activeSelf)
			{
                policeSmokeEffectList[i].SetActive(true);
                policeSmokeEffectList[i].transform.position = pos;

                return;
			}
		}

        GameObject obj = Instantiate(policeSmokeEffectObj);
        obj.transform.parent = policeSmokeEffect.transform;
        policeSmokeEffectList.Add(obj);
        obj.SetActive(true);
        obj.transform.position = pos;

	}
}
