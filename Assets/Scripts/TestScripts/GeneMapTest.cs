using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneMapTest : MonoBehaviour
{
    [SerializeField] private GameObject testTile;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 60; i++)
        {
            for (int j = 0; j < 60; j++)
            {
                GameObject obj = Instantiate(testTile, this.transform);
                obj.transform.position = new Vector3(i, j);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
