using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 한석호 작성

public class MapWallColider : MonoBehaviour
{
	public enum Arrow { UP, DOWN, RIGHT, LEFT };
	public Arrow ArrowState;

	private Transform trans;

	private void Awake()
	{
		trans = this.gameObject.transform;
	}
	/// <summary>
	/// 플레이어가 투명 벽에 닿으면 팅겨낸다.
	/// </summary>
	/// <param name="collision"></param>
	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.GetComponent<PlayerMove>() != null)
		{
			while (true)
			{
				if (ArrowState == Arrow.RIGHT)  // 오른쪽 벽
				{
					collision.transform.position += new Vector3(-0.01f, 0f);
					if (collision.transform.position.x <= trans.position.x - 0.5f)
					{
						break;
					}
				}
				else if (ArrowState == Arrow.LEFT)  // 왼쪽 벽
				{
					collision.transform.position += new Vector3(0.01f, 0f);
					if (collision.transform.position.x >= trans.position.x + 0.5f)
					{
						break;
					}
				}
				else if (ArrowState == Arrow.UP)    // 위쪽 벽
				{
					collision.transform.position += new Vector3(0f, -0.01f);
					if (collision.transform.position.y <= trans.position.y - 0.5f)
					{
						break;
					}
				}
				else if (ArrowState == Arrow.DOWN)    // 아래쪽 벽
				{
					collision.transform.position += new Vector3(0f, 0.01f);
					if (collision.transform.position.y >= trans.position.y + 0.5f)
					{
						break;
					}
				}
			}
		}
	}
}
