using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 한석호 작성

public class Banana : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<IHouse>() != null)
		{
			this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
		}
	}
}
