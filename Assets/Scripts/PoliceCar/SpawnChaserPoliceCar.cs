using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 한석호 작성
public class SpawnChaserPoliceCar : MonoBehaviour, ISpawnCar
{
	[SerializeField] private GameObject chaserPoliceCar;
	[SerializeField] private Transform playerTrans;

	private List<Vector3> vecList = new List<Vector3>();
    public void SpawnCar(int count)
	{
		vecList.Clear();
		for (int i = 0; i < count; i++)
		{
			while (true)
			{
				Vector3 ve = new Vector3(Random.Range(-10, -20), Random.Range(-5, 75));
				if (vecList.FindIndex(a => a.Equals(ve)) == -1)
				{
					vecList.Add(ve);
					GameObject obj = Instantiate(chaserPoliceCar, this.transform);
					obj.transform.localPosition = ve;
					obj.GetComponent<ISetTransform>().SetTransform(playerTrans);
					
					break;
				}
		} }
	}
}
